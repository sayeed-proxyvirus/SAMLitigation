using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAMLitigation.Models.ViewModel
{
    public class SAM_Litigation_DetailsViewModel
    {
        public decimal LitigationDetailID { get; set; }
        public decimal LitigationID { get; set; }
        public decimal PartialViewNumber { get; set; }
        public string LitigationActionName { get; set; }
        public DateTime ActionTakenDate { get; set; }
        public string CaseNumber { get; set; }
        public string CommentsPriorAction { get; set; }
        public string CommentsPostAction { get; set; }
        public string CourtName { get; set; }
        public string LawyerName { get; set; }
        public decimal? MakerUserID { get; set; }
        public decimal? CheckerUserID { get; set; }

        // Computed property for Status
        [NotMapped]
        public string Status
        {
            get
            {
                if (MakerUserID.HasValue && MakerUserID.Value > 0)
                {
                    if (CheckerUserID.HasValue && CheckerUserID.Value > 0)
                    {
                        return "Verified";
                    }
                    return "PendingVerification";
                }
                return "Draft";
            }
        }
    }
}