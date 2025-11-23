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

        /// <summary>
        /// Build menu tree structure from flat data
        /// </summary>
        private List<MenuItemViewModel> BuildMenuTreeStructure(List<MenuItemViewModel> flatMenuList)
        {
            if (flatMenuList == null || !flatMenuList.Any())
                return new List<MenuItemViewModel>();

            // Create dictionary for fast lookup
            var menuDictionary = flatMenuList.ToDictionary(m => m.MenuID);
            var rootMenus = new List<MenuItemViewModel>();

            foreach (var menu in flatMenuList)
            {
                if (menu.ParentMenuID == null || menu.ParentMenuID == 0 ||
                    !menuDictionary.ContainsKey(menu.ParentMenuID.Value))
                {
                    // This is a root menu
                    rootMenus.Add(menu);
                }
                else
                {
                    // This is a child - add to parent's children list
                    var parent = menuDictionary[menu.ParentMenuID.Value];
                    menu.MenuLevel = parent.MenuLevel + 1;
                    parent.ChildrenMenu.Add(menu);
                }
            }

            // Sort recursively
            SortMenuTreeRecursive(rootMenus);

            _logger.LogInformation($"Built tree structure with {rootMenus.Count} root menus");

            return rootMenus;
        }

        /// <summary>
        /// Recursive function to sort menu tree
        /// </summary>
        private void SortMenuTreeRecursive(List<MenuItemViewModel> menus)
        {
            if (menus == null || !menus.Any())
                return;

            menus.Sort((a, b) => a.DisplayOrder.CompareTo(b.DisplayOrder));

            foreach (var menu in menus)
            {
                if (menu.ChildrenMenu != null && menu.ChildrenMenu.Any())
                {
                    SortMenuTreeRecursive(menu.ChildrenMenu);
                }
            }
        }

        /// <summary>
        /// Get user's menu tree (for AJAX calls or API)
        /// </summary>
        [HttpGet]
        public IActionResult GetUserMenuTree()
        {
            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");

                if (string.IsNullOrEmpty(userIdString))
                {
                    return Json(new { success = false, message = "User not logged in" });
                }

                decimal userId = decimal.Parse(userIdString);

                var rawMenuData = _menuService.GetMenuHierarchyByUserId(userId);
                var flatMenuList = _menuService.ConvertToMenuItemList(rawMenuData);
                var menuTree = BuildMenuTreeStructure(flatMenuList);

                return Json(new
                {
                    success = true,
                    userId = userId,
                    totalItems = flatMenuList.Count,
                    rootMenus = menuTree.Count,
                    data = menuTree
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting user menu tree: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Render menu tree as partial view (for Layout)
        /// </summary>
        [HttpGet]
        public IActionResult GetMenuPartial()
        {
            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");
                decimal userId = string.IsNullOrEmpty(userIdString) ? 0 : decimal.Parse(userIdString);

                var rawMenuData = _menuService.GetMenuHierarchyByUserId(userId);
                var flatMenuList = _menuService.ConvertToMenuItemList(rawMenuData);
                var menuTree = BuildMenuTreeStructure(flatMenuList);

                return PartialView("_MenuTreePartial", menuTree);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error rendering menu partial: {ex.Message}");
                return PartialView("_MenuTreePartial", new List<MenuItemViewModel>());
            }
        }

        /// <summary>
        /// TEST: Display menu tree in a test page
        /// </summary>
        public IActionResult TestMenu()
        {
            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");
                decimal userId = string.IsNullOrEmpty(userIdString) ? 0 : decimal.Parse(userIdString);

                var rawMenuData = _menuService.GetMenuHierarchyByUserId(userId);
                var flatMenuList = _menuService.ConvertToMenuItemList(rawMenuData);
                var menuTree = BuildMenuTreeStructure(flatMenuList);

                ViewBag.UserId = userId;
                ViewBag.TotalItems = flatMenuList.Count;
                ViewBag.RootMenus = menuTree.Count;

                return View(menuTree);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in TestMenu: {ex.Message}");
                ViewBag.Error = ex.Message;
                return View(new List<MenuItemViewModel>());
            }
        }

        /// <summary>
        /// TEST: Get raw menu data (for debugging)
        /// </summary>
        [HttpGet]
        public IActionResult GetRawMenuData()
        {
            try
            {
                var userIdString = HttpContext.Session.GetString("UserId");
                decimal userId = string.IsNullOrEmpty(userIdString) ? 0 : decimal.Parse(userIdString);

                var rawData = _menuService.GetMenuHierarchyByUserId(userId);

                return Json(new
                {
                    success = true,
                    userId = userId,
                    totalRecords = rawData.Count,
                    menuCount = rawData.Count(x => x.ISMenu),
                    actionCount = rawData.Count(x => !x.ISMenu),
                    data = rawData
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting raw menu data: {ex.Message}");
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}