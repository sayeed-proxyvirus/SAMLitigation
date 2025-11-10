using SAMLitigation.Models;

namespace SAMLitigation.Services
{
    public interface CauseService
    {
        public List<SAM_Litigation_Cause> GetCauses();
        bool AddCause(SAM_Litigation_Cause Cause);
        bool UpdateCause(SAM_Litigation_Cause Cause);
        SAM_Litigation_Cause GetCauseById(decimal Id);
    }
}
