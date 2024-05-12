using Microsoft.AspNetCore.Mvc;
using Zadanie7.Services;

namespace Zadanie7.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ITripsService _tripsService;

    public TripsController(ITripsService tripsService)
    {
        _tripsService = tripsService;
    }

    [HttpGet]
    public ActionResult GetTrips()
    {
        _tripsService.ReadAll();
        return Ok();
    }
}