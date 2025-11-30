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
            var userID = HttpContext.Session.GetString("userId");
            if (!string.IsNullOrEmpty(userID)) 
            {
                _logger.LogInformation($"UserID is needed!!!!!");
            }
            try
            {
                userId = decimal.Parse(userID);
                _logger.LogInformation($"GetUserMenu called for userId: {userId}");
                var menu = _dynamicMenuService.GetUserMenuHierarchy(userId);
                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving menu for user {userId}");
                return StatusCode(500, new { error = "An error occurred while retrieving the menu" });
            }
        }

        /// <summary>
        /// Returns rendered HTML for the menu using partial views
        /// This is what gets called from the layout AJAX
        /// </summary>
        [HttpGet]
        public IActionResult GetMenuHtml(decimal userId)
        {
            var userID = HttpContext.Session.GetString("userId");
            if (!string.IsNullOrEmpty(userID))
            {
                _logger.LogInformation($"UserID is needed!!!!!");
            }
            try
            {
                userId = decimal.Parse(userID);
                _logger.LogInformation($"GetMenuHtml called for userId: {userId}");

                var menuHierarchy = _dynamicMenuService.GetUserMenuHierarchy(userId);

                if (menuHierarchy?.RootItems == null || !menuHierarchy.RootItems.Any())
                {
                    _logger.LogWarning($"No menu items found for user {userId}");
                    return PartialView("_EmptyMenu");
                }

                _logger.LogInformation($"Returning {menuHierarchy.RootItems.Count} root menu items");

                // Return the partial view with the root items
                return PartialView("_MenuItemPartial", menuHierarchy.RootItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GetMenuHtml for user {userId}");
                return PartialView("_ErrorMenu");
            }
        }

        [HttpGet]
        public IActionResult GetUserMenuFlat(decimal userId)
        {
            var userID = HttpContext.Session.GetString("userId");
            if (!string.IsNullOrEmpty(userID))
            {
                _logger.LogInformation($"UserID is needed!!!!!");
            }
            try
            {
                userId = decimal.Parse(userID);
                var menuItems = _dynamicMenuService.GetUserMenuFlat(userId);
                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving flat menu for user {userId}");
                return StatusCode(500, new { error = "An error occurred while retrieving the flat menu" });
            }
        }

        /// <summary>
        /// Test endpoint - Remove after debugging
        /// </summary>
        [HttpGet]
        public IActionResult TestMenuData(decimal userId)
        {
            var userID = HttpContext.Session.GetString("userId");
            if (!string.IsNullOrEmpty(userID))
            {
                _logger.LogInformation($"UserID is needed!!!!!");
            }
            try
            {
                userId = decimal.Parse(userID);
                var menu = _dynamicMenuService.GetUserMenuHierarchy(userId);
                return Json(new
                {
                    success = true,
                    userId = userId,
                    rootItemsCount = menu.RootItems?.Count ?? 0,
                    rootItems = menu.RootItems
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }


    }
}