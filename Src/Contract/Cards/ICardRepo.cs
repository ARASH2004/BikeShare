using Domain.Cards;

namespace Contract.Cards
{
    public interface ICardRepo
    {
        Task<Card> AddAsync(Card Card);
        void Delete(long CardId);
        void Update(Card Card);
        Task<IEnumerable<Card>> GetAllUserCardAsync(long UserId);
        Task<Card> GetUserActiveCardAsync(long UserId);
        Task<Card> GetByIDAsync(long CardId);
        Task SetActiveAsync(long CardId);
        

    }
}
