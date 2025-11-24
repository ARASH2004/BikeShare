using Application.Cards.Dtos;
using BikeShareApi.Dtos.Cards.Request;
using BikeShareApi.Dtos.Cards.Responce;
using Contract.Cards;
using Microsoft.AspNetCore.Mvc;

namespace BikeShareApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _CardService;

        public CardController(ICardService CardService)
        {
            _CardService = CardService;
        }
        [HttpPost]
        public ActionResult<AddCardResponse> AddCard([FromBody] AddCardRequest Request)
        {
            var Card = new WriteCard(Request.UserId, Request.CardNumber, Request.Cvv2, Request.Password);
           var AddedCard = _CardService.Add(Card);
            return Ok(new AddCardResponse
                (
                AddedCard.CardId,
                AddedCard.userId,
                AddedCard.cardNumber,
                AddedCard.cvv2,
                AddedCard.Password
                ));   
        }
        [HttpPut("{Id}")]
        public ActionResult SetActive(long Id)
        {
            _CardService.SetDefault(Id);
            return Ok();
        }
        [HttpDelete]
        public ActionResult Delete(long Id)
        {
            _CardService.Delete(Id);
            return Ok();
        }

    }
}
