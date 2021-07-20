using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class EstadiaController : Controller
    {
        [Route("Estadia/Registrar")]
        public ActionResult EstadiaRegistrar()
        {
            return View();
        }

        // GET: EstadiaControllercs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstadiaControllercs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadiaControllercs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadiaControllercs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadiaControllercs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadiaControllercs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstadiaControllercs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
