using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FoxRoles.Models;
using Microsoft.AspNet.Identity;

namespace Gymbo.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //testt for git
        // GET: Bookings
        
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            // ApplicationDbContext db1 = new ApplicationDbContext();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                 (x => x.Id == currentUserId);

            return View(db.booking.ToList().Where(x => x.User == currentUser));
        }

        // GET: Bookings/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings bookings = db.booking.Find(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            return View(bookings);
        }

        // GET: Bookings/Create
        
        public ActionResult Create()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            //This takes the gym sessions from the database 
            // sends it to the view to be displayed in a dropdown list
            List<GymSessions> list = db.gymsessions.ToList();

            ViewBag.GymSessionsList = new SelectList(list, "Id", "GymSessionName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,GymSessionId,GymSessionName")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                int gsid = bookings.GymSessionId;


                //  string GSname = db.gymsessions.Find(GymSessionsName);

                GymSessions gymSessions = db.gymsessions.Find(gsid);
                string GSname = gymSessions.GymSessionName;
                int rp = gymSessions.RemainingPlaces;


                if (rp > 0)
                {
                    db.gymsessions.Remove(gymSessions);
                    db.SaveChanges();
                    rp -= 1;


                    bookings.GymSessionName = GSname;

                    string currentUserId = User.Identity.GetUserId();
                    // ApplicationDbContext db1 = new ApplicationDbContext();
                    ApplicationUser currentUser = db.Users.FirstOrDefault
                         (x => x.Id == currentUserId);
                    bookings.User = currentUser;
                    gymSessions.RemainingPlaces = rp;

                    db.gymsessions.Add(gymSessions);

                    db.booking.Add(bookings);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return Redirect("Full");
                }

            }


            return View(bookings);
        }

        // GET: Bookings/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings bookings = db.booking.Find(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GymSessionId,GymSessionName")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings bookings = db.booking.Find(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            return View(bookings);
        }

        // POST: Bookings/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Bookings bookings = db.booking.Find(id);
        //    db.booking.Remove(bookings);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //public ActionResult Full()
        //{
        //    return View();
        //}
    }
}
