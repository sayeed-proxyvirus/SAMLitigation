using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("SAM_Litigation_Cause")]
    public class SAM_Litigation_Cause
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("LitigationCauseID")]
        public decimal LitigationCauseID { get; set; }
        [Column("CauseName")]
        public string CauseName { get; set; }
    }
}
