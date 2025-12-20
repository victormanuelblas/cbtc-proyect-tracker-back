using Tracker.Domain.Entities;

namespace Tracker.Domain.Ports.Out
{
    public interface IShipmentRepository
    {
        Task<Shipment?> GetShipmentByIdAsync(int shipmentId);
        Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
        Task SaveShipmentAsync(Shipment shipment);
        Task UpdateShipmentAsync(Shipment shipment);
        Task DeleteShipmentAsync(Shipment shipment);
    }
}