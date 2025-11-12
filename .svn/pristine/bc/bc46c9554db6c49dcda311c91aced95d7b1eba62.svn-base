using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("SAM_Litigation_Master")]
    public class SAM_Litigation_Master
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal LitigationID { get; set; }
        public decimal ProjectID { get; set; }
        public decimal LitigationTypeID { get; set; }
        public decimal LitigationStatusId { get; set; }
        public decimal LitigationCauseID { get; set; }
        public decimal ComplainantLitigationPartyID { get; set; }
        public decimal DefendentLitigationPartyID { get; set; }
        public string? InitialCaseNumber { get; set; }
        public Boolean IsAgainstIDCOL { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }


    }
}
