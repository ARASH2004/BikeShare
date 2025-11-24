using Application.Cards.Dtos;

namespace Application.Users.Dtos;
public  record ReadUser(long userId,string UserName, string FirstName, string FamilyName, string Password, 
    string PhoneNumber, string Email,List<ReadCard> ReadCards);
    
