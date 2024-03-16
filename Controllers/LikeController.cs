using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Data;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers // Assurez-vous de remplacer "WebApplication1" par le nom de votre espace de noms
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly apiContext _context;

        public LikeController(apiContext context)
        {
            _context = context;
        }
        // GET: api/Like/like/{id}
        [HttpGet("/image/{id}")]
        public IActionResult GetLikeOf(int id)
        {
            try
            {
                // Get the sum of like for the specified photo id
                var totalLike = _context.Like.Where(l => l.idPicture == id).Sum(l => 1); // Sum of like is simply the count of like for the given photo
                return Ok(totalLike);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // POST: api/Like
        [HttpPost("add")]
        public IActionResult Like([FromBody] LikeDTO likeDTO)
        {
            try
            {
                if (likeDTO == null)
                {
                    return BadRequest("Like object is null");
                }
                var like = likeDTO.GetLike();
                // Check if the user already liked the picture
                var existingLike = _context.Like.FirstOrDefault(l => l.idUser == like.idUser && l.idPicture == like.idPicture);
                if (existingLike != null)
                {
                    // If like already exists, remove it (unlike)
                    _context.Like.Remove(existingLike);
                    _context.SaveChanges();
                    return Ok("Like removed successfully");
                }
                
                _context.Like.Add(like);
                _context.SaveChanges();

                return Ok("Like added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
