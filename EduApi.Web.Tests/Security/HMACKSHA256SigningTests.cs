using System;
using EduApi.Web.Providers;
using EduApi.Web.Security;
using Moq;
using NUnit.Framework;

namespace EduApi.Web.Tests.Security
{
    public class HMACKSHA256SigningTests
    {
        [TestFixture]
        public class When_Generating_a_Signing_Credentials_Object
        {
            [Test]
            public void Should_Not_Blowup()
            {
                var suppliedApplicationSettingsProvider = new Mock<IApplicationSettingsProvider>();
                suppliedApplicationSettingsProvider
                    .Setup(x => x.GetValue(It.IsAny<string>()))
                    .Returns("db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==");

                var sut = new HMACSHA256SigningCredentialsProvider(suppliedApplicationSettingsProvider.Object);

                try
                {
                    var signingCreds = sut.GetSigningCredentials();
                    Assert.That(true);
                }
                catch (Exception ex)
                {
                    Assert.That(false,"An exception should not be thrown.");
                }


            }
        }
    }
}
