using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bookify.Api.FunctionalTests.Infrastructure;
using Bookify.Application.Users.GetLoggedInUser;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shouldly;

namespace Bookify.Api.FunctionalTests.Users;

public class GetLoggedInUserTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    [Fact]
    public async Task Get_ShouldReturnUnauthorized_WhenAccessTokenIsMissing()
    {
        var response = await HttpClient.GetAsync("api/v1/users/me");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Get_ShouldReturnUser_WhenAccessTokenIsNotMissing()
    {
        var accessToken = await GetAccessToken();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            JwtBearerDefaults.AuthenticationScheme,
            accessToken);

        var user = await HttpClient.GetFromJsonAsync<UserResponse>("api/v1/users/me");

        user.ShouldNotBeNull();
    }
}
