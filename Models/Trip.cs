namespace Zadanie7.Models;

public class Trip
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }

    public Trip(int idTrip, string name, string description, DateTime dateFrom, DateTime dateTo, int maxPeople)
    {
        IdTrip = idTrip;
        Name = name;
        Description = description;
        DateFrom = dateFrom;
        DateTo = dateTo;
        MaxPeople = maxPeople;
    }
}