using Flykits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flykits.Controllers
{
    public class HomeController : Controller
    {
        private FlykitsDBEntities _db = new FlykitsDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(_db.Kits.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")] Kit kitToCreate)
        {
            if (!ModelState.IsValid) return View();

            _db.Kits.Add(kitToCreate);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var kitToEdit = (from k in _db.Kits
                           where k.Id == id
                           select k).First();
            return View(kitToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit( Kit kitToEdit)
        {
            var originalKit = (from k in _db.Kits
                               where k.Id == kitToEdit.Id
                               select k).First();
            if (!ModelState.IsValid) return View(originalKit);

            _db.Entry(originalKit).CurrentValues.SetValues(kitToEdit);
            _db.SaveChanges();

            return RedirectToAction("index");
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var originalKit = (from k in _db.Kits
                                   where k.Id == id
                                   select k).First();
                if(originalKit == null)
                {
                    return View("NotFound");
                }
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
