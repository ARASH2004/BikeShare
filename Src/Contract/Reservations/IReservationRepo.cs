using Domain.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Reservations
{
    public interface IReservationRepo
    {
        Task<Reservation> AddAsync(Reservation reservation);
        void Update(Reservation reservation);
        Task<IEnumerable<Reservation>> GetAllForUserAsync(long UserId);
        Task<Reservation> GetByIdAsync(long ResId);
        void Delete(long ResId);

    }
}
