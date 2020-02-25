using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class RestaurantDbContext:DbContext//Inherited from Entity framework, allows to communicate with database
    {
        public RestaurantDbContext() :base("DefaultConnection") //Calls constructor 
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}