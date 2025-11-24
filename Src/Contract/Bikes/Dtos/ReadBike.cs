using Domain.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bikes.Dtos;

public record ReadBike(long BikeId, string Modle, double PricePerHour, string Location, DateTime LastCheckUp, BikeStatus Status);

