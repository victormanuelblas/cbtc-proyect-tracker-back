using Tracker.Application.DTOs.Shipment;
using Tracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Tracker.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Tracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        private readonly ILogger<ShipmentController> _logger;
        public ShipmentController(IShipmentService shipmentService, ILogger<ShipmentController> logger)
        {
            _shipmentService = shipmentService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShipment([FromBody] CreateShipmentDto createShipmentDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "POST /api/shipments requested by user {UserId}. CustomerId: {CustomerId}",
                userId ?? "anonymous",
                createShipmentDto.CustomerId);

            try
            {
                var shipmentDto = await _shipmentService.CreateShipmentAsync(createShipmentDto);
                _logger.LogInformation(
                       "Shipment {ShipmentId} created successfully by user {UserId}",
                       shipmentDto.ShipmentId,
                       userId ?? "anonymous");
                return CreatedAtAction(nameof(GetShipmentById), new { shipmentId = shipmentDto.ShipmentId }, shipmentDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Create shipment failed - {Entity} not found. User: {UserId}, CustomerId: {CustomerId}",
                    ex.EntityName,
                    userId ?? "anonymous",
                    createShipmentDto.CustomerId);

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating shipment. User: {UserId}, CustomerId: {CustomerId}",
                    userId ?? "anonymous",
                    createShipmentDto.CustomerId);

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("{shipmentId}")]
        public async Task<IActionResult> GetShipmentById(int shipmentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "GET /api/shipments/{ShipmentId} requested by user {UserId}",
                shipmentId,
                userId ?? "anonymous");

            try
            {
                var shipmentDto = await _shipmentService.GetShipmentByIdAsync(shipmentId);
                _logger.LogDebug(
                        "Shipment {ShipmentId} retrieved successfully by user {UserId}",
                        shipmentId,
                        userId ?? "anonymous");
                return Ok(shipmentDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Shipment {ShipmentId} not found. Requested by user {UserId}",
                    shipmentId,
                    userId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error retrieving shipment {ShipmentId}. User: {UserId}",
                    shipmentId,
                    userId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{shipmentId}")]
        public async Task<IActionResult> UpdateShipment(int shipmentId, [FromBody] UpdateShipmentDto updateShipmentDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "PUT /api/shipments/{ShipmentId} requested by user {UserId}",
                shipmentId,
                userId ?? "anonymous");

            try
            {
                var shipmentDto = await _shipmentService.UpdateShipmentAsync(shipmentId, updateShipmentDto);
                _logger.LogInformation(
                        "Shipment {ShipmentId} updated successfully by user {UserId}",
                        shipmentId,
                        userId ?? "anonymous");
                return Ok(shipmentDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Update failed - Shipment {ShipmentId} not found. User: {UserId}",
                    shipmentId,
                    userId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating shipment {ShipmentId}. User: {UserId}",
                    shipmentId,
                    userId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpDelete("{shipmentId}")]
        public async Task<IActionResult> DeleteShipment(int shipmentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "DELETE /api/shipments/{ShipmentId} requested by user {UserId}",
                shipmentId,
                userId ?? "anonymous");

            try
            {
                await _shipmentService.DeleteShipmentAsync(shipmentId);
                _logger.LogWarning(
                        "Shipment {ShipmentId} deleted by user {UserId}",
                        shipmentId,
                        userId ?? "anonymous");
                return Ok(new
                {
                    message = "Shipment deleted successfully"
                });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Delete failed - Shipment {ShipmentId} not found. User: {UserId}",
                    shipmentId,
                    userId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting shipment {ShipmentId}. User: {UserId}",
                    shipmentId,
                    userId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShipments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "GET /api/shipments requested by user {UserId}",
                userId ?? "anonymous");

            try
            {
                var shipments = await _shipmentService.GetAllShipmentsAsync();
                _logger.LogInformation(
                        "Retrieved {ShipmentCount} shipments for user {UserId}",
                        shipments.Count(),
                        userId ?? "anonymous");
                return Ok(shipments);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error retrieving all shipments. User: {UserId}",
                    userId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }
        [HttpPatch("{shipmentId}/status")]
        public async Task<IActionResult> UpdateShipmentStatus(int shipmentId, [FromBody] UpdateShipmentStatusDto updateShipmentStatusDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "PATCH /api/shipments/{ShipmentId}/status requested by user {UserId}. New status: {NewStatus}",
                shipmentId,
                userId ?? "anonymous",
                updateShipmentStatusDto.ShipmentStatus);

            try
            {
                var shipmentDto = await _shipmentService.UpdateShipmentStatusAsync(shipmentId, updateShipmentStatusDto);
                _logger.LogInformation(
                        "Shipment {ShipmentId} status updated to {NewStatus} by user {UserId}",
                        shipmentId,
                        updateShipmentStatusDto.ShipmentStatus,
                        userId ?? "anonymous");
                return Ok(shipmentDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Status update failed - Shipment {ShipmentId} not found. User: {UserId}",
                    shipmentId,
                    userId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating shipment {ShipmentId} status. User: {UserId}",
                    shipmentId,
                    userId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}