namespace SAMLitigation.Models.ViewModel
{
    public class MenuItemRawViewModel
    {
        public decimal MenuID { get; set; }
        public string MenuName { get; set; }
        public decimal? ParentMenuID { get; set; }
        public int DisplayOrder { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }

}
