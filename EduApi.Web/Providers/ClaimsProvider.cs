using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace EduApi.Web.Providers
{
    public interface IClaimsProvider
    {
        List<Claim> GetUserClaims(int userId);
    }

    /// <summary>
    /// Gets user Claims
    /// </summary>
    /// <remarks>
    /// Gets a collection claims based on the user id.
    /// </remarks>
    /// <returns>List<Claim></Claim></returns>
    /// <seealso cref="Claim" />
    public class DefaultClaimsProvider : IClaimsProvider
    {
        public List<Claim> GetUserClaims(int userId)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, "John Doe"));
            claims.Add(new Claim(ClaimTypes.Email, "john@doe.com"));

            return claims;
        }
    }
}