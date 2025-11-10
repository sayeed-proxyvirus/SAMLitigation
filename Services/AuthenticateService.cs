using SAMLitigation.Models;

namespace SAMLitigation.Services
{
    public interface AuthenticateService
    {
        UserTable AuthenticateAsync(string username, string password);
    }
}
