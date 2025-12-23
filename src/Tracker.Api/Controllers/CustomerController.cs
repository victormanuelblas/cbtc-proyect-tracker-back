using Tracker.Application.DTOs.Customer;
using Tracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Tracker.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Tracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "POST /api/customers requested by user {UserId}. Customer name: {CustomerName}",
                userId ?? "anonymous",
                createCustomerDto.Name);

            try
            {
                var customerDto = await _customerService.CreateCustomerAsync(createCustomerDto);

                _logger.LogInformation(
                       "Customer {CustomerId} created successfully by user {UserId}",
                       customerDto.CustomerId,
                       userId ?? "anonymous");
                return CreatedAtAction(nameof(GetCustomerById), new { customerId = customerDto.CustomerId }, customerDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Create customer failed - {Entity} not found. User: {UserId}, Customer name: {CustomerName}",
                    ex.EntityName,
                    userId ?? "anonymous",
                    createCustomerDto.Name);

                return NotFound(new { message = ex.Message });
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(
                    "Create customer failed - Domain validation error. User: {UserId}, Customer name: {CustomerName}, Error: {ErrorMessage}",
                    userId ?? "anonymous",
                    createCustomerDto.Name,
                    ex.Message);

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating customer. User: {UserId}, Customer name: {CustomerName}",
                    userId ?? "anonymous",
                    createCustomerDto.Name);

                return StatusCode(500, new { message = "Internal server error" });
            }
        }


        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "GET /api/customers/{CustomerId} requested by user {UserId}",
                customerId,
                userId ?? "anonymous");

            try
            {
                var customerDto = await _customerService.GetCustomerByIdAsync(customerId);
                _logger.LogDebug(
                        "Customer {CustomerId} retrieved successfully by user {UserId}",
                        customerId,
                        userId ?? "anonymous");
                return Ok(customerDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Customer {CustomerId} not found. Requested by user {UserId}",
                    customerId,
                    userId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error retrieving customer {CustomerId}. User: {UserId}",
                    customerId,
                    userId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] UpdateCustomerDto updateCustomerDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "PUT /api/customers/{CustomerId} requested by user {UserId}",
                customerId,
                userId ?? "anonymous");

            try
            {
                var customerDto = await _customerService.UpdateCustomerAsync(customerId, updateCustomerDto);
                _logger.LogInformation(
                         "Customer {CustomerId} updated successfully by user {UserId}",
                         customerId,
                         userId ?? "anonymous");
                return Ok(customerDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Update failed - Customer {CustomerId} not found. User: {UserId}",
                    customerId,
                    userId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(
                    "Update customer failed - Domain validation error. User: {UserId}, CustomerId: {CustomerId}, Error: {ErrorMessage}",
                    userId ?? "anonymous",
                    customerId,
                    ex.Message);

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating customer {CustomerId}. User: {UserId}",
                    customerId,
                    userId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "DELETE /api/customers/{CustomerId} requested by user {UserId}",
                customerId,
                userId ?? "anonymous");

            try
            {
                await _customerService.DeleteCustomerAsync(customerId);
                _logger.LogWarning(
                        "Customer {CustomerId} deleted by user {UserId}",
                        customerId,
                        userId ?? "anonymous");
                return Ok(new
                {
                    message = "Customer deleted successfully"
                });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Delete failed - Customer {CustomerId} not found. User: {UserId}",
                    customerId,
                    userId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting customer {CustomerId}. User: {UserId}",
                    customerId,
                    userId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "GET /api/customers requested by user {UserId}",
                userId ?? "anonymous");

            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                _logger.LogInformation(
                        "Retrieved {CustomerCount} customers for user {UserId}",
                        customers.Count(),
                        userId ?? "anonymous");
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error retrieving all customers. User: {UserId}",
                    userId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}