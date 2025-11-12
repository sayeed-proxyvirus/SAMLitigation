using SAMLitigation.Models;

namespace SAMLitigation.Services
{
    public interface LitigationMasterService
    {
        public bool AddLitigationMaster(SAM_Litigation_Master sAM_Litigation_Master);
        public List<SAM_Litigation_Master> GetAll();
    }
}
