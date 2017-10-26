using System;
using System.Collections;
using System.Linq;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using EduApi.Web.Data;
using EduApi.Web.Data.Helpers;
using EduApi.Web.Models;

namespace EduApi.Web.Services
{
    public interface IPeopleService
    {
        Task<IEnumerable> Get(QuerySpec querySpec);
        Task<Person> Get(int id);
        Task<Person> Add(Person model);
        Task<Person> Update(Person model);
        Task<bool> Delete(int id);
    }

    // TODO: Separate into its own project. EduApi.Services
    /// <summary>
    /// The People Service that allows CRUD.
    /// </summary>
    public class PeopleService : IPeopleService
    {
        private readonly DatabaseContext _db;
        public PeopleService(DatabaseContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable> Get(QuerySpec querySpec)
        {
            // When paginating it would be great to know how many records are total.
            // Options:
            //    1) We could wrap in envelope: { data:[], _metadata:{ totalCount: 100, limit:25, offset:3 } }
            //       Drawback: you now need to do something like students.items to get to the data.
            //    2) We could use custom headers: x-total-count: 100, x-limit:25, x-offset:3
            //       Drawback: you need special logic to pull from the headers.
            //    3) We could use link: <a href="resource?offset=2&limit=25" rel="next"> 
            //                          <a href="resource?offset=1&limit=25" rel="prev">
            //                          <a href="resource?offset=9&limit=25" rel="last">
            //       Drawback: Where does total go?  
            //var total = await _db.People.Where(x => x.Deleted == false).CountAsync();

            var total = await _db.People.Where(x => x.Deleted == false).CountAsync();

            IQueryable query = _db.People.Where(x=>!x.Deleted)
                                     .Where(querySpec.Filter)
                                     .OrderBy(querySpec.OrderBy)
                                     .Skip(querySpec.PageNumber)
                                     .Take(querySpec.PageSize);

            if (!string.IsNullOrEmpty(querySpec.Select))
                query = query.Select("new(" + querySpec.Select + ")");

            return await query.ToListAsync();
        }

        public async Task<Person> Get(int id)
        {
            var model = await _db.People.SingleOrDefaultAsync(x=>x.Id==id && x.Deleted==false);

            return model;
        }

        public async Task<Person> Add(Person model)
        {
            model.SetInsertAuditFields();
            _db.People.Add(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<Person> Update(Person model)
        {
            var modelToUpdate = await _db.People.SingleOrDefaultAsync(x=>x.Id==model.Id);

            if (modelToUpdate == null)
                return null;

            // Note: Why not use _db.Entry(modelToUpdate).CurrentValues.SetValues(model) ? 
            // Because it only maps first level values and not nested children like Person.Addresses.
            // TODO: Map with automapper.
            modelToUpdate.PersonKey = model.PersonKey;
            modelToUpdate.FirstName = model.FirstName;
            modelToUpdate.LastName = model.LastName;
            modelToUpdate.DateOfBirth = model.DateOfBirth;
            modelToUpdate.Email = model.Email;
            modelToUpdate.GenderAtBirth = model.GenderAtBirth;
            
            modelToUpdate.SetUpdateAuditFields();

            await _db.SaveChangesAsync();

            return modelToUpdate;
        }

        public async Task<bool> Delete(int id)
        {
            var modelToDelete = await _db.People.SingleOrDefaultAsync(x => x.Id == id);

            if (modelToDelete == null)
                return false;

            // Hard Delete:
            //_db.Students.Remove(modelToDelete);

            // Soft Delete:
            modelToDelete.Deleted = true;
            modelToDelete.DeletedUtcDate = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return true;
        }
    }
}