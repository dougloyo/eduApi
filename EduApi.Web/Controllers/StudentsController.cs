using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EduApi.Web.Models;
using EduApi.Web.Services;

namespace EduApi.Web.Controllers
{
    /// <summary>
    /// The Students resource endpoint.
    /// </summary>
    public class StudentsController : ApiController
    {
        private IStudentsService _studentsService;
        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        // GET: api/Students
        /// <summary>
        /// Gets all students
        /// </summary>
        /// <remarks>
        /// Gets a collection of all students.
        /// </remarks>
        /// <returns>Student[] An array of students.</returns>
        /// <seealso cref="Student" />
        /// <response code="200"></response>
        [ResponseType(typeof(IEnumerable<Student>))]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var model = await _studentsService.Get();
                return Ok(model);
            }
            catch (SqlException ex)
            {
                return InternalServerError(ex);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Students/5
        /// <summary>
        /// Get a student by Id
        /// </summary>
        /// <remarks>
        /// Gets a single student based on the requested Id.
        /// </remarks>
        /// <param name="id"> The student id to filter by.</param>
        /// <returns>Student</returns>
        /// <seealso cref="Student" />
        /// <response code="200"></response>
        [ResponseType(typeof(Student))]
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
        /// <summary>
        /// Posts / Adds a student
        /// </summary>
        /// <remarks>
        /// Allows for adding a new student resource
        /// </remarks>
        /// <param name="model"> The Student request to be persisted.</param>
        /// <returns>Student</returns>
        /// <seealso cref="Student"/>
        /// <response code="201"></response>
        [ResponseType(typeof(Student))]
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
        /// <summary>
        /// Puts / Updates a student
        /// </summary>
        /// <remarks>
        /// Allows for updating student resource
        /// </remarks>
        /// <param name="id"> The Student's id.</param>
        /// <param name="model"> The Student request to be updated.</param>
        /// <returns></returns>
        /// <seealso cref="Student"/>
        /// <response code="200"></response>
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
        /// <summary>
        /// Deletes a student
        /// </summary>
        /// <remarks>
        /// Allows for deleteing a student resource based on Id
        /// </remarks>
        /// <param name="id"> The student id requested to be deleted.</param>
        /// <returns></returns>
        /// <seealso cref="Student"/>
        /// <response code="200"></response>
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
