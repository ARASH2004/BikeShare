using Application.Users.Dtos;
using Application.Users.Dtos.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Users
{
    public interface IUserServices
    {
        Task<ReadUser> AddAsync(WriteUser WriteUser);
         void Delete(long UserId);
        Task UpdatePersonalInfoAsync(UpdatePersonalInfo UpdatePersonalInfo);
        Task UpdateContactInfoAsync(UpdateContactInfo UpdateContactInfo);
        Task UpdateAccountInfoAsync(UpdateAccountInfo UpdateAccountInfo);
        Task<ReadUser> GetUserByIdAsync(long id);
        Task<IEnumerable<ReadUser>> GetAllUsersAsync();
        Task<ReadUser> LoginAsync(string UserName, string Password);
    }
}
