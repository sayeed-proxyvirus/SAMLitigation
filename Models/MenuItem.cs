using System.ComponentModel.DataAnnotations;

namespace SAMLitigation.Models
{
    public class MenuItem
    {
        public decimal ParentID { get; set; }
        public string ParentCode { get; set; }
        public decimal ChieldID { get; set; }
        public string? ChieldCode { get; set; }
        public string DisplayName { get; set; }
        public string ApplicationName { get; set; }
        public decimal SeqNo { get; set; }
        public bool IsMenu { get; set; }
        public bool IsDisplayable { get; set; }
    }
}
