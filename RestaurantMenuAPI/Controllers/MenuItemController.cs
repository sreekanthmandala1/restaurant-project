using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantMenuAPI.Models;
using RestaurantMenuAPI.Services;
using System;
using System.Threading.Tasks;

namespace RestaurantMenuAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ILogger<MenuItemController> _logger;

        public MenuItemController(IMenuItemService menuItemService, ILogger<MenuItemController> logger)
        {
            _menuItemService = menuItemService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Fetching all menu items...");

            try
            {
                var items = await _menuItemService.GetAllAsync();

                if (items == null || !items.Any())
                {
                    _logger.LogWarning("No menu items found.");
                    return NoContent(); 
                }

                _logger.LogInformation("Successfully fetched menu items.", items);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving menu items.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error", Error = ex.Message });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching menu item with ID: {Id}", id);
            try
            {
                var item = await _menuItemService.GetByIdAsync(id);
                if (item == null)
                {
                    _logger.LogWarning("Menu item with ID {Id} not found.", id);
                    return NotFound();
                }
                _logger.LogInformation("Successfully fetched menu item: {@Item}", item);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching menu item with ID {Id}.", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("addOrUpdate")]
        public async Task<IActionResult> AddOrUpdate([FromBody] MenuItem menuItem)
        {
            _logger.LogInformation("Processing menu item: {@MenuItem}", menuItem);

            try
            {
                var savedItem = await _menuItemService.AddOrUpdateAsync(menuItem);
                _logger.LogInformation("Menu item processed successfully: {@SavedItem}", savedItem);
                return Ok(savedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing menu item: {@MenuItem}", menuItem);
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpPut]
        public async Task<IActionResult> Update(MenuItem menuItem)
        {
            _logger.LogInformation("Updating menu item: {@MenuItem}", menuItem);
            try
            {
                var updatedItem = await _menuItemService.UpdateAsync(menuItem);
                _logger.LogInformation("Menu item updated successfully: {@UpdatedItem}", updatedItem);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating menu item: {@MenuItem}", menuItem);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting menu item with ID: {Id}", id);
            try
            {
                var result = await _menuItemService.DeleteAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Menu item with ID {Id} not found for deletion.", id);
                    return NotFound();
                }
                _logger.LogInformation("Menu item with ID {Id} deleted successfully.", id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting menu item with ID {Id}.", id);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
