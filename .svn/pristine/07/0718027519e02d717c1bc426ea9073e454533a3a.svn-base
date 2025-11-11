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

        [HttpPost]
        public IActionResult AddCause(string causeName) 
        {
            try
            {
                var Cause = new SAM_Litigation_Cause
                {
                    CauseName = causeName
                };
                bool succss = _causeService.AddCause(Cause);
                if (succss)
                {
                    _logger.LogInformation("Cause is added successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Cause '{causeName}' has been added successfully!"
                    });
                }
                else 
                {
                    _logger.LogWarning("Cause data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add cause. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in AddCause: {ex.Message}");
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
        public IActionResult UpdateCause(decimal causeId, string causeName) 
        {
            try
            {
                _logger.LogInformation($"Updating lawyer: {causeName}");

                var existingCause = _causeService.GetCauseById(causeId);
                if (existingCause == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Cause not found."
                    });
                }
                existingCause.CauseName = causeName;
                bool success = _causeService.UpdateCause(existingCause);
                if (success)
                {
                    _logger.LogInformation("Cause Information is updated successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Cause'{causeName}' has been updated successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Cause data update has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update Cause. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in AddCause: {ex.Message}");
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
