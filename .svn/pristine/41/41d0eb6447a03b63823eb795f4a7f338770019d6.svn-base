using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models.ApplicationDbContext;
using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Services.ServiceImple
{
    public class LoanProjectServiceImple : LoanProjectService
    {
        private readonly SAMDbContext _context;

        public LoanProjectServiceImple(SAMDbContext context)
        {
            _context = context;
        }

        public List<LoanNCPaperProcessingProjectViewModel> GetALl()
        {
            try
            {
                var ProjectList= _context.Set<LoanNCPaperProcessingProjectViewModel>()
                                   .FromSqlRaw("EXEC GetAllProjects")
                                   .ToList();
                return ProjectList;
            } 
            catch 
            {
                throw;
            }
        }
    }
}
