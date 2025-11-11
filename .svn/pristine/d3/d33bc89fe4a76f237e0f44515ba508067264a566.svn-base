using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class SectorServiceImple : SectorService
    {
        //private readonly ILogger _logger;
        private readonly SAMDbContext _context;
        public SectorServiceImple(SAMDbContext context)
        {
            //_logger = logger;
            _context = context;
        }
        public List<Loan_NC_T_Sector> GetSectorsALL() 
        {
            try
            {
                var sector = _context.Sector
                    .FromSqlRaw("EXEC GetSectorsALL")
                    .ToList();
                return sector;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
