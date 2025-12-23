using System.ComponentModel.DataAnnotations;

namespace Tracker.Application.DTOs.Shipment
{
    public class CreateShipmentDto
    {
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "TrackingNumber is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "TrackingNumber must be between 3 and 50 characters")]
        public string TrackingNumber { get; set; } = null!;
        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Destination is required")]
        [StringLength(150, ErrorMessage = "Destination cannot exceed 150 characters")]
        public string Destination { get; set; } = null!;
    
    }
}