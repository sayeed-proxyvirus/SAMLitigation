using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class ProjectTypeServiceImple : ProjectTypeService
    {
        private readonly SAMDbContext _context;
        public ProjectTypeServiceImple(SAMDbContext context)
        {
            _context = context;
        }
        public List<Loan_NC_T_ProjectType> GetProjectTypeALL() 
        {
            try
            {
                var projecttypes = _context.ProjectType
                    .FromSqlRaw("EXEC GetProjectTypeALL")
                    .ToList();
                return projecttypes;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
