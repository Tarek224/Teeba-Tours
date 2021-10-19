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
    public class PlacesController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: Places
        public ActionResult Index(Location location)
        {
            if (location == Location.Luxor)
            {
                Place place = context.Places.FirstOrDefault(m => m.Name == "Luxor City");
                return View("LuxorIndex", place);
            }
            Place Aswanplace = context.Places.FirstOrDefault(m => m.Name == "Aswan City");
            return View("AswanIndex" , Aswanplace);
        }

        // GET: Places/Details/5
        public ActionResult PlaceDetails(int id)
        {
            Place place = context.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }


        //Get : AdminIndex
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex()
        {
            return View(context.Places.ToList());
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Location,long_Description,Short_Description,Popularity,Foriegner_Ticket,Egyptian_Ticket,Rate")] Place place,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                Images imgs = new Images();
                var f = System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/Content/images/Places/" + f));
                imgs.image1 = upload.FileName;
                imgs.image2 = upload.FileName;
                imgs.image3 = upload.FileName;
                imgs.image4 = upload.FileName;
                imgs.image5 = upload.FileName;
                place.Images = imgs;
                context.Places.Add(place);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(place);
        }

        // GET: Places/Edit/5
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = context.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public ActionResult Edit([Bind(Include = "ID,Name,Location,long_Description,Short_Description,Popularity,Foriegner_Ticket,Egyptian_Ticket,Rate")] Place place,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                Images imgs = new Images();
                var f = System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/Content/images/Places/" + f));
                imgs.image1 = upload.FileName;
                imgs.image2 = upload.FileName;
                imgs.image3 = upload.FileName;
                imgs.image4 = upload.FileName;
                imgs.image5 = upload.FileName;
                place.Images = imgs;
                context.Entry(place).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(place);
        }

        // GET: Places/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Place place = context.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = context.Places.Find(id);
            context.Places.Remove(place);
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
