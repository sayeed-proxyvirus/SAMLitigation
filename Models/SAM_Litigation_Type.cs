using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("SAM_Litigation_Type")]
    public class SAM_Litigation_Type
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("LitigationTypeID")]
        public decimal LitigationTypeID { get; set; }
        [Column("LitigationTypeName")]
        public string LitigationTypeName { get; set; }
    }
}
