using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketOnline.Interfaces;
using TicketOnline.Models.Data.Repositories;
using TicketOnline.Models.Global;
using TicketOnlineWeb.Infrastructure;

namespace TicketOnlineWeb.Controllers
{
    public class BookingController : ControllerBase
    {
        private readonly ApiTokenRequester _apiTokenRequester;

        public BookingController(ApiTokenRequester apiTokenRequester, ISessionManager sessionManager) : base(sessionManager)
        {
            _apiTokenRequester = apiTokenRequester;
        }
        // GET: Booking
        public ActionResult Index()
        {
            if (!(SessionManager.User is null))
            {
                ViewBag.Message = TempData["Message"];
                return View(_apiTokenRequester.GetAllWithToken<GetBooking>("booking/user/" + SessionManager.User.Id, SessionManager.User.Token));
            }

            return View("Index", "Auth");
        }       
    }
}