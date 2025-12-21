namespace Tracker.Application.DTOs.Shipment
{
    public class ShipmentDto
    {
        public int ShipmentId { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime ShippedAt { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public string ShipmentStatusDescription { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }

    }
}