using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie7.Contexts;
using Zadanie7.Models;

namespace Zadanie7.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly Todo0Context _context;

    public TripsController(Todo0Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
    {
        var trips = await _context.Trips
            .OrderByDescending(t => t.DateFrom)
            .ToListAsync(); 

        return Ok(trips);
    }
}