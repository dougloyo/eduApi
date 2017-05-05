using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduApi.Web.Models;

namespace EduApi.Web.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> Get();
        Student Get(int id);
        Student Add(Student model);
        void Update(Student model);
        void Delete(int id);
    }
    public class StudentsService : IStudentsService
    {
        List<Student> _students = new List<Student>
            {
                new Student { Id = 1, StudentId = "U101", FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 1, Grade = 2, Email = "john@mail.com" },
                new Student { Id = 2, StudentId = "U102", FirstName = "Jane", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 2, Grade = 2, Email = "jane@mail.com" },
            };

        public IEnumerable<Student> Get()
        {
            return _students;
        }

        public Student Get(int id)
        {
            var model = _students.SingleOrDefault(x => x.Id == id);
            return model;
        }

        public Student Add(Student model)
        {
            var maxId = _students.Max(x => x.Id);
            model.Id = maxId+1;
            _students.Add(model);
            return model;
        }

        public void Update(Student model)
        {
            var modelToUpdate = _students.SingleOrDefault(x => x.Id == model.Id);

            modelToUpdate.FirstName = model.FirstName;
        }

        public void Delete(int id)
        {
            var modelToDelete = _students.SingleOrDefault(x => x.Id == id);
            _students.Remove(modelToDelete);
        }
    }
}