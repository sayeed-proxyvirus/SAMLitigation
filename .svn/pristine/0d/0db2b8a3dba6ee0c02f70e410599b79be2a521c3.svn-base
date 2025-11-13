using Microsoft.AspNetCore.Mvc;
using SAMLitigation.Models;
using SAMLitigation.Models.ViewModel;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class SAM_Litigation_Detail_DocumentListController : Controller
    {
        private readonly ILogger<SAM_Litigation_Detail_DocumentListController> _logger;
        private readonly LitigationDetailDocumentListService _litigationDetailDocumentListService;

        public SAM_Litigation_Detail_DocumentListController(ILogger<SAM_Litigation_Detail_DocumentListController> logger, LitigationDetailDocumentListService litigationDetailDocumentListService)
        {
            _logger = logger;
            _litigationDetailDocumentListService = litigationDetailDocumentListService;
        }
        [HttpGet]
        public IActionResult Index(decimal Id)
        {
            try
            {
                var detailDoc = _litigationDetailDocumentListService.GetDetailDocumentListById(Id);
                List<SAM_Litigation_Detail_DocumentList> details;
                if (detailDoc == null)
                {
                    _logger.LogWarning($"Document Details with ID {Id} not found");
                    details = new List<SAM_Litigation_Detail_DocumentList>();
                }
                else 
                {
                    _logger.LogInformation($"Document Detail with ID {Id} retrieved successfully");
                    details = new List<SAM_Litigation_Detail_DocumentList> { detailDoc };
                }
                return View(detailDoc);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Index: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                }
                return View(new List<SAM_Litigation_Detail_DocumentList>());
            }
        }
        [HttpPost]
        public IActionResult AddDetails(decimal litigationDetailID, string documentName, string documentLink)
        {
            try
            {
                var DetailDocs = new SAM_Litigation_Detail_DocumentList
                {
                    LitigationDetailID = litigationDetailID,
                    DocumentName = documentName,
                    DocumentLink = documentLink
                };

                bool success = _litigationDetailDocumentListService.Add(DetailDocs);
                if (success)
                {
                    _logger.LogInformation("Litigation Document Info is added successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Litigation Document Info of '{litigationDetailID}' has been added successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Litigation Document Info data addition has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to add Litigation Document Info. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Add: {ex.Message}");
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
        public IActionResult UpdateDetails(decimal litigationDetailID, string documentName, string documentLink)
        {
            try
            {
                var existingDetailDoc = _litigationDetailDocumentListService.GetDetailDocumentListByIdForUpdate(litigationDetailID);
                if (existingDetailDoc == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Detail Document Info not found."
                    });
                }
                existingDetailDoc.DocumentName = documentName;
                existingDetailDoc.DocumentLink = documentLink;

                bool success = _litigationDetailDocumentListService.Update(existingDetailDoc);
                if (success)
                {
                    _logger.LogInformation("Litigation Document Info is updated successfully");
                    return Json(new
                    {
                        success = true,
                        message = $"Litigation Document Info of '{litigationDetailID}' has been updated successfully!"
                    });
                }
                else
                {
                    _logger.LogWarning("Litigation Document Info data Update has been failed");
                    return Json(new
                    {
                        success = false,
                        message = "Failed to update Litigation Document Info. Please try again."
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in Update: {ex.Message}");
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
        public IActionResult GetDetailDocById(decimal id)
        {
            try
            {
                _logger.LogInformation($"Fetching detail with ID: {id}");

                var detail = _litigationDetailDocumentListService.GetDetailDocumentListById(id);

                if (detail == null)
                {
                    _logger.LogWarning($"Detail Document with ID {id} not found");
                    return Json(new
                    {
                        success = false,
                        message = "Detail Document Info not found."
                    });
                }

                _logger.LogInformation($"Detail Document with ID {id} retrieved successfully");

                // Return the detail data as JSON - ViewModel has names, not IDs
                return Json(new
                {
                    success = true,
                    litigationDetailID = detail.LitigationDetailID,
                    documentName = detail.DocumentName,
                    documentLink = detail.DocumentLink
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetDetailDocumentListById: {ex.Message}");
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
