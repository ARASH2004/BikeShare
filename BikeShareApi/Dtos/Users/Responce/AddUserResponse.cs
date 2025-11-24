using BikeShareApi.Dtos.Cards.Responce;
using BikeShareApi.Dtos.Shared.Enums;

namespace BikeShareApi.Dtos.Users.Responce
{
    public record AddUserResponse(long UserId, string UserName,string FirstName ,string FamilyName, string PhoneNumber, string Email
        ,List<AddCardResponse> AddCardResponses);
  
}
