using System.ComponentModel.DataAnnotations;

namespace Tracker.Application.DTOs.Customer;

public class CreateCustomerDto
{
	[Required]
	[StringLength(100, MinimumLength = 2)]
	public string Name { get; set; } = null!;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = null!;

	[Required]
	public int CustomerTypeId { get; set; }
}

