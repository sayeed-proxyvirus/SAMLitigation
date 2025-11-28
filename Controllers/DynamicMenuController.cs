using Microsoft.AspNetCore.Mvc;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class DynamicMenuController : Controller
    {
        private readonly DynamicMenuService _dynamicMenuService;
        private readonly ILogger<DynamicMenuController> _logger;

        public DynamicMenuController(DynamicMenuService dynamicMenuService, ILogger<DynamicMenuController> logger)
        {
            _dynamicMenuService = dynamicMenuService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetUserMenu(decimal userId)
        {
            try
            {
                var menu = _dynamicMenuService.GetUserMenuHierarchy(userId);
                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving menu for user {userId}");
                return StatusCode(500, new { error = "An error occurred while retrieving the menu" });
            }
        }

        [HttpGet]
        public IActionResult GetUserMenuFlat(decimal userId)
        {
            try
            {
                var menuItems = _dynamicMenuService.GetUserMenuFlat(userId);
                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving flat menu for user {userId}");
                return StatusCode(500, new { error = "An error occurred while retrieving the flat menu" });
            }
        }
    }
}