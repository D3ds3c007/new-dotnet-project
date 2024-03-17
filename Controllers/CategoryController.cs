using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly apiContext _context;

        public CategoryController(apiContext context)
        {
            _context = context;
        }

        // POST: api/Category       
        [HttpGet("category/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var category = _context.Category
                    .FirstOrDefault(c => c.idCategory == id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, retourner une réponse avec le statut 500 (Internal Server Error) et un message d'erreur
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet("category")]
        public IActionResult GetAllCategories()
        {
            try
            {
                // Récupérer toutes les catégories de la base de données
                var categories = _context.Category.ToList();
                // Retourner la liste des données des catégories sous forme de réponse OK avec le statut 200
                return Ok(categories);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, retourner une réponse avec le statut 500 (Internal Server Error) et un message d'erreur
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("height-most-used-category-last-week")]
        public IActionResult GetMostUsedCategory()
        {
            try
            {
                DateTime lastWeek = DateTime.UtcNow.AddDays(-7); // Get the date of one week ago

                List<Category> topEightCategoriesLastWeek = _context.Picture
                    .Where(p => p.publishDate >= lastWeek) // Filter pictures based on date
                    .SelectMany(p => p.categoryPictures.Select(cp => cp.category)) // Flatten the CategoryPicture relationship
                    .GroupBy(category => category.idCategory) // Group by category id
                    .OrderByDescending(g => g.Count()) // Order by count in descending order
                    .Take(8) // Take the first eight categories
                    .Select(g => g.First()) // Select the category from each group
                    .ToList();



                return Ok(topEightCategoriesLastWeek);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
