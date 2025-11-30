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
                //root menu call
                var rootMenuIds = GetUserRootMenus(userId);
                if (!rootMenuIds.Any())
                {
                    _logger.LogWarning($"No root menus found for user {userId}");
                    return new HierarchicalMenuViewModel { RootItems = new List<MenuItemViewModel>() };
                }
                //all menu call func
                var allMenuItems = GetAllMenuRelationships();
                if (!allMenuItems.Any())
                {
                    _logger.LogWarning("No menu items found in SP2");
                    return new HierarchicalMenuViewModel { RootItems = new List<MenuItemViewModel>() };
                }
                //tree recur
                var hierarchy = BuildMenuHierarchy(rootMenuIds, allMenuItems);
                return new HierarchicalMenuViewModel { RootItems = hierarchy };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MenuItemViewModel> GetUserMenuFlat(decimal userId)
        {
            var hierarchy = GetUserMenuHierarchy(userId);
            return FlattenHierarchy(hierarchy.RootItems);
        }
        private List<decimal> GetUserRootMenus(decimal userId)
        {
            try
            {

                var param = new SqlParameter("@UserId", userId);
                //first rootmenu call
                var rootMenus = _context.Set<UserMenuRootViewModel>()
                    .FromSqlRaw("Select RT.RoleID as MenuID, RT.RoleName as DisplayName, RT.RoleCode as MenuCode from SecurityWeb_RoleTable RT\r\nleft join SecurityWeb_UserRoleRelation URR on URR.RoleID =  RT.RoleID\r\nleft Join UserTable UT on UT.UserId = URR.UserId\r\nWhere UT.UserId = @UserId" , param)
                    .ToList();
                return rootMenus.Select(m => m.MenuID).Distinct().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<MenuItemViewModel> GetAllMenuRelationships()
        {
            try
            {
                //all menu call
                var menuItems = _context.MenuItems
                    .FromSqlRaw("EXEC GetFullSubMenusAndControllerMethodsForSecurityWeb")
                    .ToList();

                return menuItems.Select(m => new MenuItemViewModel
                {
                    ParentID = m.ParentID,
                    ParentCode = m.ParentCode,
                    ChieldID = m.ChieldID,
                    ChieldCode = m.ChieldCode ?? string.Empty,
                    DisplayName = m.DisplayName,
                    ApplicationName = m.ApplicationName,
                    SeqNo = m.SeqNo,
                    IsMenu = m.IsMenu,
                    IsDisplayable = m.IsDisplayable
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<MenuItemViewModel> BuildMenuHierarchy(List<decimal> rootMenuIds, List<MenuItemViewModel> allMenuItems)
        {
            //self-referencing stop
            allMenuItems = allMenuItems.Where(x => x.ParentID != x.ChieldID).ToList();
            //dictionary banano lookup: ParentID -> List of Children
            var childrenLookup = allMenuItems
                .GroupBy(x => x.ParentID)
                .ToDictionary(g => g.Key, g => g.OrderBy(x => x.SeqNo).ToList());
            var rootItems = new List<MenuItemViewModel>();

            //for each root menu ID from SP1
            foreach (var rootMenuId in rootMenuIds.Distinct())
            {
                if (childrenLookup.ContainsKey(rootMenuId))
                {
                    var firstLevelItems = childrenLookup[rootMenuId];
                    foreach (var item in firstLevelItems)
                    {
                        item.Level = 1;
                        //recall
                        BuildChildrenRecursive(item, childrenLookup, 1, new HashSet<decimal> { item.ChieldID });
                        rootItems.Add(item);
                    }
                }
            }
            return rootItems.OrderBy(x => x.SeqNo).ToList();
        }

        private void BuildChildrenRecursive(
            MenuItemViewModel parent,
            Dictionary<decimal, List<MenuItemViewModel>> childrenLookup,
            int currentLevel,
            HashSet<decimal> visitedInPath)
        {
            //check parent's ChildID has any children
            if (!childrenLookup.ContainsKey(parent.ChieldID))
            {
                return;
            }

            var children = childrenLookup[parent.   ChieldID];
            foreach (var child in children)
            {
                //prevent loop to be infinite
                if (visitedInPath.Contains(child.ChieldID))
                {
                    _logger.LogWarning($"Circular reference detected: ChildID {child.ChieldID} already in path");
                    continue;
                }
                //self-referencing stop
                if (child.ParentID == child.ChieldID)
                {
                    continue;
                }
                child.Level = currentLevel + 1;
                parent.Children.Add(child);
                //path to visitednode
                var newPath = new HashSet<decimal>(visitedInPath) { child.ChieldID };
                //recall
                BuildChildrenRecursive(child, childrenLookup, currentLevel + 1, newPath);
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