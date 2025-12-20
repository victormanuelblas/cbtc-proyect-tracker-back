using Tracker.Application.DTOs.Shipment;

namespace Tracker.Application.Interfaces
{
    public interface IShipmentService
    {
        Task<IEnumerable<ShipmentDto>> GetAllShipmentsAsync();
        Task<ShipmentDto> GetShipmentByIdAsync(int shipmentId);
        Task<ShipmentDto> CreateShipmentAsync(CreateShipmentDto createShipmentDto);
        Task<ShipmentDto> UpdateShipmentAsync(int shipmentId, UpdateShipmentDto updateShipmentDto);
        Task<bool> DeleteShipmentAsync(int shipmentId);
        Task<ShipmentDto> UpdateShipmentStatusAsync(int shipmentId, UpdateShipmentStatusDto updateShipmentStatusDto);
    }
}