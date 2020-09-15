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
using CryptoTools;
using TicketOnlineWeb.Models.Forms.Mappers;

namespace TicketOnlineWeb.Controllers
{
    public class UserController : ControllerBase
    {
        public readonly ApiTokenRequester _apiTokenRequester;
        public readonly IUserEventRepository<User> _userRequester;
        private readonly ICryptoRSA _cryptoService;

        public UserController(ApiTokenRequester apiTokenRequester, IUserEventRepository<User> userRequester, ICryptoRSA cryptoService, ISessionManager sessionManager) : base(sessionManager)
        {
            _cryptoService = cryptoService;
            _apiTokenRequester = apiTokenRequester;
            _userRequester = userRequester;
        }

        // GET: User
        public ActionResult Index()
        {
            if(!(SessionManager.User is null))
                return View(_userRequester.Get(SessionManager.User.Id));

            ViewBag.Message = "You must be logged in to access your profile";
            return RedirectToAction("Index", "Auth");
        }      

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User user = _userRequester.Get(id);

            return View(new EditUser() 
            { 
                LastName = user.LastName, FirstName = user.FirstName, ScreenName = user.ScreenName,
                Email = user.Email, Address = user.Address
            });
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditUser form)
        {
            User u = _userRequester.Get(SessionManager.User.Id);
            bool canUpdate = form.LastName != u.LastName || form.FirstName != u.FirstName || form.ScreenName != u.ScreenName || form.Email != u.Email || form.Address != u.Address;
            bool screenName = _userRequester.GetAll().Where(u => u.Id != SessionManager.User.Id && u.ScreenName == form.ScreenName).FirstOrDefault() is null;
            bool email = _userRequester.GetAll().Where(u => u.Id != SessionManager.User.Id && u.Email == form.Email).FirstOrDefault() is null;        
                               
            try
            {
                if (ModelState.IsValid && (SessionManager.User.Id == id) && !(SessionManager.User.Token is null))
                {
                    if (canUpdate)
                    {
                        if (screenName && email)
                        {                          
                            _apiTokenRequester.UpdateWithToken(new User(id, form.LastName, form.FirstName, form.ScreenName, form.Email, form.Address)
                                                               , "user/" + id, SessionManager.User.Token);

                            ViewBag.Message = "Profile successfully updated";
                            return RedirectToAction(nameof(Index));
                        }

                        else if (!screenName)
                            ViewBag.Message = "Screen Name already used";

                        else
                            ViewBag.Message = "Email address not avilable";

                        return View(form);
                    }

                    ViewBag.Message = "You changed nothing, nice try";
                    return View(form);
                }

                ViewBag.Message = "Session has expired";
                return RedirectToAction("Index", "Auth");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Osef/Edit/5
        public ActionResult EditPassword()
        {
            return View();
        }

        // POST: Osef/Edit/5
        [GetPublicKey]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(G.Password form)
        {
            if (!(form is null) && ModelState.IsValid && (form.Passwd != form.NewPasswd))
            {
                form.Passwd = Convert.ToBase64String(_cryptoService.Crypter(form.Passwd));
                form.NewPasswd = Convert.ToBase64String(_cryptoService.Crypter(form.NewPasswd));

                try
                {
                    if (_apiTokenRequester.UpdateWithToken(form, "user/pwd/" + SessionManager.User.Id, SessionManager.User.Token))
                    {
                        ViewBag.Message = "Password successfully changed";
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = "Wrong password";
                    return View();
                }
                catch
                {
                    return View("Error");
                }
            }

            ViewBag.Message = "Old and new password can't be the same";
            return View();
        }
    }
}