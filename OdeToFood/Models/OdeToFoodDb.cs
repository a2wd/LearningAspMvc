using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class OdeToFoodDb : DbContext
    {
        //Could hard code the connection string, preferable to store in the web.config
        //public OdeToFoodDb() : base("server=...") { }
        //Alternatively, can specify the 'default conection' (in the web.config)
        public OdeToFoodDb() : base("name=DefaultConnection") { }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantReview> Reviews { get; set; }

        public System.Data.Entity.DbSet<OdeToFood.Models.RestaurantListViewModel> RestaurantListViewModels { get; set; }
    }
}