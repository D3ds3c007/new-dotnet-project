using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Models; 
using WebApplication1.Data; 

namespace WebApplication1.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly apiContext _context;

        public CommentsController(apiContext context)
        {
            _context = context;
        }

        // POST: api/Comments
        [HttpPost]
        public IActionResult CreateComments([FromBody] CommentsDTO commentsDTO)
        {
            try
            {
                Comments comments = commentsDTO.GetComments();
                if (comments == null)
                {
                    return BadRequest("Comments object is null");
                }

                _context.Comment.Add(comments);
                _context.SaveChanges();

                return Ok("Comments created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        // PUT: api/Comments/{id}/comments
        [HttpPut("{id}/comments")]
        public IActionResult UpdateCommentsText(int id, [FromBody] string updatedCommentsText)
        {
            try
            {
                var comments = _context.Comment.FirstOrDefault(c => c.idComment == id);
                if (comments == null)
                {
                    return NotFound("Comments not found");
                }

                comments.comment = updatedCommentsText;
                _context.SaveChanges();

                return Ok("Comments text updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        


    }
}
