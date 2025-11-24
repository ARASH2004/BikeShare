using Application.Bikes.Dtos;
using BikeShareApi.Dtos.Bikes.Request;
using BikeShareApi.Dtos.Bikes.Responce;
using Contract.Bikes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BikeShareApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BikeController : ControllerBase
{
    private readonly IBikeService _BikeService;

    public BikeController(IBikeService BikeService)
    {
        _BikeService = BikeService;
    }
    [HttpPost]
    public async Task<ActionResult<AddBikeResponse>> AddBike([FromBody] AddBikeRequest Request)
    {
        var Bike = new WriteBike(Request.Modle, Request.PricePerHour,Request.Location);
        var AddedBike = await _BikeService.AddAsync(Bike);
        return Ok(new AddBikeResponse
            (
            AddedBike.BikeId,
            AddedBike.Modle,
            AddedBike.PricePerHour
            ));
    }
  
    [HttpPut("{Id}")]
    public async Task<ActionResult> UpdatePricePerHour(long Id, [FromBody] UpdateBikePricePerHourRequest Request)
    {
       await _BikeService.ChangePricePerHourAsync(Id, Request.PricePerHour);
        return Ok();
    }
    [HttpGet("{Id}")]
    public async Task<ActionResult<GetBikePasswordResponse>> GetBikePassword(long Id)
    {
        var BikePassword = await _BikeService.GetPasswordAsync(Id);
        return Ok(new GetBikePasswordResponse(Id, BikePassword));
    }
    [HttpGet("{Id}")]
    public async Task<ActionResult<bool>> CheckPassword(long Id, [FromBody] CheckBikePasswordRequest Request)
    {
        bool IsChecked =await _BikeService.CheckPasswordAsync(Id, Request.Password);
        if (IsChecked) {
            return Ok();
        }
        else
        {
            return Conflict();
        }
        
    }
    [HttpPut("{Id}")]
    public async Task<ActionResult> ChangeStatusToActive(long Id, [FromBody] ChangeBikeStatusToActiveRequest Request)
    {
       await _BikeService.ChangeStatusToActiveAsync(Id,Request.Location);
        return Ok();
    }
    [HttpPut("{Id}")]
    public async Task<ActionResult> ChangeStatusToUnActive(long Id)
    {
      await  _BikeService.ChangeStatusToUnActiveAsync(Id);
        return Ok();
    }
    [HttpPut("{Id}")]
    public async Task<ActionResult> ChangeStatusToUnderMaintance(long  Id)
    {
       await _BikeService.ChangeStatusToUnderMaintanceAsync(Id);
        return Ok();
    }
    [HttpDelete("{Id}")]
    public ActionResult Delete(long Id)
    {
        _BikeService.Delete(Id);
        return Ok();
    }
}