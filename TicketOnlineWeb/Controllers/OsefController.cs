using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketOnlineWeb.Controllers
{
    public class OsefController : Controller
    {
        // GET: Osef
        public ActionResult Index()
        {
            return View();
        }

        // GET: Osef/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Osef/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Osef/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Osef/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Osef/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Osef/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Osef/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}