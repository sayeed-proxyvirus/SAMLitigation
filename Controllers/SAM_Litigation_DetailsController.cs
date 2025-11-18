using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAMLitigation.Models;
using SAMLitigation.Models.ViewModel;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class SAM_Litigation_DetailsController : Controller
    {
        private readonly ILogger<SAM_Litigation_DetailsController> _logger;
        private readonly LitigationDetailService _litigationDetailService;
        private readonly CourtService _courtService;
        private readonly LawyerService _lawyerService;
        private readonly LitigationActionService _litigationActionService;
        private readonly LitigationMasterService _litigationMasterService;

        public SAM_Litigation_DetailsController(ILogger<SAM_Litigation_DetailsController> logger,
            LitigationDetailService litigationDetailService, CourtService courtService,
            LawyerService lawyerService, LitigationActionService litigationActionService,
            LitigationMasterService litigationMasterService)
        {
            _logger = logger;
            _litigationDetailService = litigationDetailService;
            _courtService = courtService;
            _lawyerService = lawyerService;
            _litigationActionService = litigationActionService;
            _litigationMasterService = litigationMasterService;
        }

        [HttpGet]
        public IActionResult Index(decimal Id)
        {
            try
            {
                // Store the litigation ID in ViewBag for the view
                ViewBag.LitigationId = Id;

                // Get current user role (0 = Maker, 1 = Checker)
                // Replace this with your actual authentication logic
                ViewBag.UserRoleId = GetCurrentUserRole(); // 0 or 1

                // Get current user ID for verification
                ViewBag.CurrentUserId = GetCurrentUserId(); // Get actual logged-in user ID

                List<SAM_Litigation_Court> ListCourt = _courtService.GetCourtALL();
                ViewBag.ListCourts = ListCourt.Select(x => new SelectListItem
                {
                    Value = x.LitigationCourtID.ToString(),
                    Text = x.CourtName
                }).ToList() ?? new List<SelectListItem>();

                List<SAM_Litigation_Lawyer> ListLawyer = _lawyerService.GetLawyerALL();
                ViewBag.ListLawyers = ListLawyer.Select(x => new SelectListItem
                {
                    Value = x.LitigationLawyerID.ToString(),
                    Text = x.LawyerName
                }).ToList() ?? new List<SelectListItem>();

                List<SAM_Litigation_Action> ListAction = _litigationActionService.GetActionALL();
                ViewBag.ListActions = ListAction.Select(x => new SelectListItem
                {
                    Value = x.LitigationActionID.ToString(),
                    Text = x.LitigationActionName
                }).ToList() ?? new List<SelectListItem>();

                _logger.LogInformation($"Fetching details with Litigation ID: {Id}");

                var details = _litigationDetailService.GetDetailsALLByLitigationId(Id);

                if (details == null || !details.Any())
                {
                    _logger.LogWarning($"No details found for Litigation ID {Id}");
                    details = new List<SAM_Litigation_DetailsViewModel>();
                }
                else
                {
                    _logger.LogInformation($"Found {details.Count} detail(s) for Litigation ID {Id}");
                }

                return View(details);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Index: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                }
                return View(new List<SAM_Litigation_DetailsViewModel>());
            }
        }

        // Helper method to get current user role
        private int GetCurrentUserRole()
        {
            // TODO: Replace with your actual authentication logic
            // Return 0 for Maker, 1 for Checker
            // Example:
            // var userRole = User.Claims.FirstOrDefault(c => c.Type == "RoleId")?.Value;
            // return int.Parse(userRole ?? "0");

            return 1; // Temporary - replace with actual logic
        }

        // Helper method to get current user ID
        private decimal GetCurrentUserId()
        {
            // TODO: Replace with your actual authentication logic
            // Example:
            // var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // return decimal.Parse(userId ?? "1");

            return 1; // Temporary - replace with actual logic
        }

        [HttpPost]
        public IActionResult AddDetails(decimal litigationID, decimal litigationActionID,
            DateTime actionTakenDate, string caseNumber, string commentsPriorAction,
            string commentsPostAction, decimal litigationCourtID, decimal litigationLawyerID,
            decimal makerUserID, decimal checkerUserID)
        {
            try
            {
                var Details = new SAM_Litigation_Detail
                {
                    LitigationID = litigationID,
                    LitigationActionID = litigationActionID,
                    ActionTakenDate = actionTakenDate,
                    CommentsPriorAction = commentsPriorAction,
                    CommentsPostAction = commentsPostAction,
                    CaseNumber = caseNumber,
                    CheckerUserID = null, // Set to null initially - no checker yet
                    MakerUserID = GetCurrentUserId(), // Set current user as maker
                    LitigationCourtID = litigationCourtID,
                    LitigationLawyerID = litigationLawyerID
                };

                bool success = _litigationDetailService.Add(Details);
                if (success)
                {
                    _logger.LogInformation($"Litigation detail added successfully for Litigation ID: {litigationID}");
                    return Json(new
                    {
                        success = true,
                        message = $"Litigation detail has been added successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Litigation detail addition failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add litigation detail. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in AddDetails: {ex.Message}");
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
        public IActionResult UpdateDetails(decimal LitigationDetailID, decimal litigationID,
            decimal litigationActionID, DateTime actionTakenDate, string caseNumber,
            string commentsPriorAction, string commentsPostAction, decimal litigationCourtID,
            decimal litigationLawyerID, decimal makerUserID, decimal checkerUserID)
        {
            try
            {
                var existingDetail = _litigationDetailService.GetDetailByIdForUpdate(LitigationDetailID);
                if (existingDetail == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Detail not found."
                    });
                }

                existingDetail.LitigationID = litigationID;
                existingDetail.LitigationActionID = litigationActionID;
                existingDetail.ActionTakenDate = actionTakenDate;
                existingDetail.CaseNumber = caseNumber;
                existingDetail.CommentsPostAction = commentsPostAction;
                existingDetail.CommentsPriorAction = commentsPriorAction;
                existingDetail.LitigationCourtID = litigationCourtID;
                existingDetail.LitigationLawyerID = litigationLawyerID;
                existingDetail.MakerUserID = GetCurrentUserId(); // Update maker

                // Reset CheckerUserID to null when edited (back to pending)
                existingDetail.CheckerUserID = null;

                bool success = _litigationDetailService.Update(existingDetail);
                if (success)
                {
                    _logger.LogInformation($"Litigation detail {LitigationDetailID} updated successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Litigation detail has been updated successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning($"Litigation detail {LitigationDetailID} update failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update litigation detail. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in UpdateDetails: {ex.Message}");
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
        public IActionResult VerifyDetail(decimal litigationDetailId)
        {
            try
            {
                var existingDetail = _litigationDetailService.GetDetailByIdForUpdate(litigationDetailId);
                if (existingDetail == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Detail not found."
                    });
                }

                // Check if already verified
                if (existingDetail.CheckerUserID.HasValue && existingDetail.CheckerUserID.Value > 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "This detail has already been verified."
                    });
                }

                // Set CheckerUserID to current user - this marks it as verified
                existingDetail.CheckerUserID = GetCurrentUserId();

                bool success = _litigationDetailService.Update(existingDetail);
                if (success)
                {
                    _logger.LogInformation($"Litigation detail {litigationDetailId} verified successfully by user {existingDetail.CheckerUserID}");
                    return Json(new
                    {
                        success = true,
                        message = "Litigation detail has been verified successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning($"Litigation detail {litigationDetailId} verification failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to verify litigation detail. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in VerifyDetail: {ex.Message}");
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

        [HttpGet]
        public IActionResult GetDetailById(decimal id)
        {
            try
            {
                _logger.LogInformation($"Fetching detail with LitigationDetailID: {id}");

                var detailEntity = _litigationDetailService.GetDetailByIdForUpdate(id);

                if (detailEntity == null)
                {
                    _logger.LogWarning($"Detail with LitigationDetailID {id} not found");
                    return Json(new
                    {
                        success = false,
                        message = "Detail not found."
                    });
                }

                var detailList = _litigationDetailService.GetDetailsALLByLitigationId(detailEntity.LitigationID);
                var detail = detailList?.FirstOrDefault(d => d.LitigationDetailID == id);

                if (detail == null)
                {
                    _logger.LogWarning($"Detail with LitigationDetailID {id} not found in list");
                    return Json(new
                    {
                        success = false,
                        message = "Detail not found."
                    });
                }

                _logger.LogInformation($"Detail with LitigationDetailID {id} retrieved successfully");

                return Json(new
                {
                    success = true,
                    litigationDetailID = detail.LitigationDetailID,
                    litigationID = detailEntity.LitigationID,
                    litigationActionName = detail.LitigationActionName ?? string.Empty,
                    actionTakenDate = detail.ActionTakenDate.ToString("yyyy-MM-dd"),
                    caseNumber = detail.CaseNumber ?? string.Empty,
                    commentsPriorAction = detail.CommentsPriorAction ?? string.Empty,
                    commentsPostAction = detail.CommentsPostAction ?? string.Empty,
                    courtName = detail.CourtName ?? string.Empty,
                    lawyerName = detail.LawyerName ?? string.Empty
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetDetailById: {ex.Message}");
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