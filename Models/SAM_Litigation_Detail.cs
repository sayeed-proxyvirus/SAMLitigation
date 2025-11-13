using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models
{
    [Table("SAM_Litigation_Detail")]
    public class SAM_Litigation_Detail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal LitigationDetailID { get; set; }
        public decimal LitigationID { get; set; }
        public decimal LitigationActionID { get; set; }
        public DateTime ActionTakenDate { get; set; }
        public string CaseNumber { get; set; }
        public string CommentsPriorAction { get; set; }
        public string CommentsPostAction { get; set; }
        public decimal LitigationCourtID { get; set; }
        public decimal LitigationLawyerID { get; set; }
        public decimal? MakerUserID { get; set; }
        public decimal? CheckerUserID { get; set; }
    }
}
