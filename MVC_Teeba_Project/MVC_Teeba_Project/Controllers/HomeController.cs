using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVC_Teeba_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC_Teeba_Project.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PlacesForHome()
        {
            List<Place> places = context.Places.Where(model => model.Popularity == Popularity.Popular).ToList();
            return PartialView("_Places" , places);
        }
        public ActionResult AllPlaces(Location location)
        {
            List<Place> places = context.Places.Where(m => m.Location == location).ToList();
            return PartialView("_Places", places);
        }
    }
}