using Application.Bikes.Dtos;
using Contract.Bikes;
using Domain.Bikes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bikes
{
    public class BikeServices : IBikeService
    {
        private readonly IBikeRepo _BikeRepo;
      
        public BikeServices(IBikeRepo BikeRepo)
        {
         this._BikeRepo = BikeRepo;   
        }

        public async Task<ReadBike> AddAsync(WriteBike WriteBike)
        {
            var Bike = new Bike(WriteBike.Modle,WriteBike.PricePerHour,WriteBike.Location);
           var AddedBike = await _BikeRepo.AddAsync(Bike);
            return new ReadBike
                (
                AddedBike.Id,
                AddedBike.Modle,
                AddedBike.PricePerHour,
                AddedBike.AvalableAtLocation.Last(),
                AddedBike.LastCheckUp,
                AddedBike.Status
               );
        }

        public async Task ChangePricePerHourAsync(long BikeId, double Price)
        {
            var Bike =await _BikeRepo.GetByIdAsync(BikeId);
         Bike.ChangePricePerHour(Price);
            _BikeRepo.Update(Bike);
        }


     

        public async Task ChangeStatusToUnderMaintanceAsync(long BikeId)
        {
         await   _BikeRepo.ChangeStatusToUnderMaintanceAsync(BikeId);
        }

      

        public async Task<Guid> GetPasswordAsync(long BikeId)
        {
            var Bike = await _BikeRepo.GetByIdAsync(BikeId);
            return Bike.PaswordCode;
        }

        public async Task<IEnumerable<ReadBike>> GetAllAsync()
        {
            var AllBikes = await _BikeRepo.GetAllAsync();
            return AllBikes.Select(b => new ReadBike
            (
                b.Id,
                b.Modle,
                b.PricePerHour,
                b.AvalableAtLocation.Last(),
                b.LastCheckUp,b.Status)
            ).ToList();
        }

        public async Task<ReadBike> GetByIdAsync(long Id)
        {
            var bike=await _BikeRepo.GetByIdAsync(Id);
            return new ReadBike
                (
                bike.Id, 
                bike.Modle, 
                bike.PricePerHour,
                bike.AvalableAtLocation.Last(),
                bike.LastCheckUp,
                bike.Status
                );
        }

        public async Task Delete(long BikeId)
        {
            var Bike =await _BikeRepo.GetByIdAsync(BikeId);
            Bike.Delete();
            _BikeRepo.Update(Bike);
        }

        public async Task<bool> CheckPasswordAsync(long BikeId, Guid Password)
        {
            return await _BikeRepo.IsPassCorectAsync(BikeId, Password);
        }

        public async Task ChangeStatusToActiveAsync(long BikeId, string Location)
        {
            await _BikeRepo.ChangeStatusToActiveAsync(BikeId, Location);
        }

        public async Task ChangeStatusToUnActiveAsync(long BikeId)
        {
            await _BikeRepo.ChangeStatusToUnActiveAsync(BikeId);
        }
    }
}
