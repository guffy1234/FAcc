using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FuelAcc.WebApi
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; 
        public const string AUDIENCE = "MyAuthClient"; 
        private const string KEY = "mysupersecret_secretkey!12345678";   // encryption key. MUST be 256 bit!
        public const int LIFETIME = 3 * 60; // token lifetime 180 min

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}