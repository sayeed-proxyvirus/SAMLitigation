using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("Loan_NC_T_Sector")]
    public class Loan_NC_T_Sector
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public decimal Id { get; set; }
        
        [Column("SectorName")]

        public string SectorName { get; set; }

        public virtual ICollection<Loan_NC_T_ProjectType> ProjectTypes { get; set; }
    }
}