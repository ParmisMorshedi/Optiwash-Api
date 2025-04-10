using Microsoft.EntityFrameworkCore;
using OptiWash.Models;
using OptiWash.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace OptiWash.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly OptiWashDbContext _context;

        public OrganizationRepository(OptiWashDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            try
            {
                return await _context.Organizations
                     .Include(o => o.Cars) 
        .               ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all organizations: {ex.Message}", ex);
            }
        }

        public async Task<Organization> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Organizations
                 .Include(o => o.Cars)
                 .FirstOrDefaultAsync(o => o.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving organization with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task AddAsync(Organization organization)
        {
            try
            {
                organization.CreatedDate = DateTime.UtcNow;
                await _context.Organizations.AddAsync(organization);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding organization: {ex.Message}", ex);
            }
        }

        public async Task UpdateAsync(Organization organization)
        {
            try
            {
                _context.Entry(organization).State = EntityState.Modified;

                foreach (var car in organization.Cars)
                {
                    _context.Entry(car).State = EntityState.Unchanged;
                }


                organization.UpdatedDate = DateTime.UtcNow;
                _context.Organizations.Update(organization);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating organization with ID {organization.Id}: {ex.Message}", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var org = await GetByIdAsync(id);
                if (org == null)
                    throw new KeyNotFoundException($"Organization with ID {id} not found.");

                _context.Organizations.Remove(org);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting organization with ID {id}: {ex.Message}", ex);
            }
        }
    }
}
