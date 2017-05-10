using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using EduApi.Web.Models;
using EduApi.Web.Services;

namespace EduApi.Web.Controllers
{
    public class StudentsController : ApiController
    {
        private IStudentsService _studentsService;
        public StudentsController()
        {
            _studentsService = new StudentsService();
        }
        // GET: api/Students
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var model = await _studentsService.Get();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Students/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var model = await _studentsService.Get(id);
                return Ok(model);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Students
        public async Task<IHttpActionResult> Post([FromBody]Student model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _studentsService.Add(model);

                var createdLocation = Path.Combine(Request.RequestUri.ToString(), model.Id.ToString());

                return Created(createdLocation, model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Students/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Student model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != model.Id)
                    return BadRequest($"requested Id ({id}) and Model.Id ({model.Id}) do not match. Please verify and try again.");

                await _studentsService.Update(model);

                // For a put action we return Ok with no content.
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // DELETE: api/Students/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                await _studentsService.Delete(id);
                // Return Ok no content.
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
