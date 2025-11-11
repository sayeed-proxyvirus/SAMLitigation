using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAMLitigation.Models;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class Loan_NC_T_ProjectTypeController : Controller
    {
        private readonly ILogger<Loan_NC_T_ProjectTypeController> _logger;
        private readonly ProjectTypeService _projectTypeService;
        private readonly SectorService _sectorService; // Add sector service

        public Loan_NC_T_ProjectTypeController(
            ILogger<Loan_NC_T_ProjectTypeController> logger,
            ProjectTypeService projectTypeService,
            SectorService sectorService) // Inject sector service
        {
            _logger = logger;
            _projectTypeService = projectTypeService;
            _sectorService = sectorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // Get all project types with sector information
                List<Loan_NC_T_ProjectType> ListProjectType = _projectTypeService.GetProjectTypeALL();

                ViewBag.ProjectTypes = ListProjectType.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.ProjectTypeName
                }).ToList() ?? new List<SelectListItem>();

                List<Loan_NC_T_Sector> ListSectors = _sectorService.GetSectorsALL();

                ViewBag.Sectors = ListSectors.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.SectorName
                }).ToList() ?? new List<SelectListItem>();

                return View(ListProjectType); // Pass the list to the view
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project types and sectors");
                throw;
            }
        }
    }
}