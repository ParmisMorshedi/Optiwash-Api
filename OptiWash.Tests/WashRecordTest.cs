using Moq;
using OptiWash.Repositories.IRepository;
using OptiWash.Services;
using OptiWash.Models.DTOs;
using OptiWash.Models.Enums;
using OptiWash.Models;

namespace OptiWash.Tests
{
    public class WashRecordServiceTests
    {
        [Fact]
        public async Task AddWashRecordAsync_ShouldCallRepository_WhenCarExists()
        {
            // Arrange
            var carId = 1;
            var washDto = new WashRecordDto
            {
                CarId = carId,
                InteriorCleaned = true,
                ExteriorCleaned = true,
                Status = WashStatus.Completed,
                Notes = "Test wash"
            };

            var mockCarRepo = new Mock<ICarRepository>();
            mockCarRepo.Setup(c => c.GetCarByIdAsync(carId))
                       .ReturnsAsync(new Car { Id = carId });

            var mockWashRepo = new Mock<IWashRecordRepository>();
            var service = new WashRecordService(mockWashRepo.Object, mockCarRepo.Object);

            // Act
            await service.AddWashRecordAsync(washDto);

            // Assert
            mockWashRepo.Verify(r => r.AddWashRecordAsync(It.Is<WashRecord>(w =>
                w.CarId == carId &&
                w.InteriorCleaned == true &&
                w.ExteriorCleaned == true &&
                w.Status == WashStatus.Completed &&
                w.Notes == "Test wash"
            )), Times.Once);
        }

        [Fact]
        public async Task AddWashRecordAsync_ShouldThrow_WhenCarDoesNotExist()
        {
            // Arrange
            var washDto = new WashRecordDto { CarId = 999 };

            var mockCarRepo = new Mock<ICarRepository>();
            mockCarRepo.Setup(c => c.GetCarByIdAsync(999)).ReturnsAsync((Car?)null);

            var mockWashRepo = new Mock<IWashRecordRepository>();
            var service = new WashRecordService(mockWashRepo.Object, mockCarRepo.Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() => service.AddWashRecordAsync(washDto));
            Assert.Contains("Invalid CarId", ex.Message);
        }
        [Fact]
        public async Task GetAllWashRecordsForCarAsync_ShouldReturnMappedDtos()
        {
            // Arrange
            var carId = 1;
            var records = new List<WashRecord>
            {
            new WashRecord 
            {
                Id = 1, CarId = carId,
                InteriorCleaned = true,
                ExteriorCleaned = false,
                Status = WashStatus.Completed,
                WashDate = DateTime.Today,
                Notes = "1" 
            },
            new WashRecord
            {
                Id = 2, CarId = carId,
                InteriorCleaned = false,
                ExteriorCleaned = true,
                Status = WashStatus.Failed,
                WashDate = DateTime.Today,
                Notes = "2" 
            },
        };

            var mockWashRepo = new Mock<IWashRecordRepository>();
            mockWashRepo.Setup(r => r.GetAllWashRecordsForCarAsync(carId)).ReturnsAsync(records);

            var service = new WashRecordService(mockWashRepo.Object, new Mock<ICarRepository>().Object);

            // Act
            var result = (await service.GetAllWashRecordsForCarAsync(carId)).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(WashStatus.Completed, result[0].Status);
            Assert.Equal("2", result[1].Notes);
        }

        [Fact]
        public async Task GetIncompleteWashRecordsAsync_ShouldReturnOnlyIncompleteRecords()
        {
            // Arrange
            var records = new List<WashRecord>
            {
                new WashRecord 
                { 
                    Id = 1,
                    InteriorCleaned = false,
                    ExteriorCleaned = true 
                },
                new WashRecord 
                { Id = 2,
                    InteriorCleaned = true,
                    ExteriorCleaned = false
                }
            };

            var mockWashRepo = new Mock<IWashRecordRepository>();
            mockWashRepo.Setup(r => r.GetIncompleteWashRecordsAsync()).ReturnsAsync(records);

            var service = new WashRecordService(mockWashRepo.Object, new Mock<ICarRepository>().Object);

            // Act
            var result = (await service.GetIncompleteWashRecordsAsync()).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, r => Assert.True(!r.InteriorCleaned || !r.ExteriorCleaned));
        }

        [Fact]
        public async Task GetWashRecordsByStatusAsync_ShouldReturnOnlyMatchingStatus()
        {
            // Arrange
            var expectedStatus = WashStatus.Completed;
            var mockData = new List<WashRecord>
            {
                new WashRecord
                {
                    Id = 1,
                    Status = WashStatus.Completed,
                    Car = new Car {
                    PlateNumber = "ABC123",
                        Organization = new Organization
                        {
                            Id = 1,
                            Name = "TestOrg"
                        }
                    }
                },
                new WashRecord {
                    Id = 2,
                    Status = WashStatus.Pending,
                    Car = new Car {
                        PlateNumber = "XYZ999",
                        Organization = new Organization 
                        {
                            Id = 2,
                            Name = "AnotherOrg"
                        }
                    }
                }
            };

            var mockRepo = new Mock<IWashRecordRepository>();
            mockRepo.Setup(r => r.GetWashRecordsWithCarAndOrgAsync()).ReturnsAsync(mockData);

            var service = new WashRecordService(mockRepo.Object, new Mock<ICarRepository>().Object);

            // Act
            var result = (await service.GetWashRecordsByStatusAsync(expectedStatus)).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("ABC123", result[0].Car.PlateNumber);
            Assert.Equal("TestOrg", result[0].Car.Organization.Name);
        }

        [Fact]
        public void FilterMonthlyRecords_ShouldGroupByOrgId_AndSplitByCompletionStatus()
        {
            // Arrange
            var month = new DateTime(2025, 5, 1);
            var records = new List<WashRecord>
            {
                new WashRecord
                {
                    WashDate = new DateTime(2025, 5, 10),
                    Status = WashStatus.Completed,
                    InteriorCleaned = true,
                    ExteriorCleaned = true,
                    Car = new Car {
                        PlateNumber = "ABC123",
                        OrganizationId = 1,
                        Organization = new Organization { Id = 1, Name = "Org1", Location = "Stockholm" }
                    }
                },
                new WashRecord
                {
                    WashDate = new DateTime(2025, 5, 15),
                    Status = WashStatus.Failed,
                    InteriorCleaned = true,
                    ExteriorCleaned = false,
                    Car = new Car {
                    PlateNumber = "DEF456",
                    OrganizationId = 1,
                     Organization = new Organization { Id = 1, Name = "Org1", Location = "Stockholm" }
                    }
                }
            };

            // Act
            var grouped = records
                .Where(r => r.WashDate.Month == month.Month && r.WashDate.Year == month.Year)
                .Where(r => r.Car.OrganizationId != null)
                .GroupBy(r => r.Car.OrganizationId.Value)
                .ToList();

            var completed = grouped.Select(g => new WashCompanyDto
            {
                Id = g.First().Car.Organization.Id,
                Name = g.First().Car.Organization.Name,
                City = g.First().Car.Organization.Location,
                Cars = g.Where(c => c.Status == WashStatus.Completed).Select(r => new WashCarDto
                {
                    PlateNumber = r.Car.PlateNumber,
                    Interior = r.InteriorCleaned,
                    Exterior = r.ExteriorCleaned,
                    Status = r.Status,
                    Note = r.Notes
                }).ToList()
            }).ToList();

            // Assert
            Assert.Single(completed);
            Assert.Equal("Org1", completed[0].Name);
            Assert.Single(completed[0].Cars);
            Assert.Equal(WashStatus.Completed, completed[0].Cars[0].Status);
        }

    }
}
