using Application.Bikes.Dtos;


namespace Contract.Bikes
{
    public interface IBikeService
    {
        Task<ReadBike> AddAsync(WriteBike WriteBike);
        Task<bool> CheckPasswordAsync(long BikeId,Guid Password);
        Task ChangeStatusToActiveAsync(long BikeId, string Location);
        Task ChangeStatusToUnActiveAsync(long BikeId);
        Task ChangeStatusToUnderMaintanceAsync(long BikeId);
        Task Delete(long BikeId);
        Task<ReadBike> GetByIdAsync(long Id);
        Task<IEnumerable<ReadBike>> GetAllAsync();
        Task ChangePricePerHourAsync(long BikeId,double Price);
        Task<Guid> GetPasswordAsync(long BikeId);
    }
}
