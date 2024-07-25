using constprojectapi.Data;
using constprojectapi.Dto;
using Microsoft.AspNetCore.Mvc;
using constprojectapi.Models;
using constprojectapi.Response;
using Microsoft.EntityFrameworkCore;

namespace constprojectapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly UserContext _context;

  public UsersController(UserContext context)
  {
    _context = context;
  }

  // GET: api/users
  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetUsers()
  {
    var users = await _context.Users.ToListAsync();
    return Ok(users);
  }

  // GET: api/users/5
  [HttpGet("{id}")]
  public async Task<ActionResult<User>> GetUser(int id)
  {
    var user = await _context.Users.FindAsync(id);

    if (user == null)
    {
      return NotFound();
    }

    return Ok(user);
  }

  // POST api/users
  [HttpPost]
  public async Task<ActionResult<User>> PostUser(UserCreateDto userDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    if (EmailExists(userDto.Email))
    {
      var errorResponse = new ErrorResponse
      {
        Message = "Email already in use."
      };
      return Conflict(errorResponse);
    }

    // Create a new User object
    var user = new User
    {
      Email = userDto.Email
    };

    // Generate a salt and hash the password
    string salt = constprojectapi.Helpers.PasswordHelper.GenerateSalt();
    string hashedPassword = constprojectapi.Helpers.PasswordHelper.HashPassword(userDto.Password, salt);

    // Set the salt and hashed password on the user object
    user.Salt = salt;
    user.Password = hashedPassword;

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
  }

  // DELETE api/users/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(int id)
  {
    var user = await _context.Users.FindAsync(id);

    if (user == null)
    {
      return NotFound();
    }

    _context.Users.Remove(user);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  [HttpPost("login")]
  public async Task<ActionResult> Login(LoginDto loginDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
    if (user == null)
    {
      return Unauthorized(new ErrorResponse { Message = "Invalid email or password." });
    }

    if (!constprojectapi.Helpers.PasswordHelper.VerifyPassword(loginDto.Password, user.Password, user.Salt))
    {
      return Unauthorized(new ErrorResponse { Message = "Invalid email or password." });
    }

    // Assuming you have a method to generate JWT token
    var token = GenerateJwtToken(user);

    return Ok(new { Token = token });
  }

  private string GenerateJwtToken(User user)
  {
    // Generate JWT token logic here
    // This typically involves creating a security token and signing it with a secret key
    // This is just a placeholder for illustration
    return "generated-jwt-token";
  }


  private bool EmailExists(string email)
  {
    return _context.Users.Any(u => u.Email == email);
  }


  // dummy endpoint to test the database connection
  [HttpGet("test")]
  public string Test()
  {
    return "Sampai nanti";
  }
}