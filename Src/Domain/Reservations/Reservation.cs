using Domain.Bikes;
using Domain.Cards;
using Domain.Shared;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Reservations;

public class Reservation:BaseEntity
{
    public long UserId { get; private set; }
    public User User { get; private set; }
    public long BikeId { get; private set; }
    public Bike Bike { get; private set; }
    public DateTime Start { get; private set; }
    public DateTime? End { get; private set; }
    public double PricePerHour { get; private set; }
    public long CardId { get; private set; }
    public Card Card { get; private set; }
    public Reservation(long CardId,long UserId,long BikeId,double PricePerHour)
    {
        this.CardId = CardId;
        this.BikeId = BikeId;
        this.UserId = UserId;
        this.PricePerHour = PricePerHour;
        Start = CreatedDate;
    }
    public void EndReservation()
    {
        End = DateTime.Now;
    }
    public double CalculateCost() 
    {
        if (End.Value < Start)
            {
                throw new InvalidOperationException("End time cannot be earlier than start time.");
            }
        if (End is null)
            {
                throw new ArgumentNullException(nameof(End), "You must stop it first.");
            }
        return (End.Value - Start).TotalHours * this.PricePerHour;
    }
}