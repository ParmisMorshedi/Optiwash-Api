using OptiWash.Models;
using OptiWash.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptiWash.Services.IServices
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationDto>> GetAllAsync();
        Task<OrganizationDto> GetByIdAsync(int id);
        Task AddAsync(OrganizationDto dto);
        Task UpdateAsync(int id, OrganizationDto dto);
        Task AddCarToOrganizationAsync(int orgId, int carId);

        Task DeleteAsync(int id);
    }
}
