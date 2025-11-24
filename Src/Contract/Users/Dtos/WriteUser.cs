using Application.Cards.Dtos;
namespace Application.Users.Dtos;

public record WriteUser(string UserName, string Password,
    string PhoneNumber, string Email, string FirstName, string FamilyName, List<WriteCard> WriteCards);

