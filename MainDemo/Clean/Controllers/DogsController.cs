using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clean.Models;
using Clean.ViewModels;

namespace Clean.Controllers
{
    public class DogsController : Controller
    {
		protected override void HandleUnknownAction(string actionName)
		{
			RedirectToAction("Index").ExecuteResult(this.ControllerContext);
		}

        private AspConfDemoContext db = new AspConfDemoContext();

        //
        // GET: /Dogs/

        public ActionResult Index()
        {
            return View(db.Dogs.ToList());
        }

        //
        // GET: /Dogs/Details/5

        public ActionResult Details(int id = 0)
        {
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }

			var dogViewModel = new DogDetailsViewModel() { Name = dog.Name, Email = dog.Email };

            return View(dogViewModel);
        }

        //
        // GET: /Dogs/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Dogs/Create

        [HttpPost]
        public ActionResult Create(Dog dog)
        {
            if (ModelState.IsValid)
            {
                db.Dogs.Add(dog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dog);
        }

        //
        // GET: /Dogs/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        //
        // POST: /Dogs/Edit/5

        [HttpPost]
        public ActionResult Edit(Dog dog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dog);
        }

        //
        // GET: /Dogs/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        //
        // POST: /Dogs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Dog dog = db.Dogs.Find(id);
            db.Dogs.Remove(dog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}