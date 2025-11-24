using Domain.Cards;
using Domain.Reservations;
using Domain.Shared;
using Domain.Shared.Enum;


namespace Domain.Users
{
    public class User:BaseEntity
    {
        public string UserName { get;private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string FamilyName { get; private set; }
        public List<Card> Cards { get; private set; } = new();
        public List<Reservation> Reservations { get; private set; } = new();

        public User(string UserName,string Password,string FirstName, string FamilyName, string PhoneNumber, string Email)
        {
            Guard(FirstName,FamilyName, PhoneNumber,  Email);
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.FirstName = FirstName;
            this.FamilyName = FamilyName;
        }
        private static void Guard(string FirstName, string FamilyName, string PhoneNumber, string Email)
        {

            string[] fields = {FirstName,FamilyName,PhoneNumber,Email};
            if (fields.Any(string.IsNullOrWhiteSpace))
            {
                throw new ArgumentException("FirstName, FamilyName, PhoneNumber, and Email must not be empty or whitespace.");
            }
        }
        public   void UpdatePersonalInfo(string Name, string FamilyName)
        {
            this.FirstName = Name;
            this.FamilyName = FamilyName;
        }
        public void UpdateContactInfo(string Email, string PhoneNumber)
        {
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
        }
        public void UpdateAccountInfo(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }
        public void SaveCard(List<Card> cards)
        {
            Cards.AddRange(cards);
        }
    }
}
