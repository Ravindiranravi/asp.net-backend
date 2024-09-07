using Institute.Data;
using Institute.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;



namespace Institute.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "student")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get the current student's information
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentStudent()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                return BadRequest("Invalid user ID.");
            }

            var student = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId && u.Role == "student");

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(student);
        }
    }
}


//using Institute.Data;
//using Institute.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;

//namespace Institute.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class StudentController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public StudentController(ApplicationDbContext context)
//        {
//            _context = context;
//        }


//        // GET: api/Student/Details/{id}
//[HttpGet("Details/{id}")]
//public async Task<IActionResult> GetStudentDetails(int id)
//{
//    var student = await _context.Students.FindAsync(id);
//    if (student == null)
//    {
//        return NotFound();
//    }
//    return Ok(student);
//}

//        // POST: api/Student/Register
//        [HttpPost("Register")]
//        public async Task<IActionResult> RegisterStudent([FromBody] Student student)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            // Check if student already exists (optional)
//            var existingStudent = await _context.Students
//                .FirstOrDefaultAsync(s => s.Email == student.Email);

//            if (existingStudent != null)
//            {
//                return Conflict("Student with this email already exists.");
//            }

//            // Save the student to the database
//            _context.Students.Add(student);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction(nameof(RegisterStudent), new { id = student.Id }, student);
//        }
//    }
//}
