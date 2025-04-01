using OptiWash.Models;
using OptiWash.Models.DTOs;
using OptiWash.Repositories.IRepository;
using OptiWash.Services.IServices;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<IEnumerable<OrganizationDto>> GetAllAsync()
    {
        var orgs = await _organizationRepository.GetAllAsync();
        return orgs.Select(o => new OrganizationDto
        {
            Id = o.Id,
            Name = o.Name,
            Location = o.Location,
            CarPlateNumbers = o.Cars?
            .Select(c => c.PlateNumber)
            .ToList() ?? new List<string>()


        });
    }

    public async Task<OrganizationDto> GetByIdAsync(int id)
    {
        var o = await _organizationRepository.GetByIdAsync(id);
        if (o == null) throw new Exception("Organisationen hittades inte.");
        return 
            new OrganizationDto 
            { 
                Id = o.Id,
                Name = o.Name,
                Location = o.Location,
                CarPlateNumbers = o.Cars?
                .Select(c => c.PlateNumber)
                .ToList() ?? new List<string>()


            };
    }

    public async Task AddAsync(OrganizationDto dto)
    {
        var entity = new Organization 
        {
            Name = dto.Name,
            Location = dto.Location,
            Cars = dto.CarPlateNumbers.Select(plate => new Car
            {
                PlateNumber = plate
            }).ToList()

        };
        await _organizationRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(int id, OrganizationDto dto)
    {
        var existing = await _organizationRepository.GetByIdAsync(id);
        if (existing == null) throw new Exception("Organisationen hittades inte.");

        existing.Name = dto.Name;
        existing.Location = dto.Location;

        await _organizationRepository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        await _organizationRepository.DeleteAsync(id);
    }

    
}
