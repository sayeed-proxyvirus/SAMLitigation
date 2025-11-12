using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class LitigationMasterServiceImple : LitigationMasterService
    {
        private readonly SAMDbContext _context;

        public LitigationMasterServiceImple(SAMDbContext context)
        {
            _context = context;
        }

        public bool AddLitigationMaster(SAM_Litigation_Master sAM_Litigation_Master)
        {
            try 
            {
                _context.LitigationMaster.Add(sAM_Litigation_Master);
                int result = _context.SaveChanges();
                return result>0;
            }
            catch 
            {
                throw;
            }
        }

        public List<SAM_Litigation_Master> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
