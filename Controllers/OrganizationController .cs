using Microsoft.AspNetCore.Mvc;
using OptiWash.Models.DTOs;
using OptiWash.Services.IServices;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _organizationService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) =>
        Ok(await _organizationService.GetByIdAsync(id));

    [HttpPost("{id}/AddCar")]
    public async Task<IActionResult> AddCarToOrganization(int id, [FromBody] AddCarToOrgDto request)
    {
        await _organizationService.AddCarToOrganizationAsync(id, request.CarId);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrganizationDto dto)
    {
        await _organizationService.AddAsync(dto);
        return Ok(new { message = "Organisationen har lagts till!" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] OrganizationDto dto)
    {
        await _organizationService.UpdateAsync(id, dto);
        return Ok(new { message = "Organisationen har uppdaterats!" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _organizationService.DeleteAsync(id);
        return Ok(new { message = "Organisationen har tagits bort." });
    }
}
