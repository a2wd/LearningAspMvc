using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeToFood.Models;
using System.Collections.Generic;
using System.Linq;

// A restaurant's overall rating can be caluclated using various methods.
// For this application we'll want to try different methods over time, 
// but for starters we'll allow an administrator to toggle between two 
// different techniques.
//
// 1. Simple mean of the "rating" value for the most recent n reviews
//    (the admin can configure the value n).
//
// 2. Weighted mean of the last n reviews. The most recent n/2 reviews
//    will be weighted twice as much and the oldest n/2 reviews. 
//
// Overall rating should be a whole number.
namespace OdeToFood.Tests.Features
{
    [TestClass]
    public class RatingTests
    {
        [TestMethod]
        public void Computes_Result_For_One_Reviews()
        {
            var data = BuildRestaurantAndReviews(ratings: 4);

            var rater = new RestaurantRater(data);
            var result = rater.ComputeResult(new SimpleRatingAlgorithm(), 10);

            Assert.AreEqual(4, result.Rating);
        }

        [TestMethod]
        public void Computes_Result_For_Two_Reviews()
        {
            var data = BuildRestaurantAndReviews(ratings: new[] { 4, 8 });

            var rater = new RestaurantRater(data);
            var result = rater.ComputeResult(new SimpleRatingAlgorithm(), 10);

            Assert.AreEqual(6, result.Rating);
        }

        [TestMethod]
        public void Weighted_Averaging_For_Two_Reviews()
        {
            var data = BuildRestaurantAndReviews(3, 9);

            var rater = new RestaurantRater(data);
            var result = rater.ComputeResult(new WeightedRatingAlgorithm(), 10);

            Assert.AreEqual(5, result.Rating);
        }

        [TestMethod]
        public void Ratings_Includes_Only_First_N_Reviews()
        {
            var data = BuildRestaurantAndReviews(1, 1, 1, 10, 10, 10);

            var rater = new RestaurantRater(data);
            var result = rater.ComputeResult(new SimpleRatingAlgorithm(), 3);

            Assert.AreEqual(1, result.Rating);
        }

        private Restaurant BuildRestaurantAndReviews(params int[] ratings)
        {
            var restaurant = new Restaurant();

            restaurant.Reviews = ratings.Select(r => new RestaurantReview { Rating = r }).ToList();
            return restaurant;
        }
    }
}
