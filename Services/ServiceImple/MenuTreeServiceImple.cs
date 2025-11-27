using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models.ApplicationDbContext;
using SAMLitigation.Models.ViewModel;
using System.Linq;
using System.Numerics;

namespace SAMLitigation.Services.ServiceImple
{
    public class MenuTreeServiceImple : MenuItemService
    {
        private readonly SAMDbContext _context;
        private readonly ILogger<MenuTreeServiceImple> _logger;
        public MenuTreeServiceImple(SAMDbContext context, ILogger<MenuTreeServiceImple> logger)
        {
            _context = context;
            _logger = logger;
        }
        public HierarchicalMenuViewModel GetHierarchicalMenu()
        {
            var flatMenuItems = _context.MenuItems
                .FromSqlRaw("EXEC GETALL")
                .ToList();

            var menuViewModels = flatMenuItems.Select(m => new MenuItemViewModel
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

            var hierarchicalMenu = BuildHierarchyIterative(menuViewModels);

            return new HierarchicalMenuViewModel
            {
                RootItems = hierarchicalMenu
            };
        }

        public List<MenuItemViewModel> BuildHierarchyIterative(List<MenuItemViewModel> flatList)
        {
            flatList = flatList.Where(x => x.ParentID != x.ChildID).ToList();

            // Create lookup: ParentID -> List of Children
            var childrenLookup = flatList
                .GroupBy(x => x.ParentID)
                .ToDictionary(g => g.Key, g => g.OrderBy(x => x.SeqNo).ToList());

            // Get all ChildIDs for quick lookup
            var allChildIds = new HashSet<decimal>(flatList.Select(x => x.ChildID));

            // Step 1: Find Root Level (ParentIDs that are NOT in any ChildID column)
            var rootParentIds = flatList
                .Select(x => x.ParentID)
                .Distinct()
                .Where(parentId => !allChildIds.Contains(parentId))
                .ToList();

            var rootItems = new List<MenuItemViewModel>();
            foreach (var rootParentId in rootParentIds)
            {
                if (childrenLookup.ContainsKey(rootParentId))
                {
                    var roots = childrenLookup[rootParentId];
                    foreach (var root in roots)
                    {
                        root.Level = 1;
                        rootItems.Add(root);
                    }
                }
            }

            // Step 2: Process level by level iteratively
            int currentLevel = 1;
            var currentLevelItems = new List<MenuItemViewModel>(rootItems);

            while (currentLevelItems.Any())
            {
                var nextLevelItems = new List<MenuItemViewModel>();

                foreach (var parent in currentLevelItems)
                {
                    // Find children where ParentID = parent's ChildID
                    if (childrenLookup.ContainsKey(parent.ChildID))
                    {
                        var children = childrenLookup[parent.ChildID];

                        foreach (var child in children)
                        {
                            child.Level = currentLevel + 1;
                            parent.Children.Add(child);
                            nextLevelItems.Add(child);
                        }
                    }
                }

                // Check if any of the nextLevel's ChildIDs exist as ParentIDs
                if (nextLevelItems.Any())
                {
                    var nextLevelChildIds = nextLevelItems.Select(x => x.ChildID).ToHashSet();

                    // Check if any of these ChildIDs appear as ParentIDs in our lookup
                    bool hasMoreLevels = nextLevelChildIds.Any(childId => childrenLookup.ContainsKey(childId));

                    if (!hasMoreLevels)
                    {
                        // No more levels, exit loop
                        break;
                    }
                }

                // Move to next level
                currentLevelItems = nextLevelItems;
                currentLevel++;
            }

            return rootItems.OrderBy(x => x.SeqNo).ToList();
        }

        private void AttachChildren(
            MenuItemViewModel parent,
            Dictionary<decimal, List<MenuItemViewModel>> groupedByParent,  // Changed to decimal
            int currentLevel,
            HashSet<decimal> visitedInCurrentPath)  // Changed to decimal
        {
            // Check if parent's ChildID has any children
            if (!groupedByParent.ContainsKey(parent.ChildID))
            {
                return; // No children, exit recursion
            }

            var children = groupedByParent[parent.ChildID];

            foreach (var child in children)
            {
                // Skip if this ChildID was already visited in the current path
                if (visitedInCurrentPath.Contains(child.ChildID))
                {
                    continue; // Skip to prevent infinite loop
                }

                // Skip if child's ParentID equals its own ChildID
                if (child.ParentID == child.ChildID)
                {
                    continue;
                }

                child.Level = currentLevel + 1;
                parent.Children.Add(child);

                // Create a NEW HashSet<decimal> for this branch
                var newPath = new HashSet<decimal>(visitedInCurrentPath) { child.ChildID };

                // Recursively attach children with the new path
                AttachChildren(child, groupedByParent, currentLevel + 1, newPath);
            }
        }
    }
}