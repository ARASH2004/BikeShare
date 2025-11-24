using BikeShareApi.Dtos.Cards.Request;
namespace BikeShareApi.Dtos.Users.Request
{
    public record AddUserRequest(string UserName, string Password,
    string PhoneNumber, string Email, string FirstName, string FamilyName, List<AddCardRequest> AddCardRequests);
    
}
