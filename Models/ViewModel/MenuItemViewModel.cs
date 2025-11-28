namespace SAMLitigation.Models.ViewModel
{
    public class MenuItemViewModel
    {
        public decimal ParentID { get; set; }
        public string ParentCode { get; set; }
        public decimal ChildID { get; set; }
        public string? ChildCode { get; set; }
        public string DisplayName { get; set; }
        public string ApplicationName { get; set; }
        public decimal SeqNo { get; set; }
        public bool IsMenu { get; set; }
        public bool IsDisplayable { get; set; }
        public int? Level { get; set; }
        public List<MenuItemViewModel> Children { get; set; } = new List<MenuItemViewModel>();
    }
    public class HierarchicalMenuViewModel
    {
        public List<MenuItemViewModel> RootItems { get; set; } = new List<MenuItemViewModel>();
    }
}