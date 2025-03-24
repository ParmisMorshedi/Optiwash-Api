using Microsoft.EntityFrameworkCore;
using OptiWash.Models.DTOs;
using OptiWash.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CarRepository : ICarRepository
{
    private readonly OptiWashDbContext _context;

    public CarRepository(OptiWashDbContext context)
    {
        _context = context;
    }

    public async Task<Car> GetCarByIdAsync(int id)
    {
        try
        {
            return await _context.Cars.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving car with ID {id}: {ex.Message}", ex);
        }
    }

    public async Task<Car> GetCarByLicensePlateAsync(string licensePlate)
    {
        try
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.PlateNumber == licensePlate);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving car with license plate {licensePlate}: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        try
        {
            return await _context.Cars.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving all cars: {ex.Message}", ex);
        }
    }

    public async Task AddCarAsync(CarDto carDto)
    {
        try
        {
            if (carDto == null)
                throw new ArgumentNullException(nameof(carDto), "Car cannot be null");
            var car = new Car
            {
                Id = carDto.Id,
                PlateNumber = carDto.PlateNumber,
                ScannedLicensePlate = carDto.ScannedLicensePlate
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding car: {ex.Message}", ex);
        }
    }

    public async Task UpdateCarAsync(Car car)
    {
        try
        {
            if (car == null)
                throw new ArgumentNullException(nameof(car), "Car cannot be null");

            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating car with ID {car.Id}: {ex.Message}", ex);
        }
    }

    public async Task DeleteCarAsync(int id)
    {
        try
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                throw new KeyNotFoundException($"Car with ID {id} not found.");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting car with ID {id}: {ex.Message}", ex);
        }
    }
}
