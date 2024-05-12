namespace Zadanie7.Models;

public class Trip
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }

    public Trip(int id, string name, string description, DateTime dateFrom, DateTime dateTo, int maxPeople)
    {
        Id = id;
        Name = name;
        Description = description;
        DateFrom = dateFrom;
        DateTo = dateTo;
        MaxPeople = maxPeople;
    }
}