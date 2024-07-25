using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using constprojectapi.Controllers;
using constprojectapi.Data;
using constprojectapi.Models;

namespace constprojectapi.Tests;

public class UsersControllerTests
{
    private readonly DbContextOptions<UserContext> _dbContextOptions;

    public UsersControllerTests()
    {
        // Configure in-memory database for testing
        _dbContextOptions = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async Task GetUsers_ReturnsAllUsers()
    {
        // Arrange
        await using var context = new UserContext(_dbContextOptions);
        context.Users.AddRange(
            new User { Id = 1, Email = "user1@example.com", Password = "password1", Salt = "salt1" },
            new User { Id = 2, Email = "user2@example.com", Password = "password2", Salt = "salt2" }
        );
        await context.SaveChangesAsync();

        var controller = new UsersController(context);

        // Act
        var result = await controller.GetUsers();

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var users = Assert.IsType<List<User>>(okResult.Value);

        Assert.Equal(2, users.Count);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        await using var context = new UserContext(_dbContextOptions);
        var controller = new UsersController(context);

        // Act
        var result = await controller.GetUser(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetUser_ReturnsUser_WhenUserExists()
    {
        // Arrange
        await using var context = new UserContext(_dbContextOptions);
        var user = new User { Id = 3, Email = "user1@example.com", Password = "password1", Salt = "salt1" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var controller = new UsersController(context);

        // Act
        var result = await controller.GetUser(3);

        // Assert
        var actionResult = Assert.IsType<ActionResult<User>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedUser = Assert.IsType<User>(okResult.Value);

        Assert.Equal(user.Id, returnedUser.Id);
        Assert.Equal(user.Email, returnedUser.Email);
        Assert.Equal(user.Password, returnedUser.Password);
        Assert.Equal(user.Salt, returnedUser.Salt);
    }
}