using BS.Domain.Entities;

public class Booking : BaseEntity
{
    public int Id { get; private set; }

    public int SpotId { get; private set; }
    public Spot Spot { get; private set; }

    public string UserId { get; private set; }
    public User User { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public BookingStatus Status { get; private set; }

    private Booking() { }

    private Booking(Spot spot, string userId)
    {
        if (spot == null)
            throw new ArgumentNullException(nameof(spot));

        if (!spot.Resource.IsActive)
            throw new InvalidOperationException("Resource is not active.");

        SpotId = spot.Id;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        Status = BookingStatus.Active;
    }

    public static Booking Create(Spot spot, string userId)
    {
        return new Booking(spot, userId);
    }

    public void Cancel()
    {
        if (Status != BookingStatus.Active)
            throw new InvalidOperationException("Unable to Cancel Booking.");

        Status = BookingStatus.Canceled;
    }
}

public enum BookingStatus
{
    Active,
    Completed,
    Canceled,
    Pending
}