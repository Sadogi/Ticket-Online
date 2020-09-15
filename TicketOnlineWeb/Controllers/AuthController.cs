using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoTools;
using Microsoft.AspNetCore.Mvc;
using TicketOnline.Forms;
using TicketOnline.Interfaces;
using TicketOnline.Models.Data;
using TicketOnlineWeb.Infrastructure;
using TicketOnlineWeb.Models.Forms.Mappers;

namespace TicketOnlineWeb.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository<RegisterForm, LoginForm, User> _authRepository;
        private readonly ICryptoRSA _cryptoService;
        private readonly IUserEventRepository<User> _userRepository;

        public AuthController(IUserEventRepository<User> userRepository, IAuthRepository<RegisterForm, LoginForm, User> authRepository, ISessionManager sessionManager, ICryptoRSA cryptoService) : base(sessionManager)
        {
            _authRepository = authRepository;
            _cryptoService = cryptoService;
            _userRepository = userRepository;
        }

        [AnonymousRequired]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        #region Login : if sinstead of try/catch

        //[AnonymousRequired]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        //[GetPublicKey]
        //[HttpPost]
        //[AnonymousRequired]
        //public IActionResult Login(LoginForm loginForm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        loginForm.Passwd = Convert.ToBase64String(_cryptoService.Crypter(loginForm.Passwd));


        //        User user = _authRepository.Login(loginForm);

        //        if (!(user is null))
        //        {
        //            SessionManager.User = user;
        //            return RedirectToAction("Index", "Event");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Incorrect Email or password!";
        //            return View(loginForm.ToWeb());
        //        }

        //    }
        //    return View("Error");
        //}

        #endregion

        [AnonymousRequired]
        public IActionResult Login()
        {
            return View();
        }
        [GetPublicKey]
        [HttpPost]
        [AnonymousRequired]
        public IActionResult Login(LoginForm loginForm)
        {
            if (!(loginForm is null) && ModelState.IsValid)
            {
                loginForm.Passwd = Convert.ToBase64String(_cryptoService.Crypter(loginForm.Passwd));

                try 
                {
                    User user = _authRepository.Login(loginForm);

                    if (!(user is null))
                    {
                        SessionManager.User = user;
                        return RedirectToAction("Index", "Event");
                    }
                    
                    ViewBag.Message = "Incorrect Email or password !";
                }                 
                catch(Exception ex) 
                { 
                    return View("Error");
                }                
            }

            return View(loginForm.ToWeb());
        }

        [AnonymousRequired]
        public IActionResult Register()
        {
            return View();
        }

        [GetPublicKey]
        [HttpPost]
        [AnonymousRequired]
        public IActionResult Register(RegisterForm registerForm)
        {
            bool screenName = _userRepository.GetAll().Where(u => u.ScreenName == registerForm.ScreenName).FirstOrDefault() is null;
            bool email = _userRepository.GetAll().Where(u => u.Email == registerForm.Email).FirstOrDefault() is null;

            if (!(registerForm is null) && ModelState.IsValid)
            {
                registerForm.Passwd = Convert.ToBase64String(_cryptoService.Crypter(registerForm.Passwd));

                try
                {
                    if (screenName && email)
                    {
                        _authRepository.Register(registerForm);
                        return RedirectToAction("Login");
                    }

                    else if (!screenName)
                        ViewBag.Message = "Screen Name already exists";

                    else
                        ViewBag.Message = "Email already used";
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }

            return View(registerForm.ToWeb());
        }

        [AuthRequired]
        public IActionResult Logout()
        {
            SessionManager.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}