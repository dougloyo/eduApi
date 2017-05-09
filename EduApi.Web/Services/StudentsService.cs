using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        List<Student> _students = new List<Student>
            {
                new Student { Id = 1, StudentId = "U101", FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 1, Grade = 2, Email = "john@mail.com" },
                new Student { Id = 2, StudentId = "U102", FirstName = "Jane", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 2, Grade = 2, Email = "jane@mail.com" },
            };

        public async Task<IEnumerable<Student>> Get()
        {
            var model = await Task.Run(() => _students.AsEnumerable());
            return model;
        }

        public async Task<Student> Get(int id)
        {
            var model = await Task.Run(() => _students.SingleOrDefault(x => x.Id == id));
            return model;
        }

        public async Task<Student> Add(Student model)
        {
            var maxId = _students.Max(x => x.Id);
            model.Id = maxId+1;
            await Task.Run(() => _students.Add(model));
            return model;
        }

        public async Task Update(Student model)
        {
            var modelToUpdate = _students.SingleOrDefault(x => x.Id == model.Id);

            if (modelToUpdate == null)
                throw new InvalidOperationException();

            await Task.Run(() => modelToUpdate.FirstName = model.FirstName);
        }

        public async Task Delete(int id)
        {
            var modelToDelete = _students.SingleOrDefault(x => x.Id == id);

            if (modelToDelete == null)
                throw new InvalidOperationException();

            await Task.Run(() => _students.Remove(modelToDelete));
        }
    }
}