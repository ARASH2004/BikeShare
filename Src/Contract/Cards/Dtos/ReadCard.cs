using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cards.Dtos
{
    public record ReadCard(long CardId,long userId,  string cardNumber, string cvv2, string Password,bool IsActive);



}
