using Moq;
using OptiWash.Models;
using OptiWash.Models.DTOs;
using OptiWash.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiWash.Tests
{
    public class OrganizationTest
    {
        [Fact]
        public async Task GetByIdAsync_ShouldReturnOrganizationDto_WhenExists()
        {
            // Arrange
            var org = new Organization
            {
                Id = 1,
                Name = "TestOrg",
                Location = "Stockholm",
                Cars = new List<Car> { new Car { PlateNumber = "ABC123" } }
            };

            var mockRepo = new Mock<IOrganizationRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(org);

            var service = new OrganizationService(mockRepo.Object, new Mock<ICarRepository>().Object);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.Equal("TestOrg", result.Name);
            Assert.Single(result.CarPlateNumbers);
            Assert.Contains("ABC123", result.CarPlateNumbers);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IOrganizationRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Organization?)null);
            var service = new OrganizationService(mockRepo.Object, new Mock<ICarRepository>().Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => service.GetByIdAsync(99));
            Assert.Contains("hittades inte", ex.Message);
        }
        [Fact]
        public async Task AddAsync_ShouldCallRepositoryWithMappedOrganization()
        {
            // Arrange
            var dto = new OrganizationDto
            {
                Name = "New Org",
                Location = "Göteborg",
                CarPlateNumbers = new List<string> { "DEF456", "GHI789" }
            };

            var mockRepo = new Mock<IOrganizationRepository>();
            var service = new OrganizationService(mockRepo.Object, new Mock<ICarRepository>().Object);

            // Act
            await service.AddAsync(dto);

            // Assert
            mockRepo.Verify(r => r.AddAsync(It.Is<Organization>(o =>
                o.Name == "New Org" &&
                o.Cars.Count == 2 &&
                o.Cars.Any(c => c.PlateNumber == "DEF456") &&
                o.Cars.Any(c => c.PlateNumber == "GHI789")
            )), Times.Once);
        }
        [Fact]
        public async Task AddCarToOrganizationAsync_ShouldThrowException_WhenOrganizationNotFound()
        {
            // Arrange
            var mockOrgRepo = new Mock<IOrganizationRepository>();
            mockOrgRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Organization?)null);

            var service = new OrganizationService(mockOrgRepo.Object, new Mock<ICarRepository>().Object);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => service.AddCarToOrganizationAsync(1, 10));
            Assert.Contains("Organization not found", ex.Message);
        }

    }
}
