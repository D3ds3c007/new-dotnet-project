using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication1.Models; 
using WebApplication1.Data;// Assurez-vous de remplacer "WebApplication1" par le nom de votre espace de noms

namespace WebApplication1.Controllers // Assurez-vous de remplacer "WebApplication1" par le nom de votre espace de noms
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
    //    /* 
       private readonly apiContext _context;

        public UserInfoController(apiContext context)
        {
            _context = context;
        }

        // PUT: api/User/bio
        [HttpPut("bio")]
        public IActionResult UpdateBio([FromBody] User model)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.idUser == model.idUser);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                user.bio = model.bio;
                _context.SaveChanges();

                return Ok("Bio updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // PUT: api/User/photo
        [HttpPut("photo")]
        public IActionResult UpdatePhoto([FromBody] User model)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.idUser == model.idUser);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                user.pdpPath = model.pdpPath;
                _context.SaveChanges();

                return Ok("Photo updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        // */
    }
}
