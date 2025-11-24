using Contract.Users;
using Domain.Cards;
using Domain.Users;
using Microsoft.EntityFrameworkCore;


namespace Infrustructure.PersistantEf.Repos.Users
{
    public class UserRepo : IUserRepo
    {
        private readonly BikeRentContext _BikeRentContext;

        public UserRepo( BikeRentContext BikeRentContext)
        {
            _BikeRentContext = BikeRentContext;
        }

        public async Task<User> AddAsync(User User)
        {
           var AddEdUser = await _BikeRentContext.Users.AddAsync(User);
            _BikeRentContext.SaveChanges();
            return   AddEdUser.Entity;
        }

        public  async Task<User> AddRangeCardAsync(long UserId, List<Card> cards)
        {
            var User = await _BikeRentContext.Users
                .Include(u=>u.Cards)
                .Where(u => !u.IsDeleted && u.Cards.All(c => !c.IsDeleted))
                .FirstOrDefaultAsync(u=>u.Id == UserId);
            cards.First().SetActive();
           
            User.SaveCard(cards);
            _BikeRentContext.SaveChanges();
            return User;
        }

        public void Delete(long UserId)
        {
            var User = _BikeRentContext.Users.FirstOrDefault(u=>u.Id == UserId);
           
            User.Delete();
            _BikeRentContext.Update(User);
            _BikeRentContext.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _BikeRentContext.Users
                         .Include(u => u.Cards)
                         .Where(u => !u.IsDeleted && u.Cards.All(c => !c.IsDeleted))
                         .ToListAsync();

        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _BikeRentContext.Users
                 .Include(u=>u.Cards)
                 .Where(u => !u.IsDeleted && u.Cards.All(c => !c.IsDeleted))
                 .FirstOrDefaultAsync(u=>u.Id == id);
        }

        public async Task UpdateAccountInfoAsync(string UserName, string Password, long UserId)
        {
            var User =await _BikeRentContext.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            User.UpdateAccountInfo(UserName, Password);
            _BikeRentContext.Users.Update(User);
            _BikeRentContext.SaveChanges();
        }

        public async Task UpdateContactInfoAsync(string Email, string PhoneNumber, long UserId)
        {
            var User =await _BikeRentContext.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            User.UpdateContactInfo(Email, PhoneNumber);
            _BikeRentContext.Users.Update(User);
            _BikeRentContext.SaveChanges();
        }

        public async Task UpdatePersonalInfoAsync(string Name, string FamilyName, long UserId)
        {
            var User =await _BikeRentContext.Users.FirstOrDefaultAsync(u=>u.Id == UserId);
            User.UpdatePersonalInfo(Name, FamilyName);
            _BikeRentContext.Users.Update(User);
            _BikeRentContext.SaveChanges();
        }

        public async Task<User> LoginAsync(string UserName, string Password)
        {
            return await _BikeRentContext.Users.FirstOrDefaultAsync(u=>u.UserName==UserName && u.Password == Password);
        }
    }
}
