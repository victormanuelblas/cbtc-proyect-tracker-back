using AutoMapper;
using Tracker.Domain.Entities;
using Tracker.Domain.Ports.Out;
using Tracker.Domain.Exceptions;
using Tracker.Application.DTOs.Customer;
using Tracker.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Tracker.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CustomerService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto dto)
        {
            _logger.LogDebug(
                "Creating customer: Name: {CustomerName}, Email: {CustomerEmail}, TypeId: {CustomerTypeId}",
                dto.Name,
                dto.Email,
                dto.CustomerTypeId);
            try
            {
                if (dto.CustomerTypeId <= 0)
                {

                    _logger.LogWarning(
                            "Invalid CustomerTypeId {CustomerTypeId} when creating customer {CustomerName}",
                            dto.CustomerTypeId,
                            dto.Name);
                    throw new BusinessRuleException("CustomerType", "CustomerTypeId inválido");
                }
                var customer = new Customer(
                    dto.Name,
                    dto.DocmNumber,
                    dto.Email,
                    dto.Phone,
                    dto.CustomerTypeId
                );

                await _unitOfWork.CustomersRepository.SaveCustomerAsync(customer);
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation(
                                   "Customer {CustomerId} created successfully: {CustomerName} ({CustomerEmail})",
                                   customer.CustomerId,
                                   customer.Name,
                                   customer.Email);

                return _mapper.Map<CustomerDto>(customer);
            }
            catch (BusinessRuleException) { throw; }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating customer: Name: {CustomerName}, Email: {CustomerEmail}",
                    dto.Name,
                    dto.Email);
                throw;
            }
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int customerId)
        {
            _logger.LogDebug("Getting customer with ID {CustomerId}", customerId);
            try
            {
                var customer = await _unitOfWork.CustomersRepository.GetCustomerByIdAsync(customerId);
                if (customer == null)
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found", customerId);

                    throw new NotFoundException(customerId, "Customer");
                }
                _logger.LogDebug(
                       "Successfully retrieved customer {CustomerId}: {CustomerName}",
                       customerId,
                       customer.Name);
                return _mapper.Map<CustomerDto>(customer);
            }
            catch (NotFoundException){throw;}
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customer with ID {CustomerId}", customerId);
                throw;
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            _logger.LogDebug("Getting all customers");
            try
            {
                var customers = await _unitOfWork.CustomersRepository.GetAllCustomersAsync();
                _logger.LogInformation("Retrieved {CustomerCount} customers", customers.Count());

                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all customers");
                throw;
            }
        }

        public async Task<CustomerDto> UpdateCustomerAsync(int customerId, UpdateCustomerDto updateCustomerDto)
        {
            _logger.LogDebug("Updating customer with ID {CustomerId}", customerId);
            try
            {
                var customer = await _unitOfWork.CustomersRepository.GetCustomerByIdAsync(customerId);
                if (customer == null)
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found for update", customerId);

                    throw new NotFoundException(customerId, "Customer");
                }
                _logger.LogDebug(
                      "Updating customer {CustomerId}: Old Name={OldName}, New Name={NewName}",
                      customerId,
                      customer.Name,
                      updateCustomerDto.Name);
                _mapper.Map(updateCustomerDto, customer);
                await _unitOfWork.CustomersRepository.UpdateCustomerAsync(customer);
                _logger.LogInformation(
                        "Customer {CustomerId} updated successfully: {CustomerName} ({CustomerEmail})",
                        customerId,
                        customer.Name,
                        customer.Email);
                return _mapper.Map<CustomerDto>(customer);
            }
            catch (NotFoundException){throw;}
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer with ID {CustomerId}", customerId);
                throw;
            }
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            _logger.LogDebug("Attempting to delete customer with ID {CustomerId}", customerId);
            try
            {
                var customer = await _unitOfWork.CustomersRepository.GetCustomerByIdAsync(customerId);
                if (customer == null)
                {
                    _logger.LogWarning("Customer with ID {CustomerId} not found for deletion", customerId);

                    throw new NotFoundException(customerId, "Customer");
                }
                _logger.LogDebug(
                        "Deleting customer {CustomerId}: {CustomerName} ({CustomerEmail})",
                        customerId,
                        customer.Name,
                        customer.Email);
                await _unitOfWork.CustomersRepository.DeleteCustomerAsync(customer);
                _logger.LogInformation("Customer {CustomerId} deleted successfully", customerId);
                return true;
            }
            catch (NotFoundException){throw;}
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting customer with ID {CustomerId}", customerId);
                throw;
            }
        }
    }
}