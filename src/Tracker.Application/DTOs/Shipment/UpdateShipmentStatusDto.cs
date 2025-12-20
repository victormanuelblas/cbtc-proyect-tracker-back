namespace Tracker.Application.DTOs.Shipment
{
    public class UpdateShipmentStatusDto
    {
        public int ShipmentId { get; set; }
        public string ShipmentStatus { get; set; } = null!;
        public string? ReceivedBy { get; set; }
        public DateTime? ReceivedAt { get; set; }
    }
}