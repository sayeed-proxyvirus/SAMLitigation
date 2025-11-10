using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAMLitigation.Models;
using SAMLitigation.Services;
using SAMLitigation.Services.ServiceImple;

namespace SAMLitigation.Controllers
{
    public class SAM_Litigation_CourtController : Controller
    {
        private readonly ILogger<SAM_Litigation_CourtController> _logger;
        private readonly CourtService _courtService;


        public SAM_Litigation_CourtController(ILogger<SAM_Litigation_CourtController> logger, CourtService courtService)
        {
            _logger = logger;
            _courtService = courtService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                
                List<SAM_Litigation_Court> ListCourt = _courtService.GetCourt();

                ViewBag.Courts = ListCourt.Select(x => new SelectListItem
                {
                    Value = x.LitigationCourtID.ToString(),
                    Text = x.CourtName
                }).ToList() ?? new List<SelectListItem>();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult AddCourt
            (
            string courtName
            )
        {
            try
            {
                var Court = new SAM_Litigation_Court
                {
                    CourtName = courtName
                };
                bool success = _courtService.AddCourt(Court);
                if (success)
                {
                    _logger.LogInformation("Court is added successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Lawyer '{courtName}' has been added successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Court data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add court. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in AddCourt: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new
                {
                    success = false,
                    message = "An error occurred: " + ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult UpdateCourt
            (
            decimal courtId,
            string courtName
            )
        {
            try
            {
                _logger.LogInformation($"Updating lawyer: {courtName}");

                var existingCourt = _courtService.GetById(courtId);
                if (existingCourt == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Court not found."
                    });
                }
                existingCourt.CourtName = courtName;
                bool success = _courtService.UpdateCourt(existingCourt);
                if (success)
                {
                    _logger.LogInformation("Court Information is updated successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Court'{courtName}' has been updated successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Court data update has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update Court. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in AddCourt: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                }
                return Json(new
                {
                    success = false,
                    message = "An error occurred: " + ex.Message
                });
            }
        }
    }
}
