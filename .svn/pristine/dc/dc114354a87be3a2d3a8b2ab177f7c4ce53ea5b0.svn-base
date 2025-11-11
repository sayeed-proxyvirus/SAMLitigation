using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class TypeServiceImple : TypeService
    {
        private readonly ILogger _logger;
        private readonly SAMDbContext _context;
        public TypeServiceImple(SAMDbContext context)
        {
            _context = context;
        }

        public SAM_Litigation_Type GetTypeById(decimal Id)
        {
            try
            {
                var type = _context.Type
                    .FirstOrDefault(c => c.LitigationTypeID == Id);

                return type;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<SAM_Litigation_Type> GetTypesALL()
        {
            try
            {
                var type = _context.Type
                    .FromSqlRaw("EXEC GetTypesALL")
                    .ToList();
                return type;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AddType(SAM_Litigation_Type Type)
        {
            try
            {
                _context.Type.Add(Type);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateType(SAM_Litigation_Type Type)
        {
            try
            {
                _context.Type.Update(Type);
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
