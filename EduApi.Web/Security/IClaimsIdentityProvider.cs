using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace EduApi.Web.Security
{
    /// <summary>
    /// Provides a Claims Principal
    /// </summary>
    public interface IClaimsIdentityProvider
    {

        /// <summary>
        /// Gets a Claims Identity based off of a userId.
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <returns type="ClaimsIdentity">ClaimsIdentity</returns>
        ClaimsIdentity GetClaimsIdentity(string userId);
    }

    public class DefaultClaimsIdentityProvider : IClaimsIdentityProvider
    {
        private readonly IClaimsProvider _claimsProvider;

        public DefaultClaimsIdentityProvider(IClaimsProvider claimsProvider)
        {
            _claimsProvider = claimsProvider;
        }

        public ClaimsIdentity GetClaimsIdentity(string userId)
        {
            var claimsIdentity = new ClaimsIdentity();
            
            claimsIdentity.AddClaims(_claimsProvider.GetUserClaims(userId));

            return claimsIdentity;
        }
    }
}