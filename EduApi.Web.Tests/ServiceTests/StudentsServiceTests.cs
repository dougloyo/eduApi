using System;
using System.Linq;
using EduApi.Web.Models;
using EduApi.Web.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduApi.Web.Tests.ServiceTests
{
    public class StudentsServiceTests
    {
        [TestClass]
        public class When_getting_all_students
        {
            [TestMethod]
            public void Should_return_a_collection_of_students()
            {
                IStudentsService service = new StudentsService();
                var actualModel = service.Get();
                Assert.IsTrue(actualModel.Any());
            }
        }

        [TestClass]
        public class When_getting_a_student_by_id
        {
            [TestMethod]
            public void Should_return_a_the_student_with_the_requested_id()
            {
                IStudentsService service = new StudentsService();
                var suppliedStudentId = 1;
                var actualModel = service.Get(suppliedStudentId);
                Assert.IsTrue(actualModel.Id == suppliedStudentId);
            }
        }

        [TestClass]
        public class When_adding_a_student
        {
            [TestMethod]
            public void Should_return_a_the_student_with_an_id()
            {
                IStudentsService service = new StudentsService();
                var suppliedStudent = new Student { FirstName = "John", LastName = "Doe" };
                var actualModel = service.Add(suppliedStudent);
                Assert.IsTrue(actualModel.Id != 0);
            }
        }

        [TestClass]
        public class When_editing_a_student
        {
            [TestMethod]
            public void Should_update_student_info()
            {
                IStudentsService service = new StudentsService();
                
                // Get a student and update his name.
                var actualModel = service.Get(1);
                actualModel.FirstName = "Johnathan";

                // Call the update method
                service.Update(actualModel);

                // Get the model again to ensure it has been updated.
                var updatedModel = service.Get(1);

                Assert.IsTrue(actualModel.FirstName == updatedModel.FirstName);
            }
        }

        [TestClass]
        public class When_deleting_a_student
        {
            [TestMethod]
            public void Should_delete_student()
            {
                IStudentsService service = new StudentsService();

                service.Delete(1);

                // Get a student and update his name.
                var actualModel = service.Get(1);
                Assert.IsNull(actualModel);
            }
        }
    }
}
