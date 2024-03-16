using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Data;
using WebApplication1.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Controllers // Assurez-vous de remplacer "WebApplication1" par le nom de votre espace de noms
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly apiContext _context;

        public PictureController(apiContext context)
        {
            _context = context;
        }
        // GET: api/Picture/{id}
       [HttpGet("/{id}")]
        public IActionResult GetImageById(int id)
        {
            try
            {
                // Find the image with the specified id
                var image = _context.Picture.FirstOrDefault(p => p.idPicture == id);
                
                if (image == null)
                {
                    return NotFound(); // Retourne une réponse 404 si aucune image n'est trouvée avec l'ID spécifié
                }
                
                return Ok(image); // Retourne les détails de l'image
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet("/picture/all")]
        public IActionResult GetPicturesWithCategories()
        {
            try
            {
                // Récupérer toutes les images avec les données associées de la base de données en utilisant une requête de jointure
                 var pictures = _context.Picture
                    .Include(p => p.categoryPictures)
                        .ThenInclude(cp => cp.category) // Inclure les données des catégories associées
                    .Include(p => p.likes) // Inclure les likes associés
                        .ThenInclude(l => l.user) // Inclure les données de l'utilisateur associé à chaque like
                    .Include(p => p.comments) // Inclure les commentaires associés
                        .ThenInclude(c => c.User) // Inclure les données de l'utilisateur associé à chaque commentaire
                    .ToList();



                // Définir les options de sérialisation JSON pour gérer les références circulaires
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                // Sérialiser les images en JSON en utilisant les options spécifiées
                var jsonResult = JsonSerializer.Serialize(pictures, options);

                // Retourner le résultat JSON
                return Ok(jsonResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}