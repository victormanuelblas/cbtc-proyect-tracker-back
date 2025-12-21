namespace Tracker.Application.DTOs.Shipment
{
    public class UpdateShipmentStatusDto
    {
        public int ShipmentStatus { get; set; }
        public string? ReceivedBy { get; set; }
        public DateTime? ReceivedAt { get; set; }
    }
}