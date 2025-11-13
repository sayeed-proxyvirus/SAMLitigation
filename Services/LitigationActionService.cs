using SAMLitigation.Models;

namespace SAMLitigation.Services
{
    public interface LitigationActionService
    {
        public List<SAM_Litigation_Action> GetActionALL();
        bool AddAction(SAM_Litigation_Action Action);
        bool UpdateAction(SAM_Litigation_Action Action);
        SAM_Litigation_Action GetActionById(decimal Id);
    }
}
