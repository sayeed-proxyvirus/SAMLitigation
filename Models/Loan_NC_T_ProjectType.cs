using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("Loan_NC_T_ProjectType")]
    public class Loan_NC_T_ProjectType
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public decimal Id { get; set; }

        [Column("ProjectTypeName")]
        public string ProjectTypeName { get; set; }

        [Column("SectorId")]
        public decimal SectorId { get; set; }

        public virtual Loan_NC_T_Sector Sector { get; set; }
    }
}