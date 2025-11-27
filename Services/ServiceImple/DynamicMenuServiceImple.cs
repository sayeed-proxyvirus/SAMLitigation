using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models.ApplicationDbContext;
using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Services.ServiceImple
{
    public class DynamicMenuServiceImple : DynamicMenuService
    {
        private readonly SAMDbContext _context;
        private readonly ILogger<DynamicMenuServiceImple> _logger;

        public DynamicMenuServiceImple(SAMDbContext context, ILogger<DynamicMenuServiceImple> logger)
        {
            _context = context;
            _logger = logger;
        }

        public HierarchicalMenuViewModel GetUserMenuHierarchy(decimal userId)
        {
            try
            {
                // Step 1: Get root menu IDs for this user from SP1
                var rootMenuIds = GetUserRootMenus(userId);

                if (!rootMenuIds.Any())
                {
                    _logger.LogWarning($"No root menus found for user {userId}");
                    return new HierarchicalMenuViewModel { RootItems = new List<MenuItemViewModel>() };
                }

                // Step 2: Get all menu relationships from SP2
                var allMenuItems = GetAllMenuRelationships();

                if (!allMenuItems.Any())
                {
                    _logger.LogWarning("No menu items found in SP2");
                    return new HierarchicalMenuViewModel { RootItems = new List<MenuItemViewModel>() };
                }

                // Step 3: Build hierarchical structure
                var hierarchy = BuildMenuHierarchy(rootMenuIds, allMenuItems);

                return new HierarchicalMenuViewModel { RootItems = hierarchy };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error building menu hierarchy for user {userId}");
                throw;
            }
        }

        public List<MenuItemViewModel> GetUserMenuFlat(decimal userId)
        {
            var hierarchy = GetUserMenuHierarchy(userId);
            return FlattenHierarchy(hierarchy.RootItems);
        }

        /// <summary>
        /// SP1: Gets root menu IDs for a specific user
        /// </summary>
        private List<decimal> GetUserRootMenus(decimal userId)
        {
            try
            {
                userId = 4;
                var param = new SqlParameter("@UserId", userId);

                // Assuming SP1 returns MenuID column
                var rootMenus = _context.Set<UserMenuRootViewModel>()
                    .FromSqlRaw("EXEC SP1_GetUserRootMenus @UserId", param)
                    .ToList();

                return rootMenus.Select(m => m.MenuID).Distinct().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching root menus for user {userId}");
                throw;
            }
        }

        /// <summary>
        /// SP2: Gets all menu relationships (parent-child mappings)
        /// </summary>
        private List<MenuItemViewModel> GetAllMenuRelationships()
        {
            try
            {
                // This calls your existing SP2 that returns all menu relationships
                var menuItems = _context.MenuItems
                    .FromSqlRaw("EXEC GETALL")
                    .ToList();

                return menuItems.Select(m => new MenuItemViewModel
                {
                    ParentID = m.ParentID,
                    ParentCode = m.ParentCode,
                    ChildID = m.ChildID,
                    ChildCode = m.ChildCode,
                    DisplayName = m.DisplayName,
                    ApplicationName = m.ApplicationName,
                    SeqNo = m.SeqNo,
                    IsMenu = m.IsMenu,
                    IsDisplayable = m.IsDisplayable
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all menu relationships");
                throw;
            }
        }

        /// <summary>
        /// Builds hierarchical menu structure starting from root menu IDs
        /// </summary>
        private List<MenuItemViewModel> BuildMenuHierarchy(List<decimal> rootMenuIds, List<MenuItemViewModel> allMenuItems)
        {
            // Filter to remove self-referencing items
            allMenuItems = allMenuItems.Where(x => x.ParentID != x.ChildID).ToList();

            // Build lookup: ParentID -> List of Children
            var childrenLookup = allMenuItems
                .GroupBy(x => x.ParentID)
                .ToDictionary(g => g.Key, g => g.OrderBy(x => x.SeqNo).ToList());

            var rootItems = new List<MenuItemViewModel>();

            // For each root menu ID from SP1
            foreach (var rootMenuId in rootMenuIds.Distinct())
            {
                // Find all menu items where ParentID matches this root menu ID
                if (childrenLookup.ContainsKey(rootMenuId))
                {
                    var firstLevelItems = childrenLookup[rootMenuId];

                    foreach (var item in firstLevelItems)
                    {
                        item.Level = 1;

                        // Recursively build children
                        BuildChildrenRecursive(item, childrenLookup, 1, new HashSet<decimal> { item.ChildID });

                        rootItems.Add(item);
                    }
                }
            }

            return rootItems.OrderBy(x => x.SeqNo).ToList();
        }

        /// <summary>
        /// Recursively builds children for a menu item
        /// </summary>
        private void BuildChildrenRecursive(
            MenuItemViewModel parent,
            Dictionary<decimal, List<MenuItemViewModel>> childrenLookup,
            int currentLevel,
            HashSet<decimal> visitedInPath)
        {
            // Check if this parent's ChildID has any children
            if (!childrenLookup.ContainsKey(parent.ChildID))
            {
                return; // No children, exit recursion
            }

            var children = childrenLookup[parent.ChildID];

            foreach (var child in children)
            {
                // Prevent infinite loops - skip if already visited in current path
                if (visitedInPath.Contains(child.ChildID))
                {
                    _logger.LogWarning($"Circular reference detected: ChildID {child.ChildID} already in path");
                    continue;
                }

                // Skip self-referencing items
                if (child.ParentID == child.ChildID)
                {
                    continue;
                }

                child.Level = currentLevel + 1;
                parent.Children.Add(child);

                // Create new path for this branch to track visited nodes
                var newPath = new HashSet<decimal>(visitedInPath) { child.ChildID };

                // Recursively build this child's children
                BuildChildrenRecursive(child, childrenLookup, currentLevel + 1, newPath);
            }
        }

        /// <summary>
        /// Flattens hierarchical menu into a single list
        /// </summary>
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