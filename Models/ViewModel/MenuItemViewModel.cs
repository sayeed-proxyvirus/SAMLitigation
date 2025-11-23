namespace SAMLitigation.Models.ViewModel
{
    public class MenuItemViewModel
    {
        public decimal MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public decimal? ParentMenuID { get; set; }
        public int DisplayOrder { get; set; }
        public int MenuLevel { get; set; }

        public List<MenuItemViewModel> ChildrenMenu { get; set; }
        public MenuItemViewModel() 
        {
            ChildrenMenu = new List<MenuItemViewModel>();
        }
    }
}
