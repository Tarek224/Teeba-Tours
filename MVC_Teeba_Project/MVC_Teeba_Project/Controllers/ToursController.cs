using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Teeba_Project.Models;

namespace MVC_Teeba_Project.Controllers
{
    public class ToursController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View(context.Tours.ToList());
        }

        // GET: Tours/Details/5
        public ActionResult TourDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = context.Tours.FirstOrDefault(model => model.ID == id);
            if (tour == null)
            {
                return HttpNotFound();
            }

            ViewBag.discountprice = tour.Price - (tour.Price * (decimal)0.1);
            
            return View(tour);
        }
        public ActionResult TourPrograms(int id)
        {
            List<Tour_Program> Tour_programs = context.Tour_Programs.Where(model => model.Tour_Id == id).ToList();
            List<Program> programs = new List<Program>();
            List<Place> places = new List<Place>();

            foreach (var item in Tour_programs)
            {
                var query = context.Programs.FirstOrDefault(m => m.ID == item.Program_Id);
                programs.Add(query);
            }
            foreach (var item in programs)
            {
                var query = context.Places.FirstOrDefault(m => m.ID == item.Place_Id);
                places.Add(query);
            }
            ViewBag.Places = places;

            return PartialView("_ToursProgram", programs);
        }

        [Authorize]
        public ActionResult BookTour(int id)
        {
            ViewBag.TourBooked = context.Tours.FirstOrDefault(m => m.ID == id);
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken,Authorize] 
        public ActionResult BookTour(Passenger passenger , int TourId)
        {
            if (!ModelState.IsValid)
            {
                try {
                    passenger.Tour_Id = TourId;
                    passenger.Email = User.Identity.Name;
                    context.Passengers.Add(passenger);
                    Tour tour = context.Tours.FirstOrDefault(m => m.ID == TourId);
                    tour.Num_pass += 1;
                    context.SaveChanges();
                    ViewBag.TourBooked = context.Tours.FirstOrDefault(m => m.ID == TourId);
                    return RedirectToAction("TourDetails", new { id = TourId });

                }catch(Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            else
            {
                ViewBag.TourBooked = context.Tours.FirstOrDefault(m => m.ID == TourId);
                return View(passenger);
            }
        }

        [HttpPost, ValidateAntiForgeryToken,Authorize]
        public ActionResult SetDependants(int TourId, Dependent dependent)
        {
            if (ModelState.IsValid)
            {
                if (context.Passengers.FirstOrDefault(m => m.Email == User.Identity.Name) != null)
                {
                    var passenger = context.Passengers.FirstOrDefault(p => p.Email == User.Identity.Name);
                    dependent.Passenger_Id = passenger.ID;
                    context.Dependents.Add(dependent);
                    Tour tour = context.Tours.FirstOrDefault(m => m.ID == TourId);
                    tour.Num_pass += 1;
                    context.SaveChanges();
                    ViewBag.TourBooked = context.Tours.FirstOrDefault(m => m.ID ==TourId);
                    return View("BookTour");
                }
                else
                {
                    ViewBag.TourBooked = context.Tours.FirstOrDefault(m => m.ID == TourId);
                    ModelState.AddModelError(string.Empty, "You should register and at least in one tour before add any dependents");
                    return View("BookTour");
                }
            }
            else
            {
                return View("BookTour");
            }
        }

        ////////////////////////////   Admin   /////////////////////////////////////////

        [Authorize(Roles ="Admin")]
        public ActionResult AdminIndex()
        {
            return View(context.Tours.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            
            return View(context.Tours.FirstOrDefault(t => t.ID == id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Num_pass,Num_Days,Location,Price,Title,Date,Hotel_Name")] Tour tour,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                Images imgs = new Images();
                var f = System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/Content/images/Tours/" + f));
                imgs.image1 = upload.FileName;
                imgs.image2 = upload.FileName;
                imgs.image3 = upload.FileName;
                imgs.image4 = upload.FileName;
                imgs.image5 = upload.FileName;
                tour.Images = imgs;
                context.Tours.Add(tour);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tour);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = context.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Name,Num_pass,Num_Days,Location,Price,Title,Date,Hotel_Name")] Tour tour, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                Images imgs = new Images();
                var f = System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/Content/images/Tours/" + f));
                imgs.image1 = upload.FileName;
                imgs.image2 = upload.FileName;
                imgs.image3 = upload.FileName;
                imgs.image4 = upload.FileName;
                imgs.image5 = upload.FileName;
                tour.Images = imgs;
                context.Entry(tour).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tour);
        }

        // GET: Tours/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = context.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = context.Tours.Find(id);
            context.Tours.Remove(tour);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
