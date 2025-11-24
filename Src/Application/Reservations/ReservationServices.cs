using Application.Reservations.Dtos;
using Domain.Reservations;
using Application.Bikes.Dtos;
using Contract.Bikes;
using Contract.Reservations;
using Domain.Shared.Enum;
using Application.Cards.Dtos;
using System.Threading.Tasks;
using Contract.Cards;

namespace Application.Reservations
{
    public class ReservationServices : IReservationService
    {
        private readonly IReservationRepo _ReservationRepo;
        private readonly IBikeRepo _BikeRepo;
        private readonly ICardRepo _CardRepo;

        public ReservationServices(IReservationRepo ReservationRepo,IBikeRepo BikeRepo,ICardRepo cardRepo)
        {
            _ReservationRepo = ReservationRepo;
            _BikeRepo = BikeRepo;
            this._CardRepo = cardRepo;
        }
        public async Task EndReservationAsync(long Resid,string Location)
        {
            var Res = await _ReservationRepo.GetByIdAsync(Resid);
            Res.EndReservation();
           await _BikeRepo.ChangeStatusToActiveAsync(Res.BikeId,Location);
            _ReservationRepo.Update(Res);
        }

        public async Task<IEnumerable<ReadReservation>> GetAllUserReservationsAsync(long UserId)
        {
            var UserReservations = await _ReservationRepo.GetAllForUserAsync(UserId);
            return    UserReservations.Select(u => new ReadReservation(
                    u.Id,
                    u.UserId,
                    u.CardId,        
                    u.BikeId,
                    u.Start,
                    u.End,
                    u.PricePerHour,
                ReadBikes: new ReadBike(
                    u.Bike.Id,
                    u.Bike.Modle,
                    u.Bike.PricePerHour,
                    u.Bike.AvalableAtLocation.Last()
                    , u.Bike.LastCheckUp,
                    u.Bike.Status)));



        }

        public async Task<ReadReservation> GetByIdAsync(long Resid)
        {
         var Res =await _ReservationRepo.GetByIdAsync(Resid);
            return new ReadReservation(
                Res.Id,
                Res.UserId,
                Res.CardId,
                Res.BikeId,
                Res.Start,
                Res.End,
                Res.PricePerHour,
                ReadBikes: new ReadBike(
                    Res.Bike.Id,
                    Res.Bike.Modle, 
                    Res.Bike.PricePerHour,
                    Res.Bike.AvalableAtLocation.Last(),
                    Res.Bike.LastCheckUp,
                    Res.Bike.Status));
        }

        public async Task<double> GetTotalPriceAsync(long Resid)
        {
            try
            {
                var Res = await _ReservationRepo.GetByIdAsync(Resid);
               var TotalPrice =  Res.CalculateCost();
                _ReservationRepo.Update(Res);
                return TotalPrice;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<ReadReservation> AddReservationAsync(WriteReservation WriteReservation)
        {
            var Bike = await _BikeRepo.GetByIdAsync(WriteReservation.BikeId);
            if (Bike == null)
            {
                throw new ArgumentNullException("Bike not found.");
            }
            if (Bike.Status == BikeStatus.Available)
            {
                var Res = new Reservation(
                    WriteReservation.UserId,
                    WriteReservation.CardId,
                    WriteReservation.BikeId, 
                    Bike.PricePerHour);

                Bike.MarkAsUnAvalable();
                Bike.GenerateNewPasswordCode();
                _BikeRepo.Update(Bike);
               var AddedRes =await _ReservationRepo.AddAsync(Res);
                return new ReadReservation
                    (
                    AddedRes.Id,
                    AddedRes.UserId,
                    AddedRes.CardId,
                    AddedRes.BikeId,
                    AddedRes.Start,
                    AddedRes.End,
                    AddedRes.PricePerHour,
                    new ReadBike
                       (
                        AddedRes.Bike.Id,
                        AddedRes.Bike.Modle,
                        AddedRes.Bike.PricePerHour,
                        AddedRes.Bike.AvalableAtLocation.Last(),
                        AddedRes.Bike.LastCheckUp,
                        AddedRes.Bike.Status
                       )
                    );
            }
            else
            {
                throw new ArgumentException("BikeIsntAvailable");
            }
        }

        public async Task<ReadCard> GetCardAsync(long Resid)
        {
            var Res = await _ReservationRepo.GetByIdAsync(Resid);
            if (Res != null)
            {
                 var Card =await _CardRepo.GetByIDAsync(Card.CardId);
                return new ReadCard(
                    Card.Id,
                    Card.UserId,
                    Card.CardNumber,
                    Card.Cvv2,
                    Card.Password,
                    Card.IsActive);
            }
        }
    }
}
