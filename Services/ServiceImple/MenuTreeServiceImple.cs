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
        public List<MenuItemRawViewModel> GetRawMenuData(decimal roleId)
        {
            var param = new SqlParameter("@RoleId", roleId);

            return _context.Set<MenuItemRawViewModel>()
                .FromSqlRaw("EXEC GetUserAccessList @RoleId", param)
                .ToList();
        }
        private MenuItemViewModel ConvertToMenuModel(ChildofMenuInfoViewModel item)
        {
            return new MenuItemViewModel
            {
                MenuID = item.ChieldID,
                MenuName = item.DisplayName,
                ParentMenuID = item.ParentID,
                DisplayOrder = (int)item.SeqNo,
                ControllerName = item.ChieldCode,     
                ActionName = "",                      
                ChildrenMenu = new List<MenuItemViewModel>()
            };
        }

        public List<MenuItemViewModel> BuildMenuTree(List<ChildofMenuInfoViewModel> raw)
        {

            var list = raw.Select(x => new MenuItemViewModel
            {
                MenuID = x.ChieldID,
                MenuName = x.DisplayName,
                ParentMenuID = x.ParentID,
                DisplayOrder = (decimal)x.SeqNo,
                ChildrenMenu = new List<MenuItemViewModel>()
            }).ToList();

            var lookup = list.ToDictionary(m => m.MenuID, m => m);

            List<MenuItemViewModel> rootNodes = new List<MenuItemViewModel>();

            foreach (var node in list)
            {
                if (node.ParentMenuID == null || node.ParentMenuID == 0)
                {
                    rootNodes.Add(node);
                }
                else
                {
                    if (lookup.ContainsKey(node.ParentMenuID.Value))
                    {
                        lookup[node.ParentMenuID.Value].ChildrenMenu.Add(node);
                    }
                }
            }

            foreach (var node in list)
            {
                node.ChildrenMenu = node.ChildrenMenu.OrderBy(c => c.DisplayOrder).ToList();
            }

            return rootNodes.OrderBy(x => x.DisplayOrder).ToList();
        }




    }
}
