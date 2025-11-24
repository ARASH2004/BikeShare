namespace BikeShareApi.Dtos.Cards.Responce
{
    public record AddCardResponse(long CardId,long UserId, string CardNumber,string Cvv2,string Password);
   
}
