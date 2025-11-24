using Domain.Cards;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Users
{
    public  interface IUserRepo
    {
        Task<User> AddAsync(User User);
        void Delete(long UserId);
        Task UpdatePersonalInfoAsync(string Name, string FamilyName,  long UserId);
        Task UpdateContactInfoAsync(string Email, string PhoneNumber, long UserId);
        Task UpdateAccountInfoAsync(string UserName, string Password,long UserId);
        Task<User> GetUserByIdAsync(long id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> AddRangeCardAsync(long UserId, List<Card> cards);
        Task<User> LoginAsync(string UserName,string Password);
    }
}
