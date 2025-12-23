using Tracker.Application.DTOs.Shipment;
using Tracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Tracker.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;


namespace Tracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipment([FromBody] CreateShipmentDto createShipmentDto)
        {
            var shipmentDto = await _shipmentService.CreateShipmentAsync(createShipmentDto);
            return CreatedAtAction(nameof(GetShipmentById), new { shipmentId = shipmentDto.ShipmentId }, shipmentDto);
        }

        [HttpGet("{shipmentId}")]
        public async Task<IActionResult> GetShipmentById(int shipmentId)
        {
            var shipmentDto = await _shipmentService.GetShipmentByIdAsync(shipmentId);
            return Ok(shipmentDto);
        }

        [HttpPut("{shipmentId}")]
        public async Task<IActionResult> UpdateShipment(int shipmentId, [FromBody] UpdateShipmentDto updateShipmentDto)
        {
            var shipmentDto = await _shipmentService.UpdateShipmentAsync(shipmentId, updateShipmentDto);
            return Ok(shipmentDto);
        }

        [HttpDelete("{shipmentId}")]
        public async Task<IActionResult> DeleteShipment(int shipmentId)
        {
            await _shipmentService.DeleteShipmentAsync(shipmentId);
            return Ok(new
            {
                message = "Shipment deleted successfully"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShipments()
        {
            var shipments = await _shipmentService.GetAllShipmentsAsync();
            return Ok(shipments);
        }
        [HttpPatch("{shipmentId}/status")]
        public async Task<IActionResult> UpdateShipmentStatus(int shipmentId, [FromBody] UpdateShipmentStatusDto updateShipmentStatusDto)
        {
            var shipmentDto = await _shipmentService.UpdateShipmentStatusAsync(shipmentId, updateShipmentStatusDto);
            return Ok(shipmentDto);
        }
    }
}
