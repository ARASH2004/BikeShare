using Application.Bikes.Dtos;

namespace BikeShareApi.Dtos.Reservations.Responce
{
    public record AddReservationResponse(long ResId,long CardId,long BikeId);
   
}
