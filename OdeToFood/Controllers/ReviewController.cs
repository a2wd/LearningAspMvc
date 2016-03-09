using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class ReviewController : Controller
    {
        OdeToFoodDb _db = new OdeToFoodDb();

        // GET: Review
        public ActionResult Index([Bind(Prefix="id")] int restaurantId)
        {
            var restaurant = _db.Restaurants.Find(restaurantId);
            if(restaurant != null)
            {
                return View(restaurant);
            }

            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Create(int restaurantId)
        {
            var review = new RestaurantReview();
            review.RestaurantId = restaurantId;
            return View(review);
        }

        [HttpPost]
        public ActionResult Create(RestaurantReview review)
        {
            if(ModelState.IsValid)
            {
                _db.Reviews.Add(review);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }

            return View(review);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _db.Reviews.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Exclude ="ReviewerName")]RestaurantReview review)
        {
            //Re-populate reviewer name
            var originalReview = _db.Reviews.AsNoTracking().Where(r => r.Id == review.Id).FirstOrDefault();
            if (originalReview != null)
            {
                review.ReviewerName = originalReview.ReviewerName;

                if (ModelState.IsValid)
                {
                    _db.Entry(review).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index", new { id = review.RestaurantId });
                }
            }

            return View(review);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
