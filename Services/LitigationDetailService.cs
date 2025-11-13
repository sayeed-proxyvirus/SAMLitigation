using SAMLitigation.Models;
using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Services
{
    public interface LitigationDetailService
    {
        public bool Add(SAM_Litigation_Detail sAM_Litigation_Detail);
        public bool Update(SAM_Litigation_Detail sAM_Litigation_Detail);
        public List<SAM_Litigation_DetailsViewModel> GetDetailsALLEx();
        SAM_Litigation_DetailsViewModel GetDetailById(decimal Id);
        SAM_Litigation_Detail GetDetailByIdForUpdate(decimal Id);
    }
}