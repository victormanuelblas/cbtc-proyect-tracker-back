using AutoMapper;
using Tracker.Domain.Entities;
using Tracker.Domain.Ports.Out;
using Tracker.Domain.Exceptions;
using Tracker.Application.DTOs.Customer;
using Tracker.Application.Interfaces;

namespace Tracker.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            await _unitOfWork.CustomersRepository.SaveCustomerAsync(customer);
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomersRepository.GetCustomerByIdAsync(customerId);
            Console.WriteLine("Debug: Retrieved customer is " + (customer == null ? "null" : "not null"));
            if (customer == null)
            
            {
                Console.WriteLine($"Debug: Customer with ID {customerId} not found.");
                throw new NotFoundException(customerId, "Customer");
            }
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.CustomersRepository.GetAllCustomersAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> UpdateCustomerAsync(int customerId, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _unitOfWork.CustomersRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new NotFoundException(customerId, "Customer");
            }
            _mapper.Map(updateCustomerDto, customer);
            await _unitOfWork.CustomersRepository.UpdateCustomerAsync(customer);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var customer = await _unitOfWork.CustomersRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new NotFoundException(customerId, "Customer");
            }
            await _unitOfWork.CustomersRepository.DeleteCustomerAsync(customer);
            return true;
        }
    }
}