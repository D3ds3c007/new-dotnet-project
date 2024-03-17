using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication1.Models; 
using WebApplication1.Data;
using WebApplication1.Models.DTO;// Assurez-vous de remplacer "WebApplication1" par le nom de votre espace de noms

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
        public IActionResult UpdateBio([FromBody] UserBioDTO modelDTO)
        {
            try
            {
                var model = modelDTO.GetUser();
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
        public IActionResult UpdatePhoto([FromBody] UserPdpPathDTO modelDTO)
        {
            try
            {

                var base64String = modelDTO.image;
                var imageBytes = Convert.FromBase64String(base64String);
                var imagePath = Path.Combine("Images/pdp", Guid.NewGuid().ToString() + ".jpg");
                System.IO.File.WriteAllBytes(imagePath, imageBytes);

                User model = modelDTO.GetUser();
                var user = _context.Users.FirstOrDefault(u => u.idUser == model.idUser);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                user.pdpPath = imagePath;
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
