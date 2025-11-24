using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bikes.Dtos;

public record WriteBike(string Modle, double PricePerHour,string Location);
