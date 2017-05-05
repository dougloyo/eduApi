using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace EduApi.Web.Controllers
{
    public class StudentsController : ApiController
    {
        // GET: api/Students
        public async Task<IHttpActionResult> Get()
        {
            // await a new task.
            var model = await Task.Run(()=> new [] { "value1", "value2" });

            return Ok(model);
        }

        // GET: api/Students/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var model = await Task.Run(() => "Value");
            return Ok(model);
        }

        // POST: api/Students
        public async Task<IHttpActionResult> Post([FromBody]string value)
        {
            var model = value;
            
            var createdLocation = Path.Combine(Request.RequestUri.ToString(), model);

            return Created(createdLocation, model);
        }

        // PUT: api/Students/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]string requestModel)
        {
            // For a put action we return Ok with no content.
            return Ok();
        }

        // DELETE: api/Students/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            // Return Ok no content.
            return Ok();
        }
    }
}
