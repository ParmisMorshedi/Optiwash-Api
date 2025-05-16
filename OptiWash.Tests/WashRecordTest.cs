using Moq;
using OptiWash.Repositories.IRepository;
using OptiWash.Services;
using OptiWash.Models.DTOs;
using OptiWash.Models.Enums;

namespace OptiWash.Tests
{
    public class WashRecordServiceTests
    {
        [Fact]
        public async Task GetWashRecordByIdAsync_ReturnsCorrectRecord()
        {
            // Arrange
            var mockWashRepo = new Mock<IWashRecordRepository>();
            var mockCarRepo = new Mock<ICarRepository>();

            mockWashRepo.Setup(repo => repo.GetWashRecordByIdAsync(123))
                        .ReturnsAsync(new WashRecord { Id = 123, CarId = 456 });

            var service = new WashRecordService(mockWashRepo.Object, mockCarRepo.Object);

            // Act
            var result = await service.GetWashRecordByIdAsync(123);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(456, result.CarId);
        }

        [Fact]
        public async Task AddWashRecordAsync_CallsRepositoryMethod()
        {
            // Arrange
            var mockWashRepo = new Mock<IWashRecordRepository>();
            var mockCarRepo = new Mock<ICarRepository>();
            var service = new WashRecordService(mockWashRepo.Object, mockCarRepo.Object);

            var dto = new WashRecordDto
            {
                CarId = 111,
                WashDate = DateTime.UtcNow,
                InteriorCleaned = true,
                ExteriorCleaned = false,
                Status = WashStatus.Completed,
                Notes = "Test from unit test"
            };

            mockCarRepo.Setup(r => r.GetCarByIdAsync(111))
               .ReturnsAsync(new Car { Id = 111 });
            // Act
            await service.AddWashRecordAsync(dto);

            // Assert
            mockWashRepo.Verify(r => r.AddWashRecordAsync(It.IsAny<WashRecord>()), Times.Once);
        }

        [Fact]
        public async Task AddWashRecordAsync_ThrowsException_WhenCarNotFound()
        {
            // Arrange
            var mockWashRepo = new Mock<IWashRecordRepository>();
            var mockCarRepo = new Mock<ICarRepository>();
            var service = new WashRecordService(mockWashRepo.Object, mockCarRepo.Object);

            var dto = new WashRecordDto
            {
                CarId = 999,
                WashDate = DateTime.UtcNow
            };

            mockCarRepo.Setup(r => r.GetCarByIdAsync(999))
                       .ReturnsAsync((Car)null!);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddWashRecordAsync(dto));
        }

        [Fact]
        public async Task GetAllWashRecordsForCarAsync_ReturnsMappedDtos()
        {
            // Arrange
            var mockWashRepo = new Mock<IWashRecordRepository>();
            var mockCarRepo = new Mock<ICarRepository>();
            var service = new WashRecordService(mockWashRepo.Object, mockCarRepo.Object);

            var records = new List<WashRecord> { new WashRecord
            {
            Id = 1,
            CarId = 100,
            WashDate = DateTime.UtcNow,
            InteriorCleaned = true,
            ExteriorCleaned = true,
            Notes = "Ok",
            Status = WashStatus.Completed
            },
             new WashRecord
            {
                 Id = 2,
                 CarId = 100,
                 WashDate = DateTime.UtcNow,
                 InteriorCleaned = false,
                 ExteriorCleaned = true, Notes = "Missed interior",
                 Status = WashStatus.Failed }
            };

            mockWashRepo.Setup(r => r.GetAllWashRecordsForCarAsync(100))
                        .ReturnsAsync(records);

            // Act
            var result = await service.GetAllWashRecordsForCarAsync(100);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, r => Assert.Equal(100, r.CarId));
        }

    }
}
