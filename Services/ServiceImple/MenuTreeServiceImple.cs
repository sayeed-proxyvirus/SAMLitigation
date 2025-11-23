using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models.ApplicationDbContext;
using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Services.ServiceImple
{
    public class MenuTreeServiceImple : MenuItemService
    {
        private readonly SAMDbContext _context;
        public MenuTreeServiceImple(SAMDbContext context) 
        {
            _context = context;
        }
        public List<MenuItemViewModel> GetMenuItems(decimal Id) 
        {
            try
            {
                var param = new SqlParameter("@RoleId", Id);
                var menulist = _context.Set<MenuItemViewModel>()
                    .FromSqlRaw("EXEC GetUserAccessList @RoleId", param)
                    .ToList();
                var menutree = BuildMenuTree(menulist);
                return menutree;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<MenuItemViewModel> BuildMenuTree(List<MenuItemViewModel> menulist) 
        {
            var rootlist = new List<MenuItemViewModel>();
            var menuDictionary = menulist.ToDictionary(m => m.MenuID);
            foreach (var menu in menulist) 
            {
                if (menu.ParentMenuID == null || menu.ParentMenuID == 0)
                {
                    rootlist.Add(menu);
                }
                else 
                {
                    if (menuDictionary.ContainsKey(menu.ParentMenuID.Value))
                    {
                        var parent = menuDictionary[menu.ParentMenuID.Value];
                        parent.Children.Add(menu);
                    }
                }
                return rootlist;
            }
            return rootlist;
        }
    }
}
