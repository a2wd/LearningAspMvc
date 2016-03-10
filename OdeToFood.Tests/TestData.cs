using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Tests
{
    class TestData
    {
        public static IQueryable<Restaurant> Restaurants
        {
            get
            {
                var restaurants = new List<Restaurant>();
                for (int i = 0; i < 100; i++)
                {
                    var restaurant = new Restaurant();                    
                    restaurant.Reviews = new List<RestaurantReview>()
                    {
                        new RestaurantReview { Rating = 4 }
                    };
                    restaurants.Add(restaurant);                    
                }
                return restaurants.AsQueryable();
            }
        }
    }
}
