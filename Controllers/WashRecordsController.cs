using Microsoft.AspNetCore.Mvc;
using OptiWash.Services.IServices;
using OptiWash.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using OptiWash.Models.DTOs;

namespace OptiWash.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WashRecordsController : ControllerBase
    {
        private readonly IWashRecordService _washRecordService;
        private readonly ICarService _carService;

        public WashRecordsController(IWashRecordService washRecordService)
        {
            _washRecordService = washRecordService;
        }

        // GET: api/washrecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WashRecord>>> GetWashRecords()
        {

            var washRecords = await _washRecordService.GetIncompleteWashRecordsAsync();
            return Ok(washRecords);
        }

        // GET: api/washrecords/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WashRecord>> GetWashRecord(int id)
        {
            var washRecord = await _washRecordService.GetWashRecordByIdAsync(id);
            if (washRecord == null)
            {
                return NotFound();
            }
            return Ok(washRecord);
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
        public async Task<IActionResult> PutWashRecord(int id, WashRecord washRecord)
        {
            if (id != washRecord.Id)
            {
                return BadRequest();
            }
            await _washRecordService.UpdateWashRecordAsync(washRecord);
            return NoContent();
        }

        // DELETE: api/washrecords/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWashRecord(int id)
        {
            await _washRecordService.DeleteWashRecordAsync(id);
            return NoContent();
        }
    }
}

