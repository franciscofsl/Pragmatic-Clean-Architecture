using System.Net;
using System.Net.Http.Json;
using Bookify.Api.Controllers.Users;
using Bookify.Api.FunctionalTests.Infrastructure;
using Shouldly;

namespace Bookify.Api.FunctionalTests.Users;

public class RegisterUserTests : BaseFunctionalTest
{
    public RegisterUserTests(FunctionalTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Theory]
    [InlineData("", "first", "last", "12345")]
    [InlineData("test.com", "first", "last", "12345")]
    [InlineData("@test.com", "first", "last", "12345")]
    [InlineData("test@", "first", "last", "12345")]
    [InlineData("test@test.com", "", "last", "12345")]
    [InlineData("test@test.com", "first", "", "12345")]
    [InlineData("test@test.com", "first", "last", "")]
    [InlineData("test@test.com", "first", "last", "1")]
    [InlineData("test@test.com", "first", "last", "12")]
    [InlineData("test@test.com", "first", "last", "123")]
    [InlineData("test@test.com", "first", "last", "1234")]
    public async Task Register_ShouldReturnBadRequest_WhenRequestIsInvalid(
        string email,
        string firstName,
        string lastName,
        string password)
    {
        var request = new RegisterUserRequest(email, firstName, lastName, password);

        var response = await HttpClient.PostAsJsonAsync("api/v1/users/register", request);

        response.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenRequestIsValid()
    {
        var request = new RegisterUserRequest("create@test.com", "first", "last", "12345");

        var response = await HttpClient.PostAsJsonAsync("api/v1/users/register", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}