using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AukcijeAPI.Controllers
{
    public class AukcijaController : Controller
    {
        // GET: AukcijaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AukcijaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AukcijaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AukcijaController/Create
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

        // GET: AukcijaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AukcijaController/Edit/5
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

        // GET: AukcijaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AukcijaController/Delete/5
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
