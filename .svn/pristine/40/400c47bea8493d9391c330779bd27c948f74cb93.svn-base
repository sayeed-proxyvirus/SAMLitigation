using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;
using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Services.ServiceImple
{
    public class UserTableServiceImple : UserTableService
    {
        private readonly SAMDbContext _context;

        public UserTableServiceImple(SAMDbContext context)
        {
            _context = context;
        }

        public UserTable GetUserByUsername(string username)
        {
            try
            {
                SqlParameter paramUserName = new SqlParameter("@UserName", username);

                var userTable = _context.UsersTable
                                    .FromSqlRaw("EXEC GetUserByUserName @UserName", paramUserName)
                                    .AsEnumerable()
                                    .FirstOrDefault();
                return userTable;
            }
            catch
            {
                throw;
            }
        }

        public List<UserRoleRelationViewModel> GetUserRoleByUserId(decimal UserId)
        {
            try
            {
                SqlParameter paramUserId = new SqlParameter("@UserId", UserId);

                var userRoles = _context.Set<UserRoleRelationViewModel>()
                             .FromSqlRaw("EXEC Projects_Processflow_GetUserRoleByUserID @UserId", paramUserId)
                             .ToList();
                return userRoles;
            }
            catch
            {
                throw;
            }
        }

        public bool ValidateUser(string username, string password)
        {
            try
            {
                SqlParameter paramUserName = new SqlParameter("@UserName", username);
                SqlParameter paramPassword = new SqlParameter("@Password", password);


                var result = _context.UsersTable
                            .FromSqlRaw("EXEC ValidateUser @UserName, @Password", paramUserName, paramPassword)
                            .AsEnumerable()
                            .FirstOrDefault();

                return result != null;


            }
            catch
            {
                throw;
            }
        }
    }
}
