using Contract.Reservations;
using Domain.Reservations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.PersistantEf.Repos.Reservations
{
    public class ReservationRepo : IReservationRepo
    {
        private readonly BikeRentContext _BikeRentContext;

        public ReservationRepo(BikeRentContext BikeRentContext)
        {
            _BikeRentContext = BikeRentContext;
        }
        public async Task<Reservation> AddAsync(Reservation reservation)
        {
          var Res = await _BikeRentContext.Reservations.AddAsync(reservation);
            _BikeRentContext.SaveChanges();
           return Res.Entity;
        }

        public void Delete(long ResId)
        {
           var Res = _BikeRentContext.Reservations.FirstOrDefault(r => r.Id == ResId);
            Res.Delete();
            _BikeRentContext.Reservations.Update(Res);
        }

        public async Task<IEnumerable<Reservation>> GetAllForUserAsync(long UserId)
        {
            return await _BikeRentContext.Reservations
                .Include(r=>r.Bike)
                .Where(r=>(
                (r.UserId == UserId) &&
                (!r.IsDeleted) &&
                (!r.Bike.IsDeleted))
                ).ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(long ResId)
        {
            var Res = await _BikeRentContext.Reservations
                .Include(r=>r.Card)
                .Include(r=>r.Bike)
                .Where(r=>
                (!r.IsDeleted)
                ).FirstOrDefaultAsync(r=>r.Id == ResId);
            return Res;
        }

        public void  Update(Reservation reservation)
        {
           _BikeRentContext.Reservations.Update(reservation);
           _BikeRentContext.SaveChanges();
        }
    }
}
