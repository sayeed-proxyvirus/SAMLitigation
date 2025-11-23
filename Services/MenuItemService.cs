using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Services
{
    public class MenuItemService
    {
        public List<MenuItemViewModel> GetMenuTree(decimal Id);
        public List<MenuItemViewModel> BuildMenuTree(List<MenuItemViewModel> menulist);
    }
}
