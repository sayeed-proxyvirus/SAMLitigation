using Microsoft.AspNetCore.Mvc;
using SAMLitigation.Models;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class SAM_Litigation_ActionController : Controller
    {
        private readonly ILogger<SAM_Litigation_ActionController> _logger;
        private readonly LitigationActionService _litigationActionService;
        public SAM_Litigation_ActionController(ILogger<SAM_Litigation_ActionController> logger, LitigationActionService litigationActionService)
        {
            _logger = logger;
            _litigationActionService = litigationActionService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<SAM_Litigation_Action> ListAction = _litigationActionService.GetActionALL();
                return View(ListAction);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult AddAction(string actionName) 
        {
            try
            {
                var action = new SAM_Litigation_Action 
                { 
                    LitigationActionName = actionName 
                };
                bool success = _litigationActionService.AddAction(action);
                if (success)
                {
                    _logger.LogInformation("Action is added successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Action '{actionName}' has been added successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Actiona data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add action. Please try again."
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult UpdateAction
            (
            decimal actionId,
            string actionName
            )
        {
            try
            {
                _logger.LogInformation($"Updating Action: {actionName}");

                var existingAction = _litigationActionService.GetActionById(actionId);
                if (existingAction == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Lawyer not found."
                    });
                }
                existingAction.LitigationActionName = actionName;
                bool success = _litigationActionService.UpdateAction(existingAction);
                if (success)
                {
                    _logger.LogInformation("Action is updated successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Action '{actionName}' has been updated successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Action data update has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update action. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Updateaction: {ex.Message}");
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
