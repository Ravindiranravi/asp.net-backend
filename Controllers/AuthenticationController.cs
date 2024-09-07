//using Institute.Data;
//using Institute.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Institute.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthenticationController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public AuthenticationController(ApplicationDbContext context)
//        {
//            _context = context;
//        }



//        [HttpPost("signin")]
//        public async Task<IActionResult> SignIn([FromBody] User model)
//        {
//            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
//            {
//                return BadRequest("Invalid request.");
//            }

//            var user = await _context.Users

//                .Include(u => u.Student) // Include Student if necessary
//                .Include(u => u.Trainee) // Include Trainee if necessary
//                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password && u.Role == model.Role);

//            if (user != null)
//            {
//                if (user.Role == "student" && user.Student != null)
//                {
//                    var redirectUrl = $"/student-dashboard/{user.Student.Id}";
//                    return Ok(new { Message = "Sign-in successful", RedirectUrl = redirectUrl, Student = user.Student });
//                }

//                if (user.Role == "trainee" && user.Trainee != null)
//                {
//                    var redirectUrl = $"/trainee-dashboard/{user.Trainee.Id}";
//                    return Ok(new { Message = "Sign-in successful", RedirectUrl = redirectUrl, Trainee = user.Trainee });
//                }

//                var roleRedirectUrl = user.Role switch
//                {
//                    "admin" => "/admin-dashboard",
//                    _ => "/unknown"
//                };

//                return Ok(new { Message = "Sign-in successful", RedirectUrl = roleRedirectUrl });
//            }

//            return Unauthorized("Invalid username or password.");
//        }

//    }
//} 


using Institute.Data;
using Institute.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Institute.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] User model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Invalid request.");
            }

            var user = await _context.Users
                .Include(u => u.Student) // Include Student if necessary
                .Include(u => u.Trainee) // Include Trainee if necessary
                .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password && u.Role == model.Role);

            if (user != null)
            {
                if (user.Role == "student" && user.Student != null)
                {
                    var redirectUrl = $"/student-dashboard/{user.Student.Id}";
                    return Ok(new { Message = "Sign-in successful", RedirectUrl = redirectUrl, Student = user.Student });
                }

                if (user.Role == "trainee" && user.Trainee != null)
                {
                    var redirectUrl = $"/trainee-dashboard/{user.Trainee.Id}";
                    return Ok(new { Message = "Sign-in successful", RedirectUrl = redirectUrl, Trainee = user.Trainee });
                }

                var roleRedirectUrl = user.Role switch
                {
                    "admin" => "/admin-dashboard",
                    _ => "/unknown"
                };

                return Ok(new { Message = "Sign-in successful", RedirectUrl = roleRedirectUrl });
            }

            return Unauthorized("Invalid username or password.");
        }
    }
}
