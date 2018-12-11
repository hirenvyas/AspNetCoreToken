using CityInfo.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityInfo.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CityDbContext _cityDbContext;
     
        public CitiesController(CityDbContext cityDbContext)
        {
         
            _cityDbContext = cityDbContext;
           

        }
        [HttpGet]
        public IActionResult GetCities()
        {
            var data = _cityDbContext.Cities;          
            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
           //  var d = _cityDbContext.Users.ToList();
            // var user = _applicationDbContext.Users.Select(u => u.UserName).ToList();

            //Calling SP
          //  var dataSP = _cityDbContext.Cities.FromSql("GetCity3").ToList();
                return Ok(_cityDbContext.Cities.FirstOrDefault(x => x.Id == id));
        }
        [HttpPost]
        public ActionResult PostCity([FromBody] CityDto city)
        {
         
            _cityDbContext.Cities.Add(city);
            _cityDbContext.SaveChangesAsync();
           
            return RedirectToAction("GetCity", new { id = city.Id });
        }
        [HttpPut]
        public IActionResult UpdateCity([FromBody] CityDto city)
        {
            var data = _cityDbContext.Cities.FirstOrDefault(x => x.Id == city.Id);
            data.Name = city.Name;
            data.Description = city.Description;
            data.PointsofInterests = city.PointsofInterests;
            _cityDbContext.Cities.Update(data);
            _cityDbContext.SaveChangesAsync();
            return RedirectToAction("GetCity", new { id = data.Id });
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCity(int id)
        {
            var data = _cityDbContext.Cities.FirstOrDefault(x => x.Id ==id);
            _cityDbContext.Cities.Remove(data);
            _cityDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
