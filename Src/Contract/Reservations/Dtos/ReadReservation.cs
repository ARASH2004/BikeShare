using Application.Bikes.Dtos;


namespace Application.Reservations.Dtos
{
    public record ReadReservation(long ResId,long UserId, long CardId,long BikeId, DateTime Start, DateTime? End,  double PricePerHour,
        ReadBike ReadBikes);
}
