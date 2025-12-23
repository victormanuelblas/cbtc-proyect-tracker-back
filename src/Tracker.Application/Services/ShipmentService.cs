using AutoMapper;
using Tracker.Domain.Entities;
using Tracker.Domain.Ports.Out;
using Tracker.Domain.Exceptions;
using Tracker.Application.DTOs.Shipment;
using Tracker.Application.Interfaces;
using Microsoft.Extensions.Logging;


namespace Tracker.Application.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ShipmentService> _logger;

        public ShipmentService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ShipmentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ShipmentDto> CreateShipmentAsync(CreateShipmentDto createShipmentDto)
        {
            try
            {
                // Validate related entities exist to avoid FK violations at DB level
                var customer = await _unitOfWork.CustomersRepository.GetCustomerByIdAsync(createShipmentDto.CustomerId);
                if (customer == null)
                {
                    _logger.LogWarning("Customer {CustomerId} not found when creating shipment", createShipmentDto.CustomerId);
                    throw new NotFoundException(createShipmentDto.CustomerId, "Customer");
                }

                var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(createShipmentDto.UserId);
                if (user == null)
                {
                    _logger.LogWarning("User {UserId} not found when creating shipment", createShipmentDto.UserId);
                    throw new NotFoundException(createShipmentDto.UserId, "User");
                }

                var shipment = _mapper.Map<Shipment>(createShipmentDto);
                // Ensure non-nullable DB column `received_by` gets a non-null value
                if (string.IsNullOrWhiteSpace(shipment.Receivedby))
                {
                    shipment.Receivedby = string.Empty;
                }

                await _unitOfWork.ShipmentsRepository.SaveShipmentAsync(shipment);
                await _unitOfWork.CommitTransactionAsync();
                _logger.LogInformation("Shipment {ShipmentId} created successfully", shipment.ShipmentId);
                return _mapper.Map<ShipmentDto>(shipment);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating shipment for CustomerId {CustomerId}", createShipmentDto.CustomerId);
                throw;
            }
        }
        public async Task<ShipmentDto> GetShipmentByIdAsync(int shipmentId)
        {
            _logger.LogDebug("Getting shipment with ID {ShipmentId}", shipmentId);
            var shipment = await _unitOfWork.ShipmentsRepository.GetShipmentByIdAsync(shipmentId);
            if (shipment == null)
            {
                _logger.LogWarning("Shipment with ID {ShipmentId} not found", shipmentId);
                throw new NotFoundException(shipmentId, "Shipment");
            }
            _logger.LogDebug("Successfully retrieved shipment with ID {ShipmentId}", shipmentId);
            return _mapper.Map<ShipmentDto>(shipment);
        }
        public async Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync()
        {
            _logger.LogDebug("Getting all shipments");
            try
            {
                var shipments = await _unitOfWork.ShipmentsRepository.GetAllShipmentsAsync();
                _logger.LogInformation("Retrieved {ShipmentCount} shipments", shipments.Count());
                return _mapper.Map<IEnumerable<ShipmentDto>>(shipments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all shipments");
                throw;
            }
        }
        public async Task<ShipmentDto> UpdateShipmentAsync(int shipmentId, UpdateShipmentDto updateShipmentDto)
        {
            _logger.LogDebug("Updating shipment with ID {ShipmentId}", shipmentId);
            try
            {
                var shipment = await _unitOfWork.ShipmentsRepository.GetShipmentByIdAsync(shipmentId);
                if (shipment == null)
                {
                    _logger.LogWarning("Shipment with ID {ShipmentId} not found for update", shipmentId);

                    throw new NotFoundException(shipmentId, "Shipment");
                }
                _logger.LogDebug("Mapping update data to shipment {ShipmentId}", shipmentId);

                _mapper.Map(updateShipmentDto, shipment);

                if (string.IsNullOrWhiteSpace(shipment.Receivedby))
                {
                    shipment.Receivedby = string.Empty;
                    _logger.LogDebug("Set empty Receivedby for shipment {ShipmentId}", shipmentId);

                }

                await _unitOfWork.ShipmentsRepository.UpdateShipmentAsync(shipment);
                _logger.LogInformation("Shipment {ShipmentId} updated successfully", shipmentId);

                return _mapper.Map<ShipmentDto>(shipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating shipment with ID {ShipmentId}", shipmentId);
                throw;
            }
        }

        public async Task<bool> DeleteShipmentAsync(int shipmentId)
        {
            _logger.LogDebug("Attempting to delete shipment with ID {ShipmentId}", shipmentId);

            var shipment = await _unitOfWork.ShipmentsRepository.GetShipmentByIdAsync(shipmentId);
            if (shipment == null)
            {
                _logger.LogWarning("Shipment with ID {ShipmentId} not found for deletion", shipmentId);

                throw new NotFoundException(shipmentId, "Shipment");
            }
            try
            {
                await _unitOfWork.ShipmentsRepository.DeleteShipmentAsync(shipment);
                _logger.LogInformation("Shipment {ShipmentId} deleted successfully", shipmentId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting shipment with ID {ShipmentId}", shipmentId);
                throw;
            }
        }
        public async Task<ShipmentDto> UpdateShipmentStatusAsync(int shipmentId, UpdateShipmentStatusDto updateShipmentStatusDto)
        {
            _logger.LogDebug("Updating status for shipment {ShipmentId}", shipmentId);
            try
            {
                var shipment = await _unitOfWork.ShipmentsRepository.GetShipmentByIdAsync(shipmentId);

                if (shipment == null)
                {
                    _logger.LogWarning("Shipment {ShipmentId} not found for status update", shipmentId);

                    throw new NotFoundException(shipmentId, "Shipment");
                }
                _logger.LogInformation(
                "Updating shipment {ShipmentId} status from {OldStatus} to {NewStatus}",
                shipmentId,
                shipment.ShipmentStatusId,
                updateShipmentStatusDto.ShipmentStatus);
                //_mapper.Map(updateShipmentStatusDto, shipment);
                shipment.ShipmentStatusId = updateShipmentStatusDto.ShipmentStatus;
                shipment.Receivedby = updateShipmentStatusDto.ReceivedBy;
                shipment.ReceivedAt = updateShipmentStatusDto.ReceivedAt;

                await _unitOfWork.ShipmentsRepository.UpdateShipmentAsync(shipment);
                _logger.LogInformation("Shipment {ShipmentId} status updated successfully", shipmentId);

                return _mapper.Map<ShipmentDto>(shipment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status for shipment {ShipmentId}", shipmentId);
                throw;
            }
        }
    }
}
