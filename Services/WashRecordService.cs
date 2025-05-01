using Microsoft.EntityFrameworkCore;
using OptiWash.Models.DTOs;
using OptiWash.Models.Enums;
using OptiWash.Repositories.IRepository;
using OptiWash.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptiWash.Services
{
    public class WashRecordService : IWashRecordService
    {
        private readonly IWashRecordRepository _washRecordRepository;
        private readonly ICarRepository _carRepository;
        public WashRecordService(IWashRecordRepository washRecordRepository,ICarRepository carRepository)
        {
            _washRecordRepository = washRecordRepository;
            _carRepository = carRepository;
        }

        public async Task AddWashRecordAsync(WashRecordDto washRecordDto)
        {
            try
            {
                var car = await _carRepository.GetCarByIdAsync(washRecordDto.CarId);

                if (car == null)
                {
                    throw new ArgumentException("Invalid CarId.");
                }
                var newWashRecord = new WashRecord() 
                { 
                    CarId = car.Id,
                    WashDate = DateTime.Now,
                    InteriorCleaned = washRecordDto.InteriorCleaned,
                    ExteriorCleaned= washRecordDto.InteriorCleaned,
                    Notes = washRecordDto.Notes,
                    Status = washRecordDto.Status
                };
                await _washRecordRepository.AddWashRecordAsync(newWashRecord);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding wash record: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<WashRecordDto>> GetAllWashRecordsForCarAsync(int carId)
        {
            try
            {
                var washRecords = await _washRecordRepository.GetAllWashRecordsForCarAsync(carId);

                return washRecords.Select(wr => new WashRecordDto
                {
                    Id = wr.Id,
                    CarId = wr.CarId,
                    WashDate = wr.WashDate,
                    InteriorCleaned = wr.InteriorCleaned,
                    ExteriorCleaned = wr.ExteriorCleaned,
                    Status = wr.Status,
                    Notes = wr.Notes
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving wash records for car with ID {carId}: {ex.Message}", ex);
            }
        }
        public async Task<IEnumerable<WashRecordSimpleDto>> GetAllWashRecordsAsync()
        {
            var washRecords = await _washRecordRepository.GetAllWashRecordsAsync();

            return washRecords.Select(wr => new WashRecordSimpleDto
            {
                Id = wr.Id,
                //CarId = wr.CarId,
                CarPlateNumber = wr.Car?.PlateNumber, 
                WashDate = wr.WashDate,
                InteriorCleaned = wr.InteriorCleaned,
                ExteriorCleaned = wr.ExteriorCleaned,
                Status = wr.Status,
                Notes = wr.Notes
            });
        }
    

        public async Task<IEnumerable<WashRecord>> GetWashRecordsByStatusAsync(WashStatus status)
        {
            try
            {
                var allRecords = await _washRecordRepository.GetWashRecordsWithCarAndOrgAsync();
                return allRecords.Where(w => w.Status == status)
                    
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving records with status {status}: {ex.Message}", ex);
            }
        }


        public async Task<IEnumerable<WashRecord>> GetIncompleteWashRecordsAsync()
        {
            try
            {
                return await _washRecordRepository.GetIncompleteWashRecordsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving incomplete wash records: {ex.Message}", ex);
            }
        }

        public async Task<WashRecord> GetWashRecordByIdAsync(int id)
        {
            try
            {
                return await _washRecordRepository.GetWashRecordByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving wash record with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task UpdateWashRecordAsync(WashRecord washRecord)
        {
            try
            {
                await _washRecordRepository.UpdateWashRecordAsync(washRecord);
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
                await _washRecordRepository.DeleteWashRecordAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting wash record with ID {id}: {ex.Message}", ex);
            }
        }
        public async Task<IEnumerable<WashRecord>> GetAllWashRecordsWithCarAndOrgAsync()
        {
            return await _washRecordRepository.GetAllWashRecordsWithCarAndOrgAsync();
        }


    }

}
