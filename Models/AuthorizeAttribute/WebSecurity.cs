namespace SAMLitigation.Models.AuthorizeAttribute
{
    public class WebSecurity
    {
        public decimal UserID { get; set; }
        public string? ControllerName { get; set; }
        public string? MethodName { get; set; }
        public Boolean IsGet { get; set; }
        public decimal ApprovalDataStageSL { get; set; }
        public decimal ApprovalActivitySL { get; set; }
    }
}
