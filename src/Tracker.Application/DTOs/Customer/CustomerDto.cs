namespace Tracker.Application.DTOs.Customer
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string DocmNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CustomerTypeId { get; set; }
        public string CustomerTypeDescription { get; set; }
        
    }
}