using OptiWash.Models.DTOs;
using OptiWash.Models.Enums;

namespace OptiWash.Services.IServices
{
    public interface IWashRecordService
    {
        Task<WashRecord> GetWashRecordByIdAsync(int id);
        Task<IEnumerable<WashRecordDto>> GetAllWashRecordsForCarAsync(int carId);
        Task<IEnumerable<WashRecordSimpleDto>> GetAllWashRecordsAsync();

        Task<IEnumerable<WashRecord>> GetIncompleteWashRecordsAsync();
        Task<IEnumerable<WashRecord>> GetWashRecordsByStatusAsync(WashStatus status);

        Task AddWashRecordAsync(WashRecordDto washRecordDto);
        Task UpdateWashRecordAsync(WashRecord washRecord);
        Task DeleteWashRecordAsync(int id);
    }
}
