using Domain.Reservations;
using Domain.Shared;
using Domain.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Bikes
{
    public class Bike:BaseEntity
    {
        public string Modle { get; protected set; }
        public DateTime LastCheckUp { get; protected set; }
        public BikeStatus Status { get; protected set; }
        public Guid PaswordCode { get; protected set; }
        public double PricePerHour { get; protected set; }

        public List<Reservation> Reservations =new List<Reservation> { };
        public List<string> AvalableAtLocation = new List<string> { };
        private Bike()
        {
            
        }
        public Bike(string Modle, double PricePerHour, string Location)
        {
            this.PaswordCode=Guid.NewGuid();
            this.AvalableAtLocation.Add(Location);
            this.Modle = Modle;
            this.PricePerHour = PricePerHour;
            LastCheckUp = DateTime.Now;
            Status = BikeStatus.Available;
        }
        public void AddAvailableLocation(string Location)
        {
            AvalableAtLocation.Add(Location);
        }
        public  void GenerateNewPasswordCode()
        {
            PaswordCode = Guid.NewGuid();
        }

        public  void MarkAsMaintaceDone()
        {
            Status = BikeStatus.Available;
        }

        public void MarkAsUnAvalable()
        {
            Status = BikeStatus.Unavalable;
        }
        public void MarkAsAvalable()
        {
            Status = BikeStatus.Available;
        }
        public  void MarkAsUnderMaintance()
        {
            Status = BikeStatus.UnderMaintance;
            LastCheckUp = DateTime.Now;
        }
        public void ChangePricePerHour(double PricePerHour)
        {
            this.PricePerHour= PricePerHour;
        }
    }
}


