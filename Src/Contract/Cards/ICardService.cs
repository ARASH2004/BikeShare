using Application.Cards.Dtos;
using Domain.Cards;


namespace Contract.Cards
{
    public interface ICardService
    {
       Task<ReadCard> AddAsync(WriteCard WriteCard);
        Task SetDefaultAsync(long CardId);
        void Delete(long CardId);
        Task<IEnumerable<ReadCard>> GetAllUserCardAsync(long UserId);
        Task<ReadCard> GetUserActiveCardAsync(long UserId);
        Task<ReadCard> GetByIDAsync(long CardId);
        

    }
}
