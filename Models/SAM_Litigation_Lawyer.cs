using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("SAM_Litigation_Lawyer")]
    public class SAM_Litigation_Lawyer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("LitigationLawyerID")]
        public decimal LitigationLawyerID { get; set; }
        [Column("LawyerName")]
        public string LawyerName { get; set; }

    }
}
