using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class CauseServiceImple : CauseService
    {
        private readonly ILogger _logger;
        private readonly SAMDbContext _context;
        public CauseServiceImple(SAMDbContext context) 
        {
            _context = context;
        }

        public SAM_Litigation_Cause GetCauseById(decimal Id) 
        {
            try
            {
                var cause = _context.Cause
                    .FirstOrDefault(c => c.LitigationCauseID == Id);

                return cause;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<SAM_Litigation_Cause> GetCauses() 
        {
            try
            {
                var cause = _context.Cause
                    .FromSqlRaw("EXEC GetCauseALL")
                    .ToList();
                return cause;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AddCause(SAM_Litigation_Cause Cause) 
        {
            try
            {
                _context.Cause.Add(Cause);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateCause(SAM_Litigation_Cause Cause) 
        {
            try
            {
                _context.Cause.Update(Cause);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
