using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("Loan_NC_PaperProcessing_Cycle")]
    public class Loan_NC_PaperProcessing_Cycle
    {
        [Key]
        [Column("CycleID")]
        public decimal CycleID { get; set; }
        [Column("CycleName")]
        public string CycleName { get; set; }
    }
}
