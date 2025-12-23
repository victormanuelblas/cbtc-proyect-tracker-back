using System.ComponentModel.DataAnnotations;

namespace Tracker.Application.DTOs.Shipment
{
    public class UpdateShipmentDto
    {
        [Required]
        public int ShipmentId { get; set; }
        [Required]
        [StringLength(50)]
        public string TrackingNumber { get; set; } = null!;
        [Required]
        [StringLength(200)]
        public string Description { get; set; } = null!;
        [Required]
        [StringLength(150)]
        public string Destination { get; set; } = null!;
    
    }
}