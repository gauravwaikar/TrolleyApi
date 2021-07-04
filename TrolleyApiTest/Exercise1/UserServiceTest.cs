using AutoFixture;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using TrolleyApi.Excercise1;
using TrolleyApiTest.Common;
using Xunit;
using FluentAssertions;

namespace TrolleyApiTest.Exercise1
{
    public class UserServiceTest
    {
        private readonly IUserService _sut;
        private readonly IConfiguration _configuration;

        public UserServiceTest()
        {
            var fixtrue = FixtureBuilder.Build();
            _configuration = fixtrue.Freeze<IConfiguration>();
            _sut = fixtrue.Create<UserService>();
        }

        [Fact]
        public void When_Get_Returns_ValidResponse()
        {
            // Arrange.
            _configuration["UserToken"].Returns("User-Token");

            // Act.
            var response = _sut.Get();

            // Assert.
            response.Name.Should().Be("Gaurav Waikar");
            response.Token.Should().Be("User-Token");
        }
    }
}
