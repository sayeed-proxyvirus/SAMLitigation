using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class LitigationDetailDocumentListServiceImple : LitigationDetailDocumentListService
    {
        private readonly SAMDbContext _context;
        public LitigationDetailDocumentListServiceImple(SAMDbContext context)
        {
            _context = context;
        }
        public SAM_Litigation_Detail_DocumentList GetDetailDocumentListById(decimal Id) 
        {
            try
            {
                var param = new SqlParameter("@Id", Id);
                var detaildoc = _context.Litigation_Details_DocumentList
                    .FromSqlRaw("EXEC GetDetailDocumentListById @Id", param)
                    .AsEnumerable()
                    .FirstOrDefault();
                return detaildoc;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SAM_Litigation_Detail_DocumentList GetDetailDocumentListByIdForUpdate(decimal Id)
        {
            try
            {
                var detaildoc = _context.Litigation_Details_DocumentList
                    .FirstOrDefault(l => l.DocumnetListID == Id);
                return detaildoc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Add(SAM_Litigation_Detail_DocumentList sAM_Litigation_Detail_DocumentList)
        {
            try
            {
                _context.Litigation_Details_DocumentList.Add(sAM_Litigation_Detail_DocumentList);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(SAM_Litigation_Detail_DocumentList sAM_Litigation_Detail_DocumentList)
        {
            try
            {
                _context.Litigation_Details_DocumentList.Update(sAM_Litigation_Detail_DocumentList);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
