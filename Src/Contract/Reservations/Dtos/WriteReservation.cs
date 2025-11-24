using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Dtos
{
     public record WriteReservation(long UserId, long BikeId,long CardId);
    
}
