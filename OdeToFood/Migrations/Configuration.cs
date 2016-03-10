namespace OdeToFood.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.OdeToFoodDb>
    {
        public Configuration()
        {
            //Might want to set to false later on in development
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OdeToFood.Models.OdeToFoodDb context)
        {
            //Populate table with 'seed' data
            context.Restaurants.AddOrUpdate(r => r.Name,
                new Restaurant { Name = "Sabatino's", City = "Baltimore", Country = "USA" },
                new Restaurant { Name = "Great Lake", City = "Chicago", Country = "Canada" },
                new Restaurant {
                    Name = "Smaka",
                    City = "Gothenburg",
                    Country = "Sweden",
                    Reviews = new List<RestaurantReview>  {
                       new RestaurantReview { Rating = 9, Body = "Great Food!!!", ReviewerName = "Bob Hope" }
                    }
                }
            );

            SeedMembership();
        }

        private void SeedMembership()
        {
            ////Using WebMatrix
            //WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            //var roles = (SimpleRoleProvider)roles.Provider;
            //var membership = (SimpleMembershipProvide)membership.Provider;

            //if(!roles.RoleExits("Admin"))
            //{
            //    roles.CreateRole("Admin");
            //}
            //if(membership.GetUser("sallen", false) == null)
            //{
            //    membership.CreateUserAndAccount("sallen", "imalittleteapot");
            //}
            ////etc.
        }
    }
}
