using OptiWash.Models.Enums;

namespace OptiWash.Repositories.IRepository
{
    public interface IWashRecordRepository
    {
        Task<WashRecord> GetWashRecordByIdAsync(int id);
        Task<IEnumerable<WashRecord>> GetAllWashRecordsForCarAsync(int carId);
        
        Task<IEnumerable<WashRecord>> GetAllWashRecordsAsync();
        Task<IEnumerable<WashRecord>> GetIncompleteWashRecordsAsync();
        Task<IEnumerable<WashRecord>> GetByStatusAsync(WashStatus status);
        Task<IEnumerable<WashRecord>> GetAllWashRecordsWithCarAndOrgAsync();

        Task AddWashRecordAsync(WashRecord washRecord);
        Task UpdateWashRecordAsync(WashRecord washRecord);
        Task DeleteWashRecordAsync(int id);
    }
}
