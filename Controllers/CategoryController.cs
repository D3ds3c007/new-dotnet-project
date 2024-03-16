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
    }
}
