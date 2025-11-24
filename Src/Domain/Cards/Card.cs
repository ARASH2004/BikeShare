using Domain.Reservations;
using Domain.Shared;
using Domain.Users;


namespace Domain.Cards
{
    public class Card:BaseEntity
    {
        public long UserId { get;private set; }
        public User User { get; private set; }
        public string CardNumber { get; private set; }
        public string Cvv2 { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }
        public List<Reservation> Reservations { get; private set; }

        public Card(long UserId , string CardNumber , string Cvv2 ,string Pasword)
        {
            IsActive = false;
            this.UserId = UserId;
            this.CardNumber = CardNumber;
            this.Cvv2 = Cvv2 ;
            Password = Pasword;
        }
        private Card()
        {
            
        }
        public void SetActive()
        {
            IsActive = true;
        }
        public void Deactive()
        {
            IsActive = false;
        }
    }
}
