using System.ComponentModel.DataAnnotations;

namespace Tracker.Application.DTOs.Shipment
{
    public class UpdateShipmentStatusDto
    {
        [Required]
        [Range(1, 5, ErrorMessage = "ShipmentStatus must be between 1 and 5")]
        public int ShipmentStatus { get; set; }
        [StringLength(100, ErrorMessage = "ReceivedBy cannot exceed 100 characters")]
        public string? ReceivedBy { get; set; }
        public DateTime? ReceivedAt { get; set; }
    }
}