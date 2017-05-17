using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EduApi.Web.Data;
using EduApi.Web.Models;
using Moq;

namespace EduApi.Web.Tests.EntityFrameworkMockingHelpers
{
    internal static class AsyncDatabaseContextMockingHelper
    {
        public static Mock<DatabaseContext> GetMockedStudentDatabaseContext()
        {
            return GetListOf2Students().GetMockedStudentDatabaseContext();
        }

        public static List<Student> GetListOf2Students()
        {
            return new List<Student>
                        {
                            new Student { Id = 1, StudentId = "U101", FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 1, Grade = 2, Email = "john@mail.com" },
                            new Student { Id = 2, StudentId = "U102", FirstName = "Jane", LastName = "Doe", DateOfBirth = DateTime.Parse("1/1/2009"), Gender = 2, Grade = 2, Email = "jane@mail.com" },
                        };
        }

        public static Mock<DatabaseContext> GetMockedStudentDatabaseContext(DbSet<Student> dbSet)
        {
            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Students).Returns(dbSet);
            return mockContext;
        }

        public static Mock<DatabaseContext> GetMockedStudentDatabaseContext(this List<Student> data)
        {
            var dbSet = data.GetAsyncMockedDbSet();

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(c => c.Students).Returns(dbSet.Object);

            return mockContext;
        }

        public static Mock<DbSet<Student>> GetStudentDbSet()
        {
            return GetListOf2Students().GetAsyncMockedDbSet();
        }

        public static Mock<DbSet<T>> GetAsyncMockedDbSet<T>(this List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();

            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<T>(queryableData.Provider));
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            return mockDbSet;
        }
    }
}
