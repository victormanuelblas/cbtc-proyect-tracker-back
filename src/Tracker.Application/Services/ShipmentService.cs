using AutoMapper;
using Tracker.Domain.Entities;
using Tracker.Domain.Ports.Out;
using Tracker.Domain.Exceptions;
using Tracker.Application.DTOs.Shipment;
using Tracker.Application.Interfaces;

namespace Tracker.Application.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShipmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShipmentDto> CreateShipmentAsync(CreateShipmentDto createShipmentDto)
        {
            var shipment = _mapper.Map<Shipment>(createShipmentDto);
            await _unitOfWork.ShipmentsRepository.SaveShipmentAsync(shipment);
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<ShipmentDto>(shipment);
        }
        public async Task<ShipmentDto> GetShipmentByIdAsync(int shipmentId)
        {
            var shipment = await _unitOfWork.ShipmentsRepository.GetShipmentByIdAsync(shipmentId);
            if (shipment == null)
            {
                throw new NotFoundException(shipmentId, "Shipment");
            }
            return _mapper.Map<ShipmentDto>(shipment);
        }
        public async Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync()
        {
            var shipments = await _unitOfWork.ShipmentsRepository.GetAllShipmentsAsync();
            return _mapper.Map<IEnumerable<ShipmentDto>>(shipments);
        }
        public async Task<ShipmentDto> UpdateShipmentAsync(int shipmentId, UpdateShipmentDto updateShipmentDto)
        {
            var shipment = await _unitOfWork.ShipmentsRepository.GetShipmentByIdAsync(shipmentId);
            if (shipment == null)
            {
                throw new NotFoundException(shipmentId, "Shipment");
            }
            _mapper.Map(updateShipmentDto, shipment);
            await _unitOfWork.ShipmentsRepository.UpdateShipmentAsync(shipment);
            return _mapper.Map<ShipmentDto>(shipment);
        }
        public async Task<bool> DeleteShipmentAsync(int shipmentId)
        {
            var shipment = await _unitOfWork.ShipmentsRepository.GetShipmentByIdAsync(shipmentId);
            if (shipment == null)
            {
                throw new NotFoundException(shipmentId, "Shipment");
            }
            await _unitOfWork.ShipmentsRepository.DeleteShipmentAsync(shipment);
            return true;
        }
        public async Task<ShipmentDto> UpdateShipmentStatusAsync(int shipmentId, UpdateShipmentStatusDto updateShipmentStatusDto)
        {
            var shipment = await _unitOfWork.ShipmentsRepository.GetShipmentByIdAsync(shipmentId);
            if (shipment == null)
            {
                throw new NotFoundException(shipmentId, "Shipment");
            }
            _mapper.Map(updateShipmentStatusDto, shipment);
            await _unitOfWork.ShipmentsRepository.UpdateShipmentAsync(shipment);
            return _mapper.Map<ShipmentDto>(shipment);
        }
    }
}
