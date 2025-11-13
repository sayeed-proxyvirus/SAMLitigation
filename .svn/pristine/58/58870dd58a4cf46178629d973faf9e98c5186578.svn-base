using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;

namespace SAMLitigation.Services.ServiceImple
{
    public class LitigationActionServiceImple : LitigationActionService
    {
        private readonly SAMDbContext _context;
        public LitigationActionServiceImple(SAMDbContext context)
        {
            _context = context;
        }
        public SAM_Litigation_Action GetActionById(decimal Id)
        {
            try
            {
                var action = _context.LitigationAction
                //.Include(l => l.LawyerName)
                .FirstOrDefault(l => l.LitigationActionID == Id);

                return action;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<SAM_Litigation_Action> GetActionALL()
        {
            try
            {
                var action = _context.LitigationAction
                    .FromSqlRaw("EXEC GetActionALL")
                    .ToList();
                return action;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AddAction(SAM_Litigation_Action Action)
        {
            try
            {
                _context.LitigationAction.Add(Action);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateAction(SAM_Litigation_Action Action)
        {
            try
            {
                _context.LitigationAction.Update(Action);
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
