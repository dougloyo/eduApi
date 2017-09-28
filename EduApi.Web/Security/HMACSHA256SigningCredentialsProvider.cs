using System.IdentityModel.Tokens;
using System.Text;
using EduApi.Web.Providers;
using EduApi.Web.Security.JWT;

namespace EduApi.Web.Security
{
    // Following articles:
    //http://bitoftech.net/2014/10/27/json-web-token-asp-net-web-api-2-jwt-owin-authorization-server/
    //http://odetocode.com/blogs/scott/archive/2015/01/15/using-json-web-tokens-with-katana-and-webapi.aspx
    /// <summary>
    /// The HMAC SHA 256 signing credentials provider.
    /// </summary>
    public class HMACSHA256SigningCredentialsProvider : ISigningCredentialsProvider
    {
        private readonly IApplicationSettingsProvider _appSettings;
        private const string SignatureAlgorithm = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256";
        private const string DigestAlgorithm = "http://www.w3.org/2001/04/xmlenc#sha256";

        /// <summary>
        /// Constructor for the HMAC SHA 256 signing credentials provider.
        /// </summary>
        /// <param name="appSettings">The app settings provider</param>
        public HMACSHA256SigningCredentialsProvider(IApplicationSettingsProvider appSettings)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        /// Gets a SigningCredentials object configured with the HMAC SHA 256 algorithm.
        /// </summary>
        /// <returns>SigningCredentials</returns>
        public SigningCredentials GetSigningCredentials()
        {
            var hmacKey = _appSettings.GetValue("JWT.HmacSecretKey");
            return new SigningCredentials(
                new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(hmacKey)),
                SignatureAlgorithm,
                DigestAlgorithm);
        }
    }
}