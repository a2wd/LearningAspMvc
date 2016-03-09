using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        OdeToFoodDb _db = new OdeToFoodDb();

        public ActionResult AutoComplete(string term)
        {
            var model = _db.Restaurants
                .Where(r => r.Name.StartsWith(term))
                .Take(10)
                .Select(r => new { label = r.Name });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            //var model =
            //    from r in _db.Restaurants
            //    orderby r.Reviews.Count() descending
            //    select r;

            //var model =
            //    from r in _db.Restaurants
            //    orderby r.Name
            //    select r;

            //var model =
            //    from r in _db.Restaurants
            //    orderby r.Reviews.Average(review => review.Rating)
            //    select r;

            //var model =
            //    from r in _db.Restaurants
            //    orderby r.Reviews.Average(review => review.Rating)
            //    select new RestaurantListViewModel {
            //        Id = r.Id,
            //        Name = r.Name,
            //        City = r.City,
            //        Country = r.Country,
            //        CountOfReviews = r.Reviews.Count()
            //    };

            //Only main bonus of lambdas & extensions is the ability to use extra functions like skip() or take()
            var model =
                _db.Restaurants
                    .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                    .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
                    .ToList()
                    .Select(r => new RestaurantListViewModel
                    {
                        Id = r.Id,
                        Name = r.Name,
                        City = r.City,
                        Country = r.Country,
                        CountOfReviews = r.Reviews.Count()
                    }).ToPagedList(page, 10);

            if(Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }

            return View(model);
        }

        public ActionResult About()
        {
            var model = new AboutModel();
            model.Location = "3chillies, Gilette Way";
            model.Name = "CSI Whitley";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if(_db != null)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}