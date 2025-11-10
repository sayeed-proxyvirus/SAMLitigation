using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAMLitigation.Models;
using SAMLitigation.Services;
using static System.Net.Mime.MediaTypeNames;

namespace SAMLitigation.Controllers
{
    public class SAM_Litigation_CauseController : Controller
    {
        private readonly ILogger<SAM_Litigation_CauseController> _logger;
        private readonly CauseService _causeService;

        public SAM_Litigation_CauseController(ILogger<SAM_Litigation_CauseController> logger, CauseService causeService)
        {
            _logger = logger;
            _causeService = causeService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<SAM_Litigation_Cause> ListCause = _causeService.GetCauses();
                ViewBag.Causes = ListCause.Select(x => new SelectListItem
                {
                    Value = x.LitigationCauseID.ToString(),
                    Text = x.CauseName
                }).ToList() ?? new List<SelectListItem>();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //[HttpPost]
        //public
    }
}
