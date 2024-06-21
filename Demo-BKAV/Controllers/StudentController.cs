using Demo_BKAV;    
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/student
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents([FromQuery] string name, [FromQuery] string sortBy)
    {
        var studentsQuery = _context.Students.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            studentsQuery = studentsQuery.Where(s => s.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy.ToLower())
            {
                case "name":
                    studentsQuery = studentsQuery.OrderBy(s => s.Name);
                    break;
                case "id":
                    studentsQuery = studentsQuery.OrderBy(s => s.idStudent);
                    break;
                default:
                    studentsQuery = studentsQuery.OrderBy(s => s.idStudent);
                    break;
            }
        }

        return await studentsQuery.ToListAsync();
    }

    // GET: api/student/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students
                                    .Where(s => s.idStudent == id)
                                    .FirstOrDefaultAsync();

        if (student == null)
        {
            return NotFound();
        }

        return student;
    }

    // POST: api/student
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudent), new { id = student.idStudent }, student);
    }

    // PUT: api/student/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, Student student)
    {
        if (id != student.idStudent)
        {
            return BadRequest();
        }

        _context.Entry(student).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/student/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students
                                    .Where(s => s.idStudent == id)
                                    .FirstOrDefaultAsync();

        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.idStudent == id);
    }
}
