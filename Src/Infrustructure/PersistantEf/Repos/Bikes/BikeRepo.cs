using Contract.Bikes;
using Domain.Bikes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.PersistantEf.Repos.Bikes
{
    public class BikeRepo : IBikeRepo
    {
        private readonly BikeRentContext _BikeRentContext;

        public BikeRepo(BikeRentContext BikeRentContext)
        {
            _BikeRentContext = BikeRentContext;
        }

        public async Task<Bike> Add(Bike bike)
        {
           var Bike = await _BikeRentContext.bikes.AddAsync(bike);
            _BikeRentContext.SaveChanges();
            return Bike.Entity;
        }

        public async Task ChangeStatusToActive(long BikeId, string Location)
        {
           var Bike = await _BikeRentContext.bikes.FirstOrDefaultAsync(b=>b.Id == BikeId);
            Bike.AddAvailableLocation(Location);
            Bike.MarkAsAvalable();
            _BikeRentContext.bikes.Update(Bike);
            _BikeRentContext.SaveChanges();
        }

        public async Task ChangeStatusToUnActive(long BikeId)
        {
            var Bike = await _BikeRentContext.bikes.FirstOrDefaultAsync(b => b.Id == BikeId);
            Bike.MarkAsUnAvalable();
            _BikeRentContext.bikes.Update(Bike);
            _BikeRentContext.SaveChanges();
        }

        public async Task ChangeStatusToUnderMaintance(long BikeId)
        {
            var Bike =await _BikeRentContext.bikes.FirstOrDefaultAsync(b => b.Id == BikeId);
            Bike.MarkAsUnderMaintance();
            _BikeRentContext.bikes.Update(Bike);
            _BikeRentContext.SaveChanges();
        }

        public void DeleteBike(long BikeId)
        {
         var Bike = _BikeRentContext.bikes.FirstOrDefault(b=>b.Id == BikeId);
         Bike.Delete();
         _BikeRentContext.bikes.Update(Bike);
         _BikeRentContext.SaveChanges();    
        }

        public async Task<List<Bike>> GetAll()
        {
            return await _BikeRentContext.bikes.
                Where(b=>!b.IsDeleted).ToListAsync();
        }

        public async Task<Bike> GetById(long BikeId)

        {
            Bike Bike = await _BikeRentContext.bikes.FirstOrDefaultAsync(b => b.Id == BikeId);

            return Bike;
        }

        public async Task<bool> IsPassCorect(long BikeId, Guid Password)
        {
         return await _BikeRentContext.bikes
                .Where(b => !b.IsDeleted)
                .AnyAsync(b=>b.Id==BikeId && b.PaswordCode == Password);

        }

        public void Update(Bike bike)
        {
            _BikeRentContext.bikes.Update(bike);
            _BikeRentContext.SaveChanges();
        }
    }
}
