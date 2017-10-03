using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EduApi.Web.Controllers
{
    [Authorize]
    //TODO: Create a custom Authorize attribute that will read the JWT Claims and Authorize the request.
    //[AuthorizeByRole("Admin","Teacher")]
    public class TestSecuredController : ApiController
    {
        // GET: api/TestSecured
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
