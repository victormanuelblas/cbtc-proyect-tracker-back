using Tracker.Application.DTOs.Customer;

namespace Tracker.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int customerId);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task<CustomerDto> UpdateCustomerAsync(int customerId, UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomerAsync(int customerId);
    }
}