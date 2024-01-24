using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly DriverService _driverService;

        public DriversController(DriverService driverService)
        {
            _driverService=driverService;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult> Get(string id)
        {
            var driver = await _driverService.Get(id);
            if (driver == null) 
                return NotFound();
            return Ok(driver);
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var drivers = await _driverService.Get();
            if (drivers.Any())
                return Ok(drivers);
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Driver>> Add(Driver driver)
        {
            await _driverService.Create(driver);    
            return CreatedAtAction(nameof(Get),new { id = driver.Id} ,  driver);
        }

        [HttpPut]
        public async Task<ActionResult<Driver>> Update(string id, Driver driver)
        {
            driver.Id = id;
            await _driverService.Update(driver);
            return CreatedAtAction(nameof(Get), new { id = driver.Id }, driver);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> Delete(string id)
        {
            var exisyingDriver = await _driverService.Get(id);
            if (exisyingDriver is null)
                return BadRequest();

            await _driverService.Delete(id);
            return Ok(exisyingDriver);
        }




    }
}
