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
        public IEnumerable<Student> Get()
        {
            return new[]
            {
                new Student { Id = 1, StudentId = "U101", FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 1, Grade = 2, Email = "john@mail.com" },
                new Student { Id = 2, StudentId = "U102", FirstName = "Jane", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 2, Grade = 2, Email = "jane@mail.com" },
            };
        }

        public Student Get(int id)
        {
            throw new NotImplementedException();
        }

        public Student Add(Student model)
        {
            throw new NotImplementedException();
        }

        public void Update(Student model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}