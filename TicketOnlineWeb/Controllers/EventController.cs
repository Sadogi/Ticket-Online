using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketOnline.Interfaces;
using TicketOnline.Models.Data;
using TicketOnline.Models.Data.Repositories;
using G = TicketOnline.Models.Global;
using TicketOnlineWeb.Infrastructure;
using TicketOnlineWeb.Models.Forms;
using Microsoft.AspNetCore.Routing;
using System.Collections.ObjectModel;
using TicketOnlineWeb.Models.Event;

namespace TicketOnlineWeb.Controllers
{
    public class EventController : ControllerBase
    {
        private readonly IUserEventRepository<Event> _eventRequester;
        private readonly ApiRequester _apiRequester;
        private readonly ApiTokenRequester _apiTokenRequester;

        public EventController(ApiTokenRequester apiTokenRequester, IUserEventRepository<Event> eventRequester, ApiRequester apiRequester, ISessionManager sessionManager) : base(sessionManager)
        {
            _eventRequester = eventRequester;
            _apiRequester = apiRequester;
            _apiTokenRequester = apiTokenRequester;
        }

        // GET: Event
        public ActionResult Index()
        {            
            return View(_eventRequester.GetAll()
                                        .Where(e => e.Date >= DateTime.Now || e.Date is null)
                                        .OrderBy(e => e.Date));
        }

        #region Event and his comments
        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            //SessionManager.EventId = id;
            Event e = _eventRequester.Get(id);

            if(!(SessionManager.User is null))
                ViewBag.UserId = SessionManager.User.Id;

            ViewBag.EventId = id;
            //TempData["EventId"] = id;

            return View(new EventWithComment()
            {
                Id = e.Id,
                Name = e.Name,
                Type = e.Type,
                Organizer = e.Organizer,
                Date = e.Date,
                Location = e.Location,
                Tickets = e.Tickets,
                Price = e.Price,
                Description = e.Description,
                CommentList = _apiRequester.GetAll<G.GetComment>("comment/event/" + id).ToList()
            });
        }
        #endregion

        #region Booking and payment
        public ActionResult Book(int id, double price)
        {
            if (!(SessionManager.User is null))
                return View(new BookingForm() { UserId = SessionManager.User.Id, EventId = id, PurchaseDate = DateTime.Now, TicketsPrice = price });

            ViewBag.Message = "You must be logged in to book";
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(BookingForm b)
        {
            Event e = _eventRequester.Get(b.EventId);
            bool enoughTickets = e.Tickets >= b.TicketsPurchased;

            try
            {
                if (ModelState.IsValid && enoughTickets)
                {                   
                    G.Booking booking = new G.Booking()
                    {
                        UserId = b.UserId,
                        EventId = b.EventId,
                        PurchaseDate = DateTime.Now,
                        TicketsPurchased = b.TicketsPurchased,
                        TicketsPrice = b.TicketsPrice,
                        Amount = b.TicketsPurchased * b.TicketsPrice
                    };
                    return RedirectToAction(nameof(Payment), new RouteValueDictionary(booking));
                }

                ViewBag.Message = $"Only {e.Tickets} ticket(s) remaining";
                return View(b);
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Payment(G.Booking booking)
        {
            if (!(SessionManager.User is null))
            {
                return View(new BookingPayment()
                {
                    UserId = booking.UserId,
                    EventId = booking.EventId,
                    PurchaseDate = DateTime.Now,
                    TicketsPurchased = booking.TicketsPurchased,
                    TicketsPrice = booking.TicketsPrice,
                    Amount = booking.Amount
                });
            }

            ViewBag.Message = "You must be logged in to book";
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Payment(BookingPayment bookingPayment)
        {
            Event e = _eventRequester.Get(bookingPayment.EventId);
            bool enoughTickets = e.Tickets >= bookingPayment.TicketsPurchased;
            int? ticketsRemaining = e.Tickets - bookingPayment.TicketsPurchased;

            try
            {
                if (enoughTickets)
                {
                    //if (SessionManager.User.Id != 0 && !(SessionManager.User.Token is null))
                    //{
                    //    _apiTokenRequester.GetAllWithToken<G.PaymentMethod>("paymentMethod/all/" + SessionManager.User.Id, SessionManager.User.Token);
                    //}

                    if(_apiTokenRequester.UpdateWithToken(new Event(e.Id, e.Name, e.Type, e.Organizer, e.Date, e.Location, ticketsRemaining, e.Price, e.Description)
                                                          ,"event/" + bookingPayment.EventId, SessionManager.User.Token))
                    {
                        _apiTokenRequester.CreateWithToken(new G.Booking()
                        {
                            UserId = bookingPayment.UserId,
                            EventId = bookingPayment.EventId,
                            PurchaseDate = DateTime.Now,
                            TicketsPurchased = bookingPayment.TicketsPurchased,
                            TicketsPrice = bookingPayment.TicketsPrice,
                            Amount = bookingPayment.Amount
                        }, "booking", SessionManager.User.Token);
                    }                    

                    if (_apiTokenRequester.GetAllWithToken<G.PaymentMethod>("paymentMethod/all/" + SessionManager.User.Id, SessionManager.User.Token)
                        .Where(p => p.CardNumber == bookingPayment.CardNumber)
                        .FirstOrDefault() is null) 
                    {
                        _apiTokenRequester.CreateWithToken(new G.PaymentMethod
                        {
                            UserId = bookingPayment.UserId,
                            CardHolder = bookingPayment.CardHolder,
                            CardNumber = bookingPayment.CardNumber,
                            ExpirationDate = bookingPayment.ExpirationDate,
                            CVVnumber = bookingPayment.CVVnumber
                        }, "paymentMethod", SessionManager.User.Token);
                    }

                    TempData["Message"] = "Booking successfully completed";

                    return RedirectToAction("Index", "Booking");
                }

                ViewBag.Message = $"Only {e.Tickets} ticket(s) remaining";
                return View(bookingPayment);
            }
            catch
            {
                return View("Error");
            }
        }
        #endregion

        #region Comment
        public ActionResult Comment(int id)
        {
            if (!(SessionManager.User is null))
                return View(new G.Comment() { UserId = SessionManager.User.Id, EventId = id, Date = DateTime.Now });

            ViewBag.Message = "You must be logged in to leave a comment";
            return RedirectToAction("Index", "Auth");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(G.Comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _apiRequester.Create(comment, "comment");
                    return RedirectToAction(nameof(Details), new { id = comment.EventId });
                }

                return View(comment);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            if (SessionManager.User is null)
                return RedirectToAction("Index", "Auth");

            return View(_apiTokenRequester.GetWithToken<G.Comment>("comment/" + id, SessionManager.User.Token));
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, G.Comment comment)
        {

            try
            {
                if (!(SessionManager.User.Token is null) && comment.UserId == SessionManager.User.Id)
                {
                    if (_apiTokenRequester.UpdateWithToken(comment, "comment/" + id, SessionManager.User.Token))
                        return RedirectToAction(nameof(Details), new { id = comment.EventId });

                    ViewBag.Message = "Update failed";
                    return View(comment);
                }

                ViewBag.Message = "Session has expired";
                return RedirectToAction("Index", "Auth");
            }

            catch
            {
                return View("Error");
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            if (SessionManager.User is null)
                return RedirectToAction("Index", "Auth");

            G.Comment comment = _apiTokenRequester.GetWithToken<G.Comment>("comment/" + id, SessionManager.User.Token);
            ViewBag.returnUrl = Request.Headers["Referer"].ToString(); //stock l'url de la page qui appel la méthode delete, dans ce cas : Details/id
            
            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, string returnUrl) //string returnUrl récup value="@ViewBag.returnUrl" de la vue delete
        {
            //int CommentId = (int)TempData["EventId"];

            try
            {
                _apiTokenRequester.DeleteWithToken<G.Comment>("comment/" + id, SessionManager.User.Token);

                return Redirect(returnUrl);
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult CommentList(int id)
        {
            return View(_apiRequester.GetAll<G.GetComment>("comment/event/" + id));
        }
        #endregion
    }
}