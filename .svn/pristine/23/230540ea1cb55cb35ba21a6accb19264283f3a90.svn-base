using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class LitigationPartyServiceImple : LitigationPartyService
    {
        private readonly SAMDbContext _context;

        public LitigationPartyServiceImple(SAMDbContext context)
        {
            _context = context;
        }

        public bool Add(SAM_Litigation_Party sAM_Litigation_Party)
        {
            try 
            {
                _context.LitigationParty.Add(sAM_Litigation_Party);
                int result = _context.SaveChanges();
                return result > 0;
            } 
            catch 
            {
                throw;
            }
        }

        public List<SAM_Litigation_Party> GetAll()
        {
            try
            {
                var litigationPartyList= _context.LitigationParty
                    .FromSqlRaw("EXEC GetLitigationPartyAll")
                    .ToList();
                return litigationPartyList;
            } 
            catch 
            {
                throw;
            }
        }
    }
}
