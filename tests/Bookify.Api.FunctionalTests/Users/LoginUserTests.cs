using System.Net;
using System.Net.Http.Json;
using Bookify.Api.Controllers.Users;
using Bookify.Api.FunctionalTests.Infrastructure;
using Shouldly;

namespace Bookify.Api.FunctionalTests.Users;

public class LoginUserTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private const string Email = "login@test.com";
    private const string Password = "12345";

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenUserDoesNotExist()
    {
        var request = new LogInUserRequest(Email, Password);

        var response = await HttpClient.PostAsJsonAsync("api/v1/users/login", request);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_ShouldReturnOk_WhenUserExists()
    {
        var registerRequest = new RegisterUserRequest(Email, "first", "last", Password);
        await HttpClient.PostAsJsonAsync("api/v1/users/register", registerRequest);

        var request = new LogInUserRequest(Email, Password);

        var response = await HttpClient.PostAsJsonAsync("api/v1/users/login", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}