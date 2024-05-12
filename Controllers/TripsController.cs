using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie7.Data; // Dodaj odniesienie do kontekstu bazy danych
using Zadanie7.Models;

namespace Zadanie7.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TripsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
    {
        var trips = await _context.Trip
            .OrderByDescending(t => t.DateFrom)
            .ToListAsync(); 

        return Ok(trips);
    }
}