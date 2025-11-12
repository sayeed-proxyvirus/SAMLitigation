using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class PaperProcessing_CycleServiceImple : PaperProcessing_CycleService 
    {
        private readonly SAMDbContext _context;
        public PaperProcessing_CycleServiceImple(SAMDbContext context)
        {
            _context = context;
        }
        public List<Loan_NC_PaperProcessing_Cycle> GetCyclesALL()
        {
            try
            {
                var cycles = _context.PaperProcessing_Cycles
                    .FromSqlRaw("EXEC GetCyclesALL")
                    .ToList();
                return cycles;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    
}
