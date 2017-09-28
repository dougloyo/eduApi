using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Web;

namespace EduApi.Web.Security
{
    /// <summary>
    /// Claims Provider Interface
    /// </summary>
    public interface IClaimsProvider
    {
        /// <summary>
        /// Gets the claims attached to a user id.
        /// </summary>
        /// <param name="userId">The users unique identifier</param>
        /// <returns>List of Claims</returns>
        List<Claim> GetUserClaims(string userId);
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
        public List<Claim> GetUserClaims(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim("sub", userId),
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim(ClaimTypes.Email, "john@doe.com"),
                new Claim(ClaimTypes.Role, "Admin"),
            };

            return claims;
        }
    }
}