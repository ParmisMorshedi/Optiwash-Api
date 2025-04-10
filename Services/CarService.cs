using OptiWash.Models.DTOs;
using OptiWash.Repositories.IRepository;
using OptiWash.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptiWash.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Car> GetCarByLicensePlateAsync(string licensePlate)
        {
            try
            {
                return await _carRepository.GetCarByLicensePlateAsync(licensePlate);
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
                return await _carRepository.GetAllCarsAsync();
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
                await _carRepository.AddCarAsync(carDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding car: {ex.Message}", ex);
            }
        }

        public async Task UpdateCarAsync(CarDto carDto)
        {
            try
            {
                var existingCar = await _carRepository.GetCarByIdAsync(carDto.Id);
                if (existingCar == null)
                    throw new Exception($"Car with ID {carDto.Id} not found.");

                
                existingCar.PlateNumber = carDto.PlateNumber;
                existingCar.ScannedLicensePlate = carDto.ScannedLicensePlate;

                
                await _carRepository.UpdateCarAsync(existingCar);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating car with ID {carDto.Id}: {ex.Message}", ex);
            }
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            try
            {
                return await _carRepository.GetCarByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving car with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task DeleteCarAsync(int id)
        {
            try
            {
                await _carRepository.DeleteCarAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting car with ID {id}: {ex.Message}", ex);
            }
        }
    }
}
