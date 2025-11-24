using Domain.Cards;
using Contract.Cards;
using Microsoft.EntityFrameworkCore;
namespace Infrustructure.PersistantEf.Repos.Cards
{
    public class CardRepo : ICardRepo
    {
        private readonly BikeRentContext _BikeRentContext;

        public CardRepo(BikeRentContext BikeRentContext)
        {
            _BikeRentContext = BikeRentContext;
        }
        public async Task<Card> Add(Card Card)
        {
          var card= await _BikeRentContext.cards.AddAsync(Card);
            _BikeRentContext.SaveChanges();
            return card.Entity;
            
        }

        public async Task Delete(long CardId)
        {
           var Card = await _BikeRentContext.cards.FirstOrDefaultAsync(c=>c.Id == CardId);
           Card.Delete();
        }

        public async Task<List<Card>> GetAllUserCard(long UserId)
        {
            return await _BikeRentContext.cards
                .Where(c=>
                c.UserId == UserId &&
                !c.IsDeleted
                ).ToListAsync();
        }

        public async Task<Card> GetByID(long CardId)
        {
            var Card = await _BikeRentContext.cards
                .Where(c=>!c.IsDeleted)
                .FirstOrDefaultAsync(c => c.Id == CardId);
            return Card;
        }

        public Task<Card> GetUserActiveCard(long UserId)
        {
            return _BikeRentContext.cards
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(c => c.UserId == UserId && c.IsActive == true);
        }

        public async Task SetActive(long CardId)
        {
            var Card = await _BikeRentContext.cards.Where(c=>!c.IsDeleted).FirstOrDefaultAsync(c=>c.Id == CardId);
            if (!Card.IsActive)
            {
                Card.SetActive();
                _BikeRentContext.cards.Update(Card);
                var AllCards = _BikeRentContext.cards.Where(c=>c.Id == Card.UserId).ToList();
                AllCards.ForEach(x =>
                {
                    if (x.Id != Card.Id && x.IsActive)
                    {
                        x.Deactive();
                        _BikeRentContext.cards.Update(x);
                    }
                });
                _BikeRentContext.SaveChanges();
            }

        }

        public  void Update(Card Card)
        {
            _BikeRentContext.cards.Update(Card);
            _BikeRentContext.SaveChanges();
        }
    }
}
