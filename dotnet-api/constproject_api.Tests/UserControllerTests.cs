using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Moq;
// using Xunit;
// using constproject_api.Controllers;
// using constproject_api.Data;
// using constproject_api.Models;

namespace constproject_api.Tests;

public class UsersControllerTests
{
    // private readonly Mock<IUserRepository> _mockRepo;
    // private readonly UserController _controller;

    // public UserControllerTests()
    // {
    //     _mockRepo = new Mock<IUserRepository>();
    //     _controller = new UserController(_mockRepo.Object);
    // }

    // [Fact]
    // public async Task GetUsers_ReturnsOkResult_WithListOfUsers()
    // {
    //     // Arrange
    //     var users = new List<User>
    //     {
    //         new User { Id = 1, Name = "John Doe" },
    //         new User { Id = 2, Name = "Jane Smith" }
    //     };
    //     _mockRepo.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(users);

    //     // Act
    //     IActionResult result = await _controller.GetUsers();

    //     // Assert
    //     var okResult = Assert.IsType<OkObjectResult>(result);
    //     var returnUsers = Assert.IsType<List<User>>(okResult.Value);
    //     Assert.Equal(2, returnUsers.Count);
    // }
}