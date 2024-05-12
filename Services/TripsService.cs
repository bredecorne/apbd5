using Microsoft.Data.SqlClient;
using Zadanie7.Models;

namespace Zadanie7.Services;

public class TripsService : ITripsService
{
    private readonly string _connection;
    
    public TripsService(IConfiguration configuration)
    {
        _connection = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException();
    }

    public List<Trip> ReadAll()
    {
        using var connection = new SqlConnection(_connection);
        using var command = new SqlCommand($"SELECT * FROM trip.Trip", connection);
        
        connection.Open();
        var data = command.ExecuteReader();
        
        Console.Write(data);

        return null;
    }
}