namespace BS.Domain.Entities;

public class Spot: BaseEntity
{
    public int Id { get; private set; }
    public string Number { get; private set; }
    public decimal Price { get; private set; } 
    public bool IsActive { get; set; }
    public int ResourceId { get; private set; }
    public int Capacity {get; private set;}
    public Resource Resource { get; private set; }

    private Spot() { }

    internal Spot(string number, decimal price, int resourceId,  int capacity)
    {
        Number = number;
        Price = price;
        ResourceId = resourceId;
        IsActive = true;
        Capacity = capacity;
    }

}