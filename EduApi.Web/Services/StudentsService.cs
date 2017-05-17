using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using EduApi.Web.Data;
using EduApi.Web.Models;

namespace EduApi.Web.Services
{
    public interface IStudentsService
    {
        Task<IEnumerable<Student>> Get();
        Task<Student> Get(int id);
        Task<Student> Add(Student model);
        Task Update(Student model);
        Task Delete(int id);
    }
    public class StudentsService : IStudentsService
    {
        private readonly DatabaseContext _db;
        public StudentsService(DatabaseContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<IEnumerable<Student>> Get()
        {
            var model = await _db.Students.ToListAsync();
            return model;
        }

        public async Task<Student> Get(int id)
        {
            var model = await _db.Students.SingleOrDefaultAsync(x=>x.Id==id);

            if (model == null)
                throw new InvalidOperationException("Not found: requested entity id was not found.");

            return model;
        }

        public async Task<Student> Add(Student model)
        {
            _db.Students.Add(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task Update(Student model)
        {
            var modelToUpdate = await _db.Students.SingleOrDefaultAsync(x=>x.Id==model.Id);

            if (modelToUpdate == null)
                throw new InvalidOperationException("Not found: requested entity id was not found.");

            // Note: Why not use _db.Entry(modelToUpdate).CurrentValues.SetValues(model) ? 
            // Because it only maps first level values and not nested children like Student.Addresses.
            // TODO: Map with automapper.
            modelToUpdate.StudentId = model.StudentId;
            modelToUpdate.FirstName = model.FirstName;
            modelToUpdate.LastName = model.LastName;
            modelToUpdate.DateOfBirth = model.DateOfBirth;
            modelToUpdate.Email = model.Email;
            modelToUpdate.Gender = model.Gender;
            modelToUpdate.Grade = model.Grade;
            

            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var modelToDelete = await _db.Students.SingleOrDefaultAsync(x => x.Id == id);

            if (modelToDelete == null)
                throw new InvalidOperationException("Not found: requested entity id was not found.");

            _db.Students.Remove(modelToDelete);

            await _db.SaveChangesAsync();
        }
    }
}