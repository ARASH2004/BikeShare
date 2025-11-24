using Domain.Bikes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Bikes
{
    public interface IBikeRepo
    {
        Task<Bike> AddAsync(Bike bike);   
        void Update(Bike bike);
        void DeleteBike(long BikeId);
        Task<Bike> GetByIdAsync(long BikeId);
        Task<IEnumerable<Bike>> GetAllAsync();
        Task<bool> IsPassCorectAsync(long BikeId,Guid Password);
        Task ChangeStatusToActiveAsync(long BikeId, string location);
        Task ChangeStatusToUnActiveAsync(long BikeId);
        Task ChangeStatusToUnderMaintanceAsync(long BikeId);
    }
}
