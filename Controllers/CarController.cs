using Microsoft.AspNetCore.Mvc;
using OptiWash.Services.IServices;
using OptiWash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using OptiWash.ViewModels;
using OptiWash.Models.DTOs;

namespace OptiWash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET: api/cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarViewModel>>> GetCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            return Ok(cars);
        }

        // GET: api/cars/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarId(int id)
        {
            var car = await _carService.GetCarByIdAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        // POST: api/cars
        [HttpPost]
        public async Task<ActionResult<CarDto>> PostCar(CarDto carDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _carService.AddCarAsync(carDto);

                return Ok("Car created");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the category.", error = ex.Message });
            }
        }

        // PUT: api/cars/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }
            await _carService.UpdateCarAsync(car);
            return NoContent();
        }

        // DELETE: api/cars/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _carService.DeleteCarAsync(id);
            return NoContent();
        }
    }
}

