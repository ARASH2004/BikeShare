using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cards.Dtos
{
    public record WriteCard(long UserId, string CardNumber, string Cvv2, string Password);
    
        
    

}
