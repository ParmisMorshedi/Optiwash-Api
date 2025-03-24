namespace OptiWash.Repositories.IRepository
{
    public interface IWashRecordRepository
    {
        Task<WashRecord> GetWashRecordByIdAsync(int id);
        Task<IEnumerable<WashRecord>> GetAllWashRecordsForCarAsync(int carId);
        Task<IEnumerable<WashRecord>> GetIncompleteWashRecordsAsync();
        Task AddWashRecordAsync(WashRecord washRecord);
        Task UpdateWashRecordAsync(WashRecord washRecord);
        Task DeleteWashRecordAsync(int id);
    }
}
