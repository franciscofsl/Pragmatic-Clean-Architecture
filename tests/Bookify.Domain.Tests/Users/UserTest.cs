using Bookify.Domain.Tests.Infrastructure;
using Bookify.Domain.Users;
using Bookify.Domain.Users.Events;
using Shouldly;

namespace Bookify.Domain.Tests.Users;

public class UserTest : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

        user.FirstName.ShouldBe(UserData.FirstName);
        user.LastName.ShouldBe(UserData.LastName);
        user.Email.ShouldBe(UserData.Email);
    }

    [Fact]
    public void Create_Should_RaiseUserCreatedDomainEvent()
    {
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

        var userCreatedDomainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

        userCreatedDomainEvent.UserId.ShouldBe(user.Id);
    }

    [Fact]
    public void Create_Should_AddRegisteredRoleToUser()
    {
        var user = User.Create(UserData.FirstName, UserData.LastName, UserData.Email);

        user.Roles.ShouldContain(Role.Registered);
    }
}