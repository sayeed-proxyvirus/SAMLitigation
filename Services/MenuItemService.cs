using SAMLitigation.Models.ViewModel;

public interface MenuItemService
{
    List<MenuItemRawViewModel> GetRawMenuData(decimal roleId);
    List<MenuItemViewModel> BuildMenuTree(List<ChildofMenuInfoViewModel> raw);


}
