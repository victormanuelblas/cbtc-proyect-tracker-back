namespace Tracker.Domain.Entities;

public class Shipment
{
    public int ShipmentId { get; set; }
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public string TrackingNumber { get; set; } = null!;
    public DateTime ShippedAt { get; set; } = DateTime.UtcNow;
    public string Description { get; set; } = null!;
    public string Destination { get; set; } = null!;
    public int ShipmentStatusId { get; set; } = 1;
    public string? Receivedby { get; set; } = null!;
    public DateTime? ReceivedAt { get; set; } = null;

    public Customer Customer { get; set; } = null!;
    public User User { get; set; } = null!; 
    public ShipmentStatus ShipmentStatus { get; set; } = null!;

    protected Shipment() { }

    public Shipment(
        int customerId,
        int userId,
        DateTime shippedAt,
        string trackingNumber,
        string description,
        string destination,
        int shipmentStatusId,
        string receivedby)
    {
        CustomerId = customerId;
        UserId = userId;
        TrackingNumber = trackingNumber;
        ShippedAt = shippedAt;
        Description = description;
        Destination = destination;
        ShipmentStatusId = shipmentStatusId;
        Receivedby = receivedby;
    }

    public Shipment(
        int customerId,
        int userId,
        string trackingNumber,
        string description,
        string destination
    )
    {
        CustomerId = customerId;
        UserId = userId;
        TrackingNumber = trackingNumber;
        Description = description;
        Destination = destination;
    }

    // public void MarkAsDelivered(DateTime receivedAt)
    // {
    //     ShipmentStatusId = 3; // ENTREGADO (luego lo refinamos)
    //     ReceivedAt = receivedAt;
    // }

    public void UpdateStatus(int statusId, string? receivedBy, DateTime? receivedAt)
    {
        ShipmentStatusId = statusId;

        if (statusId == 5) // Entregado
        {
            Receivedby = receivedBy;
            ReceivedAt = receivedAt ?? DateTime.UtcNow;
        }
        else
        {
            Receivedby = null;
            ReceivedAt = null;
        }
    }

}
