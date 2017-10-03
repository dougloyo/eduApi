using System;
using System.Linq;
using System.Threading.Tasks;
using EduApi.Web.Models;
using EduApi.Web.Services;
using EduApi.Web.Tests.EntityFrameworkMockingHelpers;
using Moq;
using NUnit.Framework;

namespace EduApi.Web.Tests.ServiceTests
{
    public class StudentsServiceTests
    {
        [TestFixture]
        public class When_getting_all_students
        {
            [Test]
            public async Task Should_return_a_collection_of_students()
            {
                var suppliedDatabaseContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext();
                IPeopleService service = new PeopleService(suppliedDatabaseContext.Object);
                var actualModel = await service.Get(new QuerySpec());
                Assert.IsTrue(actualModel.Any());
            }
        }

        [TestFixture]
        public class When_getting_a_student_by_id
        {
            [Test]
            public async Task Should_return_a_the_student_with_the_requested_id()
            {
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext();
                IPeopleService service = new PeopleService(suppliedContext.Object);
                var suppliedStudentId = 1;
                var actualModel = await service.Get(suppliedStudentId);
                Assert.IsTrue(actualModel.Id == suppliedStudentId);
            }
        }

        [TestFixture]
        public class When_adding_a_student
        {
            [Test]
            public async Task Should_return_the_student_that_was_persisted()
            {
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext();
                IPeopleService service = new PeopleService(suppliedContext.Object);
                var suppliedStudent = new Person { FirstName = "John", LastName = "Doe" };
                var actualModel = await service.Add(suppliedStudent);

                // Note: We could use reflection to ensure all properties are mapped.
                Assert.AreEqual(actualModel.FirstName, suppliedStudent.FirstName);
                Assert.AreEqual(actualModel.LastName, suppliedStudent.LastName);
            }
        }

        [TestFixture]
        public class When_editing_a_student
        {
            [Test]
            public async Task Should_update_student_info()
            {
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext();
                IPeopleService service = new PeopleService(suppliedContext.Object);

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

        [TestFixture]
        public class When_deleting_a_student
        {
            [Test]
            public async Task Should_delete_student()
            {
                var suppliedStudentDbSet = AsyncDatabaseContextMockingHelper.GetStudentDbSet();
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext(suppliedStudentDbSet.Object);

                IPeopleService service = new PeopleService(suppliedContext.Object);
                await service.Delete(1);

                var actualDeletedStudent = suppliedContext.Object.People.Single(x => x.Id == 1);
                suppliedStudentDbSet.Verify<Person>(m => m.Remove(actualDeletedStudent), Times.Once);
                suppliedContext.Verify(m => m.SaveChangesAsync(), Times.Once);
            }

            [Test]
            public void Should_throw_exception_if_student_id_does_NOT_exist()
            {
                var suppliedContext = AsyncDatabaseContextMockingHelper.GetMockedStudentDatabaseContext();
                IPeopleService service = new PeopleService(suppliedContext.Object);
                
                var exception = Assert.ThrowsAsync<InvalidOperationException>(async () => await service.Delete(-99));

                Assert.That(exception.Message,Is.EqualTo("Not found: requested entity id was not found."));
            }
        }
    }
}
