using Microsoft.AspNetCore.Mvc;
using OptiWash.Services.IServices;
using OptiWash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using OptiWash.Models.DTOs;
using OptiWash.Models.Enums;
using static Azure.Core.HttpHeader;

namespace OptiWash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WashRecordsController : ControllerBase
    {
        private readonly IWashRecordService _washRecordService;
        private readonly ICarService _carService;
        private readonly IOrganizationService _organizationService;

        public WashRecordsController(IWashRecordService washRecordService, IOrganizationService organizationService)
        {
            _washRecordService = washRecordService;
            _organizationService = organizationService;
        }

        // GET: api/washrecords
        [HttpGet("car/{carId}")]
        public async Task<ActionResult<IEnumerable<WashRecordSimpleDto>>> GetWashRecords(int carId)
        {

            var washRecords = await _washRecordService.GetAllWashRecordsForCarAsync(carId); 
            return Ok(washRecords);
        }

        // GET: api/washrecords/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WashRecordDto>> GetWashRecord(int id)
        {
            var washRecord = await _washRecordService.GetWashRecordByIdAsync(id);
            if (washRecord == null)
            {
                return NotFound();
            }
            return Ok(washRecord);
        }
        // GET: api/washrecords/status/{status}
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<WashRecordWithOrganization_CarDto>>> GetByStatus(WashStatus status)
        {
            var records = await _washRecordService.GetWashRecordsByStatusAsync(status);
            var dtos = records.Select(r => new WashRecordWithOrganization_CarDto
            {
                Id = r.Id,
                CarId = r.CarId,
                CarPlateNumber = r.Car?.PlateNumber,
                OrganizationName = r.Car?.Organization?.Name,
                OrganizationCity = r.Car?.Organization?.Location,
                WashDate = r.WashDate,
                InteriorCleaned = r.InteriorCleaned,
                ExteriorCleaned = r.ExteriorCleaned,
                Status = r.Status,
                Notes = r.Notes
            }).ToList();

            return Ok(dtos);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<WashRecordSimpleDto>>> GetAll()
        {
            var records = await _washRecordService.GetAllWashRecordsAsync();
            return Ok(records);
        }

        // POST: api/washrecords
        [HttpPost]
        public async Task<ActionResult<WashRecordDto>> PostWashRecord(WashRecordDto washRecordDto)
        {
            await _washRecordService.AddWashRecordAsync(washRecordDto);
           return Ok(washRecordDto);
        }

        // PUT: api/washrecords/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWashRecord(int id, [FromBody] UpdateWashRecordDto dto)
        {
            var existing = await _washRecordService.GetWashRecordByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.WashDate = dto.WashDate;
            existing.InteriorCleaned = dto.InteriorCleaned;
            existing.ExteriorCleaned = dto.ExteriorCleaned;
            existing.Status = dto.Status;
            existing.Notes = dto.Notes;

            await _washRecordService.UpdateWashRecordAsync(existing);
            return NoContent();
        }

        // DELETE: api/washrecords/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWashRecord(int id)
        {
            await _washRecordService.DeleteWashRecordAsync(id);
            return NoContent();
        }
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyRecords([FromQuery] string month)
        {
            if (!DateTime.TryParse($"{month}-01", out DateTime monthDate))
                return BadRequest("Invalid month format");

            var allRecords = await _washRecordService.GetAllWashRecordsWithCarAndOrgAsync();

            var filtered = allRecords
                .Where(r => r.WashDate.Month == monthDate.Month && r.WashDate.Year == monthDate.Year)
                .ToList();

            var grouped = filtered
                .Where(r => r.Car.OrganizationId != null)
                .GroupBy(r => r.Car.OrganizationId.Value)
                .ToList();

            var completed = new List<WashCompanyDto>();

            var notCompleted = new List<WashCompanyDto>();

            foreach (var group in grouped)
            {
                var org = group.First().Car.Organization;

                var cars = group.Select(r => new WashCarDto
                {
                    PlateNumber = r.Car.PlateNumber,
                    Interior = r.InteriorCleaned,
                    Exterior = r.ExteriorCleaned,
                    Status = r.Status,
                    Note = r.Notes
                }).ToList();

                var company = new WashCompanyDto
                {
                    Id = org.Id,
                    Name = org.Name,
                    City = org.Location,
                    Cars = cars
                };

                if (cars.Any(c => c.Status == WashStatus.Completed))
                {
                    completed.Add(new WashCompanyDto
                    {
                        Id = org.Id,
                        Name = org.Name,
                        City = org.Location,
                        Cars = cars.Where(c => c.Status == WashStatus.Completed).ToList()
                    });
                }

                if (cars.Any(c => c.Status != WashStatus.Completed))
                {
                    notCompleted.Add(new WashCompanyDto
                    {
                        Id = org.Id,
                        Name = org.Name,
                        City = org.Location,
                        Cars = cars.Where(c => c.Status != WashStatus.Completed).ToList()
                    });
                }
            }

            return Ok(new { completed, notCompleted });
        }

    }
}

