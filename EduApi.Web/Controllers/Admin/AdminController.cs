using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EduApi.Web.Migrations;

namespace EduApi.Web.Controllers.Admin
{
    /// <summary>
    /// Admin endpoint.
    /// </summary>
    public class AdminController : ApiController
    {
        // GET: api/Admin
        /// <summary>
        /// Applies all migrations to the connection string configured database.
        /// </summary>
        /// <remarks>
        /// Allows EF migrations to be applied.
        /// </remarks>
        /// <param name="key"> The key part of the credentials. </param>
        /// <param name="secret"> The secret part of the credentials. </param>
        /// <returns></returns>
        /// <response code="200"></response>
        [Route("api/admin/migratedb")]
        public IHttpActionResult Post(string key, string secret)
        {
            try
            {
                if (key != "myKey" || secret != "1qaz@WSX")
                    return BadRequest("Bad credentials.");

                var configuration = new Configuration();
                var migrator = new DbMigrator(configuration);
                migrator.Update();
                

                return Ok("Migration applied successfully");
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
