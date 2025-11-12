using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models.ViewModel;
using SAMLitigation.Services;

namespace SAMLitigation.Controllers
{
    public class SAM_Litigation_MasterController : Controller
    {
        private  LitigationMasterService _litigationMasterService;
        private TypeService _typeService;
        private CauseService _causeService;
        private StatusService _statusService;
        private LoanProjectService _loanProjectService;
        private LitigationPartyService _litigationPartyService;

       

        public SAM_Litigation_MasterController(LitigationMasterService litigationMasterService, TypeService typeService, CauseService causeService, StatusService statusService, LoanProjectService loanProjectService, LitigationPartyService litigationPartyService)
        {
            _litigationMasterService = litigationMasterService;
            _typeService = typeService;
            _causeService = causeService;
            _statusService = statusService;
            _loanProjectService = loanProjectService;
            _litigationPartyService = litigationPartyService;
        }

        public IActionResult Index()
        {
            try 
            {
                List<LoanNCPaperProcessingProjectViewModel> ProjectList = _loanProjectService.GetALl();
                ViewBag.ProjectList = new SelectList(ProjectList, "ProjectID", "StatementName");
                ViewBag.LitigationTypeList = new SelectList(_typeService.GetTypesALL(), "LitigationTypeID", "LitigationTypeName");
                ViewBag.StatusList= new SelectList(_statusService.GetStatusALL(), "LitigationStatusId", "LitigationStatusName");
                ViewBag.CauseList = new SelectList(_causeService.GetCauses(), "LitigationCauseID", "CauseName");
                ViewBag.PartyList = new SelectList(_litigationPartyService.GetAll(), "LitigationPartyID", "LitigationParty");


                return View();
            }
            catch 
            {
                throw;
            }
        }
    }
}
