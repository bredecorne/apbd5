using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .Include(t => t.IdCountries)
            .OrderByDescending(t => t.DateFrom)
            .ToListAsync();

        return Ok(trips);
    }
    
    [HttpPost("{idTrip:int}/clients")]

    public async Task<ActionResult> CreateTripClientRelationship(int idTrip, string firstName, string lastName, string email, string telephone, string pesel, DateTime paymentDate)
    {
        var trip = await _context.Trips.FindAsync(idTrip);
        if (trip == null) return NotFound();

        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == pesel);

        if (client == null)
        {
            client = new Client(firstName, lastName, email, telephone, pesel);
            _context.Clients.Add(client);
        }
        
        await _context.SaveChangesAsync();

        var clientTrip = new ClientTrip(client.IdClient, idTrip, DateTime.Now, paymentDate);
        _context.ClientTrips.Add(clientTrip);
    
        await _context.SaveChangesAsync();

        return Ok();
    }

}