using System;
using System.Linq;
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
            [ExpectedException(typeof(NotImplementedException))]
            public void Should_throw_exception_if_student_id_not_found()
            {
                IStudentsService service = new StudentsService();
                var suppliedStudentId = -1;
                var actualModel = service.Get(suppliedStudentId);
                // No assert is needed because we are expectind an exception
            }
        }
    }
}
