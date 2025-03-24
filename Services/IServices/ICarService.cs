using OptiWash.Models.DTOs;

namespace OptiWash.Services.IServices
{
    public interface ICarService
    {
        Task<Car> GetCarByIdAsync(int id);
        Task<Car> GetCarByLicensePlateAsync(string licensePlate);
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task AddCarAsync(CarDto carDto);
        Task UpdateCarAsync(Car car);
        Task DeleteCarAsync(int id);
    }
}
