using Application.Cards.Dtos;
using Domain.Cards;
using Contract.Cards;

namespace Application.Cards
{
    public class CardServices : ICardService
    {
        private readonly ICardRepo _CardRepo;

        public CardServices(ICardRepo CardRepo)
        {
            _CardRepo = CardRepo;
        }

        public ReadCard Add(WriteCard WriteCard)
        {
            var Card = new Card(WriteCard.UserId, WriteCard.CardNumber, WriteCard.Cvv2, WriteCard.Password);
           var AddedCard = _CardRepo.Add(Card);
            return new ReadCard
                 (
                 AddedCard.Id,
                 AddedCard.UserId,
                 AddedCard.CardNumber,
                 AddedCard.Cvv2,
                 AddedCard.Password,
                 AddedCard.IsActive
                 );
        }

        public void Delete(long CardId)
        {
            _CardRepo.Delete(CardId);
        }

        public List<ReadCard> GetAllUserCard(long UserId)
        {
            return _CardRepo.GetAllUserCard(UserId)
                .Select(ReadCard => new ReadCard(ReadCard.Id,ReadCard.UserId,ReadCard.CardNumber,ReadCard.Cvv2,ReadCard.Password,ReadCard.IsActive)).ToList();
        }

        public ReadCard GetByID(long CardId)
        {
          var Card = _CardRepo.GetByID(CardId);
            return new ReadCard(Card.Id,Card.UserId,Card.CardNumber, Card.Cvv2, Card.Password,Card.IsActive);
                
        }

        public ReadCard GetUserActiveCard(long UserId)
        {
            var Card = _CardRepo.GetUserActiveCard(UserId);
            return new ReadCard(Card.Id,Card.UserId, Card.CardNumber, Card.Cvv2, Card.Password, Card.IsActive);
        }

        public void SetDefault(long CardId)
        {
             _CardRepo.SetActive(CardId);
        
        }
    }
}
