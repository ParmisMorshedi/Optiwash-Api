using Microsoft.EntityFrameworkCore;
using OptiWash.Models.Enums;
using OptiWash.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class WashRecordRepository : IWashRecordRepository
{
    private readonly OptiWashDbContext _context;

    public WashRecordRepository(OptiWashDbContext context)
    {
        _context = context;
    }

    public async Task<WashRecord> GetWashRecordByIdAsync(int id)
    {
        try
        {
            return await _context.WashRecords.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving wash record with ID {id}: {ex.Message}", ex);
        }
    }
    public async Task<IEnumerable<WashRecord>> GetByStatusAsync(WashStatus status)
    {
        return await _context.WashRecords
            .Where(w => w.Status == status)
            .Include(w => w.Car)
            .ToListAsync();
    }


    public async Task<IEnumerable<WashRecord>> GetAllWashRecordsForCarAsync(int carId)
    {
        try
        {
            return await _context.WashRecords
                .Where(w => w.CarId == carId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving wash records for car ID {carId}: {ex.Message}", ex);
        }
    }
    public async Task<IEnumerable<WashRecord>> GetAllWashRecordsAsync()
    {
        try
        {
            return await _context.WashRecords
                .Include(w => w.Car)  
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving all wash records: {ex.Message}", ex);
        }
    }


    public async Task<IEnumerable<WashRecord>> GetIncompleteWashRecordsAsync()
    {
        try
        {
            return await _context.WashRecords
                .Where(w => !w.InteriorCleaned || !w.ExteriorCleaned)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving incomplete wash records: {ex.Message}", ex);
        }
    }

    public async Task AddWashRecordAsync(WashRecord washRecord)
    {
        try
        {
            if (washRecord == null)
                throw new ArgumentNullException(nameof(washRecord), "Wash record cannot be null");

            await _context.WashRecords.AddAsync(washRecord);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding wash record: {ex.Message}", ex);
        }
    }

    public async Task UpdateWashRecordAsync(WashRecord washRecord)
    {
        try
        {
            if (washRecord == null)
                throw new ArgumentNullException(nameof(washRecord), "Wash record cannot be null");

            _context.WashRecords.Update(washRecord);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating wash record with ID {washRecord.Id}: {ex.Message}", ex);
        }
    }

    public async Task DeleteWashRecordAsync(int id)
    {
        try
        {
            var washRecord = await _context.WashRecords.FindAsync(id);
            if (washRecord == null)
                throw new KeyNotFoundException($"Wash record with ID {id} not found.");

            _context.WashRecords.Remove(washRecord);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting wash record with ID {id}: {ex.Message}", ex);
        }
    }
    public async Task<IEnumerable<WashRecord>> GetAllWashRecordsWithCarAndOrgAsync()
    {
        return await _context.WashRecords
            .Include(w => w.Car)
                .ThenInclude(c => c.Organization)
            .ToListAsync();
    }

}
