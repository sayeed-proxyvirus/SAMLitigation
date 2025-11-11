using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("SAM_Litigation_On")]
    public class SAM_Litigation_On
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("LitigationOnID")]
        public decimal LitigationOnID { get; set; }
        [Column("LitigationOnTypeName")]
        public string LitigationOnTypeName { get; set; }
    }
}
