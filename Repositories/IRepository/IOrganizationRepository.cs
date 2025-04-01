﻿using OptiWash.Models;

namespace OptiWash.Repositories.IRepository
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllAsync();
        Task<Organization> GetByIdAsync(int id);
        Task AddAsync(Organization organization);
        Task UpdateAsync(Organization organization);
        Task DeleteAsync(int id);
    }
}
