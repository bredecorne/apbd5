using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie7.Contexts;

namespace Zadanie7.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly Todo0Context _context;

    public ClientsController(Todo0Context context)
    {
        _context = context;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var client = await _context.Clients.FindAsync(idClient);
        if (client == null)
        {
            return NotFound();
        }

        var hasTrips = await _context.ClientTrips
            .AnyAsync(ct => ct.IdClient == idClient);

        if (hasTrips)
        {
            return BadRequest();
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}