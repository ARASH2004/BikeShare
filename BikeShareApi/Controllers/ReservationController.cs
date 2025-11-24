using Application.Reservations.Dtos;
using BikeShareApi.Dtos.Reservations.Request;
using BikeShareApi.Dtos.Reservations.Responce;
using Contract.Reservations;
using Microsoft.AspNetCore.Mvc;

namespace BikeShareApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _ReservationService;

        public ReservationController(IReservationService ReservationService)
        {
            _ReservationService = ReservationService;
        }

        [HttpPost("{UserId}")]
        public ActionResult<AddReservationResponse> AddReservation(long UserId,[FromBody] AddReservationRequest Request)
        {
            var Res = new WriteReservation(UserId,Request.BikeId,Request.CardId);
            try
            {
                var AddedRes = _ReservationService.AddReservation(Res);
                return Ok(new AddReservationResponse
                    (
                    AddedRes.ResId,
                    AddedRes.CardId,
                    AddedRes.BikeId
                    ));
            }
            catch (ArgumentNullException ex)
            {
                return Conflict("Missing argument: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                return Conflict("Invalid argument: " + ex.Message);
            }


        }
        [HttpPut("{ResId}")]
        public ActionResult<ReservationEndResponse> EndReservation(long ResId, [FromBody] ReservationEndRequest Request)
        {
            try
            {
                _ReservationService.EndReservation(ResId,Request.Location);
                double TotalPrice = _ReservationService.GetTotalPrice(ResId);
                var Card = _ReservationService.GetCard(ResId);
                return Ok(new ReservationEndResponse(TotalPrice, Card));
            }
            catch(ArgumentException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
