using OptiWash.Models.DTOs;

namespace OptiWash.Services.IServices
{
    public interface IWashRecordService
    {
        Task<WashRecord> GetWashRecordByIdAsync(int id);
        Task<IEnumerable<WashRecordDto>> GetAllWashRecordsForCarAsync(int carId);
        Task<IEnumerable<WashRecord>> GetIncompleteWashRecordsAsync();
        Task AddWashRecordAsync(WashRecordDto washRecordDto);
        Task UpdateWashRecordAsync(WashRecord washRecord);
        Task DeleteWashRecordAsync(int id);
    }
}
