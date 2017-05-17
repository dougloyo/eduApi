using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduApi.Web.Models;
using EduApi.Web.Services;
using EduApi.Web.Tests.EntityFrameworkMockingHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EduApi.Web.Tests.ServiceTests
{
    [TestClass]
    public class StudentsServiceTests
    {
        [TestClass]
        public class When_getting_all_students
        {
            [TestMethod]
            public async Task Should_return_a_collection_of_students()
            {
                var suppliedDatabaseContext = GetSuppliedStudentData().GetMockedStudentDatabaseContext();
                IStudentsService service = new StudentsService(suppliedDatabaseContext.Object);
                var actualModel = await service.Get();
                Assert.IsTrue(actualModel.Any());
            }

            private List<Student> GetSuppliedStudentData()
            {
                return 
                    new List<Student>
                        {
                            new Student { Id = 1, StudentId = "U101", FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 1, Grade = 2, Email = "john@mail.com" },
                            new Student { Id = 2, StudentId = "U102", FirstName = "Jane", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 2, Grade = 2, Email = "jane@mail.com" },
                        };
            }
        }

        [TestClass]
        public class When_getting_a_student_by_id
        {
            [TestMethod]
            public async Task Should_return_a_the_student_with_the_requested_id()
            {
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext();
                IStudentsService service = new StudentsService(suppliedContext.Object);
                var suppliedStudentId = 1;
                var actualModel = await service.Get(suppliedStudentId);
                Assert.IsTrue(actualModel.Id == suppliedStudentId);
            }
        }

        [TestClass]
        public class When_adding_a_student
        {
            [TestMethod]
            public async Task Should_return_the_student_that_was_persisted()
            {
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext();
                IStudentsService service = new StudentsService(suppliedContext.Object);
                var suppliedStudent = new Student { FirstName = "John", LastName = "Doe" };
                var actualModel = await service.Add(suppliedStudent);
                Assert.AreEqual(actualModel.FirstName, suppliedStudent.FirstName);
                Assert.AreEqual(actualModel.LastName, suppliedStudent.LastName);
                //Assert.IsTrue(actualModel.Id != 0);
            }
        }

        [TestClass]
        public class When_editing_a_student
        {
            [TestMethod]
            public async Task Should_update_student_info()
            {
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext();
                IStudentsService service = new StudentsService(suppliedContext.Object);

                // Get a student and update his name.
                var actualModel = await service.Get(1);
                actualModel.FirstName = "Johnathan";

                // Call the update method
                await service.Update(actualModel);

                // Get the model again to ensure it has been updated.
                var updatedModel = await service.Get(1);

                Assert.IsTrue(actualModel.FirstName == updatedModel.FirstName);
            }
        }

        [TestClass]
        public class When_deleting_a_student
        {
            [TestMethod]
            public async Task Should_delete_student()
            {
                var suppliedStudentDbSet = AsyncDatabaseContextMockingHelper.GetStudentDbSet();
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext(suppliedStudentDbSet.Object);

                IStudentsService service = new StudentsService(suppliedContext.Object);
                await service.Delete(1);

                var actualDeletedStudent = suppliedContext.Object.Students.Single(x => x.Id == 1);
                suppliedStudentDbSet.Verify<Student>(m => m.Remove(actualDeletedStudent), Times.Once);
                suppliedContext.Verify(m => m.SaveChangesAsync(), Times.Once);
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException), "Not deleted: requested student Id was not found.")]
            public async Task Should_throw_exception_if_student_id_does_NOT_exist()
            {
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext();
                IStudentsService service = new StudentsService(suppliedContext.Object);
                await service.Delete(-99);
            }
        }
    }
}
