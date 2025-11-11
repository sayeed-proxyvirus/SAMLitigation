using SAMLitigation.Models;
using SAMLitigation.Models.ApplicationDbContext;
using System.Security.Cryptography;
using System.Text;

namespace SAMLitigation.Services.ServiceImple
{
    public class AuthenticateServiceImple : AuthenticateService
    {
        private readonly SAMDbContext _context;
        private readonly UserTableService _userTableService;

        public AuthenticateServiceImple(SAMDbContext context, UserTableService userTableService)
        {
            _context = context;
            _userTableService = userTableService;
        }


        public UserTable AuthenticateAsync(string username, string password)
        {
            try
            {
                bool isValid = _userTableService.ValidateUser(username, Encrypt(password, true));

                if (!isValid)
                    return null;

                var user = _userTableService.GetUserByUsername(username);
                return user;
            }
            catch
            {
                throw;
            }
        }



        private string Encrypt(string toEncrypt, bool useHashing)
        {
            try
            {

                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

                //AppSettingsReader settingsReader = new AppSettingsReader();

                string key = "GenericIDCOLSECURITy";
                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
