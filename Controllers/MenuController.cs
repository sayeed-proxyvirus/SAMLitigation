// Controllers/MenuController.cs
using Microsoft.AspNetCore.Mvc;
using SAMLitigation.Models.ViewModel;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class MenuController : Controller
    {
        private readonly MenuItemService _menuService;
        private readonly ILogger<MenuController> _logger;

        public MenuController(MenuItemService menuService, ILogger<MenuController> logger)
        {
            _menuService = menuService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetHierarchicalMenu()
        {
            try
            {
                var menu = _menuService.GetHierarchicalMenu();
                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving hierarchical menu");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        [HttpGet]
        public IActionResult GetFlatMenu()
        {
            try
            {
                var menu = _menuService.GetHierarchicalMenu(); 
                var flatList = FlattenHierarchy(menu.RootItems);
                return Ok(flatList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving flat menu");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        public IActionResult GetMenuByLevel(int level)
        {
            try
            {
                var menu = _menuService.GetHierarchicalMenu();
                var flatList = FlattenHierarchy(menu.RootItems);
                var filteredItems = flatList.Where(x => x.Level == level).ToList();
                return Ok(filteredItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving menu for level {level}");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
        private List<MenuItemViewModel> FlattenHierarchy(List<MenuItemViewModel> items)
        {
            var result = new List<MenuItemViewModel>();
            foreach (var item in items)
            {
                result.Add(item);
                if (item.Children.Any())
                {
                    result.AddRange(FlattenHierarchy(item.Children));
                }
            }
            return result;
        }
    }
}