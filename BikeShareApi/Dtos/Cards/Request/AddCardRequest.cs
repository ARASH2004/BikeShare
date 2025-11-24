namespace BikeShareApi.Dtos.Cards.Request
{
    public record AddCardRequest(long UserId, string CardNumber, string Cvv2, string Password);
    
}
