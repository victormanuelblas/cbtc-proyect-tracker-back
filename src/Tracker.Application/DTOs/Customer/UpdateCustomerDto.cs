using System.ComponentModel.DataAnnotations;

namespace Tracker.Application.DTOs.Customer
{
    public class UpdateCustomerDto
    {
        public string Name { get; set; }
        public string DocmNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CustomerTypeId { get; set; }
    }
}