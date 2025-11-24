using Application.Cards.Dtos;
using Application.Users.Dtos;
using Application.Users.Dtos.Update;
using Contract.Cards;
using Domain.Cards;
using Contract.Users;
using Domain.Users;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _UserRepo;
        private readonly ICardRepo _CardRepo;

        public UserServices(IUserRepo UserRepo,ICardRepo CardRepo)
        {
            _UserRepo = UserRepo;
            _CardRepo = CardRepo;
        }
        public async Task<ReadUser> AddAsync(WriteUser WriteUser)
        {
            try
            {
                var User = new User
                    (
                     UserName: WriteUser.UserName,
                     Password: WriteUser.Password,
                     FirstName: WriteUser.FirstName,
                     FamilyName: WriteUser.FamilyName,
                     PhoneNumber:WriteUser.PhoneNumber,
                     Email:WriteUser.Email
                    );
                var SavedUser =await _UserRepo.AddAsync(User);
                var Cards = WriteUser.WriteCards.Select(WriteCards => new Card(
                    SavedUser.Id,
                    WriteCards.CardNumber,
                    WriteCards.Cvv2,
                    WriteCards.Password)).ToList();

                var SavedUserWithCard =await _UserRepo.AddRangeCardAsync(SavedUser.Id, Cards);
                var ReadCard = SavedUserWithCard.Cards.Select(RC => new ReadCard
                (
                    RC.Id,
                    RC.UserId,
                    RC.CardNumber,
                    RC.Cvv2,
                    RC.Password,
                    RC.IsActive
                )).ToList();
                return new ReadUser
                    (
                     SavedUser.Id,
                     SavedUser.UserName,
                     SavedUser.Password,
                     SavedUser.PhoneNumber,
                     SavedUser.Email,
                     SavedUser.FirstName,
                     SavedUser.FamilyName,
                     ReadCard
                    );
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        public void Delete(long UserId)
        {
          _UserRepo.Delete(UserId);
          
        }

        public async Task<IEnumerable<ReadUser>> GetAllUsersAsync()
        {
           
            var users =await _UserRepo.GetAllUsersAsync();
            var readUsers = users.Select(user =>
            {
                var cards = user.Cards.Select(card => new ReadCard(
                    card.Id,
                    card.UserId,
                    card.CardNumber,
                    card.Cvv2,
                    card.Password,
                    card.IsActive
                )).ToList();

                return new ReadUser(
                    user.Id,
                    user.UserName,
                    user.Password,
                    user.PhoneNumber,
                    user.Email,
                    user.FirstName,
                    user.FamilyName,
                    ReadCards: cards
                );
            });

            return readUsers;
        }

        public async Task<ReadUser> GetUserByIdAsync(long id)
        {
            var User =await _UserRepo.GetUserByIdAsync(id);
            var Cards = User.Cards.Select(Card => new ReadCard(
                Card.Id,
                Card.UserId, 
                Card.CardNumber,
                Card.Cvv2, 
                Card.Password,
                Card.IsActive)).ToList();
            return new ReadUser(
                User.Id,
                User.UserName,
                User.Password,
                User.PhoneNumber,
                User.Email,
                User.FirstName,
                User.FamilyName,
                ReadCards:Cards);
        }

        public async Task<ReadUser> LoginAsync(string UserName,string Password)
        {
            var LogedinUser =await _UserRepo.LoginAsync(UserName, Password);
            if (LogedinUser != null)
            {
                return new ReadUser(
                                     LogedinUser.Id,
                                     LogedinUser.UserName,
                                     LogedinUser.FirstName,
                                     LogedinUser.FamilyName,
                                     LogedinUser.Password,
                                     LogedinUser.PhoneNumber,
                                     LogedinUser.Email,
                                     LogedinUser.Cards.Select(rc => new ReadCard
                                                                             (
                                                             rc.Id,
                                                             rc.UserId,
                                                             rc.CardNumber,
                                                             rc.Cvv2,
                                                             rc.Password,
                                                             rc.IsActive
                                                                             )).ToList()
                                     );
            }
            else
            {
                throw new ArgumentNullException("it wasnt exist");
            }
        }

        public async Task UpdateAccountInfoAsync(UpdateAccountInfo UpdateAccountInfo)
        {
         await   _UserRepo.UpdateAccountInfoAsync(UpdateAccountInfo.UserName, UpdateAccountInfo.Password,UpdateAccountInfo.UserId);
        }

        public async Task UpdateContactInfoAsync(UpdateContactInfo UpdateContactInfo)
        {
        await _UserRepo.UpdateContactInfoAsync(UpdateContactInfo.Email,UpdateContactInfo.PhoneNumber,UpdateContactInfo.UserId);
        }

        public async Task UpdatePersonalInfoAsync(UpdatePersonalInfo UpdatePersonalInfo)
        {
           await _UserRepo.UpdatePersonalInfoAsync(UpdatePersonalInfo.Name,UpdatePersonalInfo.FamilyName,UpdatePersonalInfo.UserId);
        }
    }
}
