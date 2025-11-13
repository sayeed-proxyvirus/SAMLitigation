using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("SAM_Litigation_Detail_DocumnetList")]
    public class SAM_Litigation_Detail_DocumentList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal DocumnetListID { get; set; }
        public decimal LitigationDetailID { get; set; }
        public string DocumentName { get; set; }
        public string DocumentLink { get; set; }
    }
}
