using Moq;
using OptiWash.Models.DTOs;
using OptiWash.Repositories.IRepository;
using OptiWash.Services;


namespace OptiWash.Tests
{
    public class CarTest
    {
        [Fact]
        public async Task GetCarByIdAsync_ShouldReturnCar_WhenCarExists()
        {
            // Arrange
            var carId = 1;
            var expectedCar = new Car { Id = carId, PlateNumber = "ABC123" };
            var mockRepo = new Mock<ICarRepository>();
            mockRepo.Setup(repo => repo.GetCarByIdAsync(carId)).ReturnsAsync(expectedCar);
            var service = new CarService(mockRepo.Object);

            // Act
            var result = await service.GetCarByIdAsync(carId);

            // Assert
            Assert.Equal(expectedCar, result);
        }
        [Fact]
        public async Task AddCarAsync_ShouldCallRepositoryWithCorrectData()
        {
            // Arrange
            var carDto = new CarDto { Id = 1, PlateNumber = "ABC123", ScannedLicensePlate = "ABC123" };
            var mockRepo = new Mock<ICarRepository>();
            var service = new CarService(mockRepo.Object);

            // Act
            await service.AddCarAsync(carDto);

            // Assert
            mockRepo.Verify(r => r.AddCarAsync(carDto), Times.Once);
        }
        [Fact]
        public async Task UpdateCarAsync_ShouldThrowException_WhenCarNotFound()
        {
            // Arrange
            var carDto = new CarDto { Id = 99, PlateNumber = "DEF456" };
            var mockRepo = new Mock<ICarRepository>();
            mockRepo.Setup(r => r.GetCarByIdAsync(carDto.Id)).ReturnsAsync((Car?)null);
            var service = new CarService(mockRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => service.UpdateCarAsync(carDto));
            Assert.Contains("not found", ex.Message);
        }
        [Fact]
        public async Task DeleteCarAsync_ShouldCallRepositoryMethod()
        {
            // Arrange
            var carId = 2;
            var mockRepo = new Mock<ICarRepository>();
            var service = new CarService(mockRepo.Object);

            // Act
            await service.DeleteCarAsync(carId);

            // Assert
            mockRepo.Verify(r => r.DeleteCarAsync(carId), Times.Once);
        }


    }
}
