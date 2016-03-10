using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;

namespace OdeToFood.Controllers
{
    //[Authorize(Users="bob@bob.com", "sallen")]
    //[Authorize(Roles="administrators","sales")]
    public class HomeController : Controller
    {
        IOdeToFoodDb _db;

        public HomeController()
        {
            _db = new OdeToFoodDb();
        }

        public HomeController(IOdeToFoodDb db)
        {
            _db = db;
        }

        public ActionResult AutoComplete(string term)
        {
            var model = _db.Query<Restaurant>()
                .Where(r => r.Name.StartsWith(term))
                .Take(10)
                .Select(r => new { label = r.Name });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        [OutputCache(Duration =60)]
        public ActionResult SayHello()
        {
            return Content("Hello");
        }


        //Using a chache profile from web.config
        [OutputCache(CacheProfile = "Short", VaryByHeader = "X-Requested-With; Accept-Language", Location = OutputCacheLocation.Server)]
        //Special caching - vary by header in case of bookmarked paaging result, in conjunction with server-side only caching
        //[OutputCache(Duration = 60, VaryByHeader = "X-Requested-With", Location = OutputCacheLocation.Server)]
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
                _db.Query<Restaurant>()
                    .OrderByDescending(r => r.Reviews.Average(review => review.Rating))
                    .Where(r => searchTerm == null || r.Name.StartsWith(searchTerm))
                    .Take(100)
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

        // This could be here or on the class [Authorize]
        // [AllowAnonymous] could override an [Authorize] on the main class
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