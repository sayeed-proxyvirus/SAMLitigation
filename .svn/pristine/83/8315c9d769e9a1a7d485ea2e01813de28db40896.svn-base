using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAMLitigation.Models;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class Loan_NC_PaperProcessing_CycleController : Controller
    {
        private readonly ILogger<Loan_NC_PaperProcessing_CycleController> _logger;
        private readonly PaperProcessing_CycleService _paperProcessing_CycleService;
        public Loan_NC_PaperProcessing_CycleController(ILogger<Loan_NC_PaperProcessing_CycleController> logger, PaperProcessing_CycleService paperProcessing_CycleService)
        {
            _logger = logger;
            _paperProcessing_CycleService = paperProcessing_CycleService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<Loan_NC_PaperProcessing_Cycle> ListProcessing_Cycle = _paperProcessing_CycleService.GetCyclesALL();

                ViewBag.Cycles = ListProcessing_Cycle.Select(x => new SelectListItem
                {
                    Value = x.CycleID.ToString(),
                    Text = x.CycleName
                }).ToList() ?? new List<SelectListItem>();

                return View(ListProcessing_Cycle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Processing Cycles");
                throw;
            }
        }
    }
}
