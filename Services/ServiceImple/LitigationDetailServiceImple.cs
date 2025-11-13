using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;
using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Services.ServiceImple
{
    public class LitigationDetailServiceImple : LitigationDetailService
    {
        private readonly SAMDbContext _context;

        public LitigationDetailServiceImple(SAMDbContext context)
        {
            _context = context;
        }
        public SAM_Litigation_DetailsViewModel GetDetailById(decimal Id)
        {
            try
            {
                var detail = _context.LitigationDetailsViewModel
                    .FromSqlRaw("EXEC GetLitigationDetailALLExById @Id = {0}", Id)
            .AsEnumerable()
            .FirstOrDefault();
                return detail;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SAM_Litigation_Detail GetDetailByIdForUpdate(decimal Id)
        {
            try
            {
                var detail = _context.Litigation_Details
                    .FirstOrDefault(l => l.LitigationDetailID == Id);
                return detail;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Add(SAM_Litigation_Detail sAM_Litigation_Detail)
        {
            try
            {
                _context.Litigation_Details.Add(sAM_Litigation_Detail);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(SAM_Litigation_Detail sAM_Litigation_Detail)
        {
            try
            {
                _context.Litigation_Details.Update(sAM_Litigation_Detail);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<SAM_Litigation_DetailsViewModel> GetDetailsALLEx()
        {
            try
            {
                var litigationDetailList = _context.LitigationDetailsViewModel
                    .FromSqlRaw("EXEC GetLitigationDetailALLEx")
                    .ToList();
                return litigationDetailList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}