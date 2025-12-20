namespace Tracker.Application.DTOs.Shipment
{
    public class UpdateShipmentDto
    {
        public int ShipmentId { get; set; }
        public string TrackingNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Destination { get; set; } = null!;
    
    }
}