using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        //for the ability to return status codes..not restaurant
        //Post method..used System.Threading.tasks to bring in Task<>....POST makes something, like a create method
        //Had to bring in Restaurant from RestaurantRater.Models with CTRL period. 
        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant restaurant)
        {
            if (ModelState.IsValid && restaurant!=null)
           { 
            _context.Restaurants.Add(restaurant);
            //adds restaurant to snapshot..still have to save changes
            await _context.SaveChangesAsync();
            return Ok();
            }
            return BadRequest(ModelState);
        }

        //GET ALL
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            //Brought in ToListAsync with CTRL.
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            //have to return OK as a IHttpActionResult but also passed in restaurants as a parameter.
            return Ok(restaurants);
        }

        //GET BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant ==null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }       

        //PUT (UPDATE)
        //entities are the things in the database already
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody]Restaurant model)
        {
          if (ModelState.IsValid && model!=null)
            {
                Restaurant entity = await _context.Restaurants.FindAsync(id);
                if (entity !=null)
                {
                    entity.Name = model.Name;
                    entity.Rating = model.Rating;
                    entity.Style = model.Style;
                    entity.DollarSigns = model.DollarSigns;

                    await _context.SaveChangesAsync();

                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        //DELETE BY ID
        public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            
            if (restaurant==null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            if (await _context.SaveChangesAsync()==1)
            { 
            return Ok();
            }
            return InternalServerError();
        }
    }
}
