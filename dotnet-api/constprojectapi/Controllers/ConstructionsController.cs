using constprojectapi.Data;
using constprojectapi.Dto;
using Microsoft.AspNetCore.Mvc;
using constprojectapi.Models;
using constprojectapi.Response;
using Microsoft.EntityFrameworkCore;

namespace constprojectapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConstructionsController : ControllerBase
{
  private readonly ConstructionContext _context;

  public ConstructionsController(ConstructionContext context)
  {
    _context = context;
  }

  // GET: api/constructions
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Construction>>> GetConstructions()
  {
    return await _context.Constructions.ToListAsync();
  }

  // GET: api/constructions/5
  [HttpGet("{id}")]
  public async Task<ActionResult<Construction>> GetConstruction(int id)
  {
    var construction = await _context.Constructions.FindAsync(id);

    if (construction == null)
    {
      return NotFound();
    }

    return construction;
  }

  // POST api/constructions
  [HttpPost]
  public async Task<ActionResult<Construction>> PostConstruction(ConstructionCreateDto constructionDto)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    if (!ValidateConstructionData(constructionDto, out var errorResponse))
    {
      return BadRequest(errorResponse);
    }

    var construction = new Construction
    {
      Name = constructionDto.Name,
      Stage = constructionDto.Stage,
      Category = constructionDto.Category,
      Location = constructionDto.Location,
      Details = constructionDto.Details,
      CreatorId = 1, //later, replace with data from JWT
    };
    construction.SetStartDateFromLocalTime(constructionDto.StartDate);

    _context.Constructions.Add(construction);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetConstruction), new { id = construction.Id }, construction);
  }

  // PUT api/constructions/5
  [HttpPut("{id}")]
  public async Task<IActionResult> PutConstruction(int id, ConstructionCreateDto constructionDto)
  {
    var construction = await _context.Constructions.FindAsync(id);
    if (construction == null)
    {
      return NotFound();
    }

    if (!ValidateConstructionData(constructionDto, out var errorResponse))
    {
      return BadRequest(errorResponse);
    }

    construction.Name = constructionDto.Name;
    construction.Stage = constructionDto.Stage;
    construction.Category = constructionDto.Category;
    construction.Location = constructionDto.Location;
    construction.Details = constructionDto.Details;
    construction.CreatorId = 1; //later, replace with data from JWT
    construction.SetStartDateFromLocalTime(constructionDto.StartDate);

    _context.Entry(construction).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return Ok(construction);
  }

  // DELETE api/constructions/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteConstruction(int id)
  {
    var construction = await _context.Constructions.FindAsync(id);

    if (construction == null)
    {
      return NotFound();
    }

    _context.Constructions.Remove(construction);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool ValidateConstructionData(ConstructionCreateDto constructionDto, out ErrorResponse errorResponse)
  {
    errorResponse = null;

    var _startDate = new DateTime { };

    if (constructionDto.StartDate.HasValue)
    {
      _startDate = TimeZoneInfo.ConvertTimeToUtc(constructionDto.StartDate.Value);
    }
    else
    {
      _startDate = DateTime.UtcNow;
    }

    if (constructionDto.Stage < 4 && _startDate <= DateTime.UtcNow)
    {
      errorResponse = new ErrorResponse
      {
        Message = "StartDate must be a future date for non Construction stage project."
      };
      return false;
    }

    return true;
  }

}