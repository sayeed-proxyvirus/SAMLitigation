using SAMLitigation.Models;

namespace SAMLitigation.Services
{
    public interface LitigationPartyService
    {
        public bool Add(SAM_Litigation_Party sAM_Litigation_Party);
        public List<SAM_Litigation_Party> GetAll();
    }
}
