using Zadanie7.Models;

namespace Zadanie7.Services;

public interface ITripsService
{
    public List<Trip> ReadAll();
}