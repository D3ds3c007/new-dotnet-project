using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models; // Assurez-vous d'importer le namespace correspondant à vos modèles
using WebApplication1.Data; // Assurez-vous d'importer le namespace correspondant à votre contexte de base de données

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPersonalPageController : ControllerBase
    {
        private readonly apiContext _context;

        public UserPersonalPageController(apiContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.idUser == id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{userId}/numberOfArtwork")]
        public IActionResult GetNumberOfArtwork(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.idUser == userId);
                if (user == null)
                {
                    return NotFound();
                }
                var numberOfArtwork = user.pictures.Count();
                return Ok(numberOfArtwork);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{userId}/mostUsedCategories")]
        public IActionResult GetMostUsedCategories(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.idUser == userId);
                if (user == null)
                {
                    return NotFound();
                }
                var mostUsedCategories = user.pictures
                    .SelectMany(p => p.categoryPictures)
                    .GroupBy(cp => cp.category)
                    .OrderByDescending(g => g.Count())
                    .Take(3)
                    .Select(g => g.Key);
                return Ok(mostUsedCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{userId}/artworkByCategory/{categoryId}")]
        public IActionResult GetArtworkByCategory(int userId, int categoryId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.idUser == userId);
                if (user == null)
                {
                    return NotFound();
                }
                var artwork = user.pictures
                    .Where(p => p.categoryPictures.Any(cp => cp.idCategory == categoryId))
                    .ToList();
                return Ok(artwork);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{userId}/randomArtwork")]
        public IActionResult GetRandomArtwork(int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.idUser == userId);
                if (user == null)
                {
                    return NotFound();
                }
                var randomArtwork = user.pictures.OrderBy(x => Guid.NewGuid()).Take(2).ToList();
                return Ok(randomArtwork);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
