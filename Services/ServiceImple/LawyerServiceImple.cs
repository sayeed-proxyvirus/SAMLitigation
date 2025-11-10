using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class LawyerServiceImple : LawyerService
    {
        private readonly SAMDbContext _context;
        
        public LawyerServiceImple(SAMDbContext context) 
        {
            _context = context;
        }

        public SAM_Litigation_Lawyer GetById(decimal Id) 
        {
            try
            {
                var lawyer = _context.Lawyer
                    //.Include(l => l.LawyerName)
                .FirstOrDefault(l => l.LitigationLawyerID == Id);

                return lawyer;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<SAM_Litigation_Lawyer> GetLawyerALL() 
        {
            try
            {
                var lawyer = _context.Lawyer
                    .FromSqlRaw("EXEC GetLawyerALL")
                    .ToList();
                return lawyer;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AddLawyer(SAM_Litigation_Lawyer Lawyer) 
        {
            try
            {
                _context.Lawyer.Add(Lawyer);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception )
            {
                
                throw;
            }
        }
        public bool UpdateLawyer(SAM_Litigation_Lawyer Lawyer) 
        {
            try
            {
                _context.Lawyer.Update(Lawyer);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception )
            {

                throw;
            }
        }

    }
}
