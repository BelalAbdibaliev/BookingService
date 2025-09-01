namespace BS.Domain.Entities;

public class Resource: BaseEntity
{
    private readonly List<Spot> _spots = new();
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public string Location { get; set; }
    public bool IsActive { get; set; }
    
    public IReadOnlyCollection<Spot> Spots => _spots.AsReadOnly();

    private Resource() {}

    public Resource(string name, string description, int capacity, string location, bool isActive)
    {
        Name = name;
        Description = description;
        Capacity = capacity;
        Location = location;
        IsActive = isActive;
    }

    public Spot AddSpot(string number, decimal price, int capacity)
    {
        if (_spots.Count >= Capacity)
            throw new InvalidOperationException("Reached maximum number of spots");

        if (int.TryParse(number, out var num) && num > Capacity)
            throw new ArgumentException("Number of spot is greater than maximum number of spots");

        var spot = new Spot(number, price, Id, capacity);
        _spots.Add(spot);
        
        spot.IsActive = true;

        return spot;
    }

}