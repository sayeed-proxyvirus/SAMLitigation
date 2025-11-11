using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAMLitigation.Models;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class Loan_NC_T_SectorController : Controller
    {
        private readonly ILogger<Loan_NC_T_SectorController> _logger;
        private readonly SectorService _sectorService;
        public Loan_NC_T_SectorController(ILogger<Loan_NC_T_SectorController> logger, SectorService sectorService)
        {
            _logger = logger;
            _sectorService = sectorService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<Loan_NC_T_Sector> ListSector = _sectorService.GetSectorsALL();
                ViewBag.Sectors = ListSector.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.SectorName
                }).ToList() ?? new List<SelectListItem>();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
