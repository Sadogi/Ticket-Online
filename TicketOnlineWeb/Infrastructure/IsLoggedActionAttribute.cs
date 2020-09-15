using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketOnlineWeb.Controllers;

namespace TicketOnlineWeb.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class IsLoggedActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ControllerBase controller = (ControllerBase)context.Controller;
            //controller.ViewBag.IsLogged = !(controller.SessionManager.User.Token is null);
            controller.ViewBag.IsLogged = !(controller.SessionManager.User is null);
        }
    }
}
