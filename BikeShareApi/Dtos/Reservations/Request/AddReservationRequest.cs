using Application.Bikes.Dtos;

namespace BikeShareApi.Dtos.Reservations.Request
{
    public record AddReservationRequest(long CardId,long BikeId);
   
}
