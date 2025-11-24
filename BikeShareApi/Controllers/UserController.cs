using Application.Cards.Dtos;
using Application.Users.Dtos;
using Application.Users.Dtos.Update;
using BikeShareApi.Dtos.Cards.Responce;
using BikeShareApi.Dtos.Users.Request;
using BikeShareApi.Dtos.Users.Responce;
using Contract.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BikeShareApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _UserServices;
        

        public UserController(IUserServices UserServices)
        {
            this._UserServices = UserServices;
         
        }
        [HttpPost]
        public async Task<ActionResult<AddUserResponse>> RegisterUser([FromBody] AddUserRequest request)
        {
            try
            {
                var cards = request.AddCardRequests
                    .Select(card => new WriteCard(card.UserId, card.CardNumber, card.Cvv2, card.Password))
                    .ToList();

                var user = new WriteUser(
                    UserName: request.UserName,
                    Password: request.Password,
                    PhoneNumber: request.PhoneNumber,
                    Email: request.Email,
                    FirstName: request.FirstName,
                    FamilyName: request.FamilyName,
                    WriteCards: cards);

                var savedUser =await _UserServices.AddAsync(user);

                return Ok(new AddUserResponse(
                    savedUser.userId,
                    savedUser.UserName,
                    savedUser.FirstName,
                    savedUser.FamilyName,
                    savedUser.PhoneNumber,
                    savedUser.Email,
                    savedUser.ReadCards.Select(cr => new AddCardResponse(
                        cr.CardId,
                        cr.userId,
                        cr.cardNumber,
                        cr.cvv2,
                        cr.Password
                    )).ToList()
                ));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<AddUserResponse>> AddUser([FromBody] AddUserRequest Request)
        {
            try
            {
                var cards = Request.AddCardRequests.
                    Select(Card => new WriteCard(Card.UserId, Card.CardNumber, Card.Cvv2, Card.Password)
                          ).ToList();

                var User = new WriteUser(
                   UserName: Request.UserName,
                   Password: Request.Password,
                   PhoneNumber: Request.PhoneNumber,
                   Email: Request.Email,
                    FirstName: Request.FirstName, 
                    FamilyName: Request.FamilyName,
                    WriteCards: cards);
               var SavedUser =await _UserServices.AddAsync(User);
               
                return Ok( new AddUserResponse
                    (
                    SavedUser.userId,
                    SavedUser.UserName,
                    SavedUser.FirstName,
                    SavedUser.FamilyName,
                    SavedUser.PhoneNumber,
                    SavedUser.Email,
                    SavedUser.ReadCards.Select(CR=>new AddCardResponse
                    (
                        CR.CardId,
                        CR.userId,
                        CR.cardNumber,
                        CR.cvv2,
                        CR.Password
                    ) ).ToList()
                    ));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
   

        [HttpDelete("{Id}")]
        public ActionResult<bool> DeleteUser(long Id)
        {
            _UserServices.Delete(Id);
          return Ok();
        }
        [HttpPut ("{Id}")]
        public async Task<ActionResult> UpdateAcountInfo(long Id,[FromBody] UpdateUserAccountRequest Request)
        {
            var AccountInfo = new UpdateAccountInfo(
                Request.UserName,
                Request.Password,
                Id);

           await _UserServices.UpdateAccountInfoAsync(AccountInfo);
            return Ok(AccountInfo);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdatePersonalInfo(long Id,[FromBody]UpdateUserPersonalInfoRequest Request) 
        {
            var PersonalInfo = new UpdatePersonalInfo(
                Id,
                Request.Name,
                Request.FamilyName);

           await _UserServices.UpdatePersonalInfoAsync(PersonalInfo);
            return Ok(PersonalInfo);
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateContactInfo(long Id,[FromBody]UpdateUserContactInfoRequest Request) 
        {
            var ContactInfo = new UpdateContactInfo(
                Id,
                Request.Email,
                Request.PhoneNumber);

           await _UserServices.UpdateContactInfoAsync(ContactInfo);
            return Ok(ContactInfo);
        }
    }
}
