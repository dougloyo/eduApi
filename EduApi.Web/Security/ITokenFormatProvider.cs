using Microsoft.Owin.Security;

namespace EduApi.Web.Security
{
    public interface ITokenFormatProvider : ISecureDataFormat<AuthenticationTicket>
    {
    }
}