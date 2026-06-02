

using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly AppDbContext _context;

    public PeopleController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<Person>> GetPeople()
    {
        try
        {
            var people = await _context.People.ToListAsync();
            return Ok(people);
        }
        catch (System.Exception ex)
        {
            
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetPerson")]
    public async Task<ActionResult<Person>> GetPerson(int id)
    {
        try
        {
            var person = await _context.People.FindAsync(id);
           if (person is null) return NotFound();
            return Ok(person);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
        }
    }



    [HttpPost]
    public async Task<ActionResult<Person>> AddPerson(Person person)
    {
        try
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(
                "GetPerson",
                new {id = person.PersonId},
                person
            );
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpPut("{id:int}")]
    public async Task<ActionResult<Person>> UpdatePerson(int id, [FromBody] Person person)
    {
        try
        {
            if (id == person.PersonId)
            {
                if (! await _context.People.AnyAsync(p => p.PersonId == id)) return NotFound();
                _context.People.Update(person);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return BadRequest("Id in url and body mismatches");
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Person>> DeletePerson(int id)
    {
        try
        {
            var person = await _context.People.FindAsync(id);
            if (person is null) return NotFound();
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}