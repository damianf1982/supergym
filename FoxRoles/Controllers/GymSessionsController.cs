using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoxRoles.Models;

namespace FoxRoles.Controllers
{
    [Authorize]
    public class GymSessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GymSessions
        public ActionResult Index()
        {
            return View(db.gymsessions.ToList());
        }

    

        // GET: GymSessions/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: GymSessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GymSessionName,RemainingPlaces")] GymSessions gymSessions)
        {
            if (ModelState.IsValid)
            {
               // return View(gymSessions);
                db.gymsessions.Add(gymSessions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gymSessions);
        }




        //GET: GymSessions/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GymSessions gymSessions = db.gymsessions.Find(id);
            if (gymSessions == null)
            {
                return HttpNotFound();
            }
            return View(gymSessions);
        }

        //POST: GymSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            GymSessions gymSessions = db.gymsessions.Find(id);
            db.gymsessions.Remove(gymSessions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
