namespace SAMLitigation.Models.ViewModel
{
    public class MenuItemViewModel
    {
        public decimal MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuCode { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public decimal? ParentMenuID { get; set; }
        public decimal DisplayOrder { get; set; }
        public bool IsMenu { get; set; }
        public bool IsDisplayable { get; set; }
        public string ApplicationName { get; set; }
        public string Icon { get; set; }
        public int MenuLevel { get; set; } // NEW: Track depth level

        public List<MenuItemViewModel> ChildrenMenu { get; set; }

        public MenuItemViewModel()
        {
            ChildrenMenu = new List<MenuItemViewModel>();
        }
    }
}