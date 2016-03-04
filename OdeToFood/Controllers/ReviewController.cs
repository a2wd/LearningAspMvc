using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class ReviewController : Controller
    {
        //Child action only stops this action being called directly
        [ChildActionOnly]
        public ActionResult BestReview()
        {
            var bestReview = from r in _reviews
                             orderby r.Rating descending
                             select r;

            return PartialView("_Review", bestReview.First());
        }

        // GET: Review
        public ActionResult Index()
        {
            var model =
                from r in _reviews
                orderby r.Country
                select r;

            return View(model);
        }

        // GET: Review/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Review/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Review/Edit/5
        public ActionResult Edit(int id)
        {
            var review = _reviews.Single(r => r.Id == id);

            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var review = _reviews.Single(r => r.Id == id);

            if(TryUpdateModel(review))
            {
                //Save to database; validation succeed
                return RedirectToAction("Index");
            }

            return View(review);
        }

        // GET: Review/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Review/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        static List<RestaurantReview> _reviews = new List<RestaurantReview>
        {
            new RestaurantReview
            {
                Id = 1,
                Name = "Cinnamon Club",
                City = "London",
                Country = "UK",
                Rating = 10

            },

            new RestaurantReview
            {
                Id = 2,
                Name = "Marrakesh",
                City = "D.C.",
                Country = "USA",
                Rating = 1

            },


            new RestaurantReview
            {
                Id = 3,
                Name = "The House of Elliot",
                City = "Ghent",
                Country = "Belgium",
                Rating = 8

            },

            new RestaurantReview
            {
                Id = 4,
                Name = "XSS Example",
                City = "Fortunately, encoded for us",
                Country = "<script>alert('xss')</script>",
                Rating = 0
            }
        };
    }
}
