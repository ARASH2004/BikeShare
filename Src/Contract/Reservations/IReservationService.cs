using Application.Cards.Dtos;
using Application.Reservations.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Reservations
{
    public interface IReservationService
    {
       Task<ReadReservation> AddReservationAsync(WriteReservation WriteReservation);
        Task EndReservationAsync(long Resid,string Location);
        double GetTotalPrice(long Resid);
        Task<ReadReservation> GetByIdAsync(long Resid);
        Task<ReadCard> GetCardAsync(long Resid);
        Task<IEnumerable<ReadReservation>> GetAllUserReservationsAsync(long UserId);
    }
}
