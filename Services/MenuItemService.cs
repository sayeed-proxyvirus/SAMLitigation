using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Services
{
    public interface MenuItemService
    {
        List<ChildofMenuInfoViewModel> GetMenuHierarchyByUserId(decimal userId);
        Dictionary<decimal, List<ChildofMenuInfoViewModel>> GetMenuGroupedByParent(decimal userId);
        List<MenuItemViewModel> ConvertToMenuItemList(List<ChildofMenuInfoViewModel> rawData);
    }
}