using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Services
{
    public interface DynamicMenuService
    {
        /// <summary>
        /// Gets hierarchical menu structure for a specific user
        /// </summary>
        /// <param name="userId">User ID to fetch menus for</param>
        /// <returns>Hierarchical menu view model with root items and their children</returns>
        HierarchicalMenuViewModel GetUserMenuHierarchy(decimal userId);

        /// <summary>
        /// Gets flat list of all menu items accessible by user
        /// </summary>
        List<MenuItemViewModel> GetUserMenuFlat(decimal userId);
    }
}