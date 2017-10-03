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
    /// The persons resource endpoint.
    /// </summary>
    public class PeopleController : ApiController
    {
        private IPeopleService _peopleService;
        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        // GET: api/persons
        /// <summary>
        /// Gets all persons
        /// </summary>
        /// <remarks>
        /// Gets a collection of all persons.
        /// </remarks>
        /// <returns>Person[] An array of persons.</returns>
        /// <seealso cref="Person" />
        /// <response code="200"></response>
        /// <response code="404">If Person not found</response>
        [ResponseType(typeof(IEnumerable<Person>))]
        public async Task<IHttpActionResult> Get([FromUri]QuerySpec querySpec)
        {
            try
            {
                var model = await _peopleService.Get(querySpec);

                if (model == null)
                    return NotFound();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/persons/5
        /// <summary>
        /// Get a person by Id
        /// </summary>
        /// <remarks>
        /// Gets a single person based on the requested Id.
        /// </remarks>
        /// <param name="id"> The person id to filter by.</param>
        /// <returns>Person</returns>
        /// <seealso cref="Person" />
        /// <response code="200"></response>
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var model = await _peopleService.Get(id);

                if (model == null)
                    return NotFound();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/persons
        /// <summary>
        /// Posts / Adds a person
        /// </summary>
        /// <remarks>
        /// Allows for adding a new person resource
        /// </remarks>
        /// <param name="model"> The Person request to be persisted.</param>
        /// <returns>Person</returns>
        /// <seealso cref="Person"/>
        /// <response code="201"></response>
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> Post([FromBody]Person model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _peopleService.Add(model);

                var createdLocation = Path.Combine(Request.RequestUri.ToString(), model.Id.ToString());

                return Created(createdLocation, model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/persons/5
        /// <summary>
        /// Puts / Updates a person
        /// </summary>
        /// <remarks>
        /// Allows for updating person resource
        /// </remarks>
        /// <param name="id"> The Person's id.</param>
        /// <param name="model"> The Person request to be updated.</param>
        /// <returns></returns>
        /// <seealso cref="Person"/>
        /// <response code="200"></response>
        public async Task<IHttpActionResult> Put(int id, [FromBody]Person model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != model.Id)
                    return BadRequest($"requested Id ({id}) and Model.Id ({model.Id}) do not match. Please verify and try again.");

                var updatedModel = await _peopleService.Update(model);

                if (updatedModel == null)
                    return NotFound();

                // For a put action we return Ok with no content.
                return Ok(updatedModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/persons/5
        /// <summary>
        /// Deletes a person
        /// </summary>
        /// <remarks>
        /// Allows for deleteing a person resource based on Id
        /// </remarks>
        /// <param name="id"> The person id requested to be deleted.</param>
        /// <returns></returns>
        /// <seealso cref="Person"/>
        /// <response code="200"></response>
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _peopleService.Delete(id);

                if (deleted == false)
                    return NotFound();

                // Return Ok no content.
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
