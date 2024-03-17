<<<<<<< HEAD
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
       [HttpGet("picture/{id}")]
        public IActionResult GetImageById(int id)
        {
            try
            {
                // Find the image with the specified id
                var image = _context.Picture
                    .Include(p => p.categoryPictures)
                        .ThenInclude(cp => cp.category) // Inclure les données des catégories associées
                    .Include(p => p.likes) // Inclure les likes associés
                        .ThenInclude(l => l.user) // Inclure les données de l'utilisateur associé à chaque like
                    .Include(p => p.comments) // Inclure les commentaires associés
                        .ThenInclude(c => c.User) // Inclure les données de l'utilisateur associé à chaque commentaire
                    .FirstOrDefault(p => p.idPicture == id);
                
                if (image == null)
                {
                    return NotFound(); // Retourne une réponse 404 si aucune image n'est trouvée avec l'ID spécifié
                }
                
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                // Sérialiser les images en JSON en utilisant les options spécifiées
                var jsonResult = JsonSerializer.Serialize(image, options);

                // Retourner le résultat JSON
                return Ok(jsonResult);            }
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
=======
﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PictureController : ControllerBase
	{
		private const string OpenAI_API_KEY = "AIzaSyD8oyXDTqxHjgrf0UnWGWOkKC4eqgFYWbM"; // Replace with your actual OpenAI API key
		private const string OpenAI_API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro-vision:generateContent?key=" + OpenAI_API_KEY;

		private readonly apiContext _context;
		public PictureController(apiContext context)
		{
			_context = context;
		}

		[HttpPost("upload")]
		public IActionResult uploadPicture([FromBody] PictureDTO picture)
		{
            try
            {
                var base64String = picture.image;
                var imageBytes = Convert.FromBase64String(base64String);
                var imagePath = Path.Combine("Images", Guid.NewGuid().ToString() + ".jpg");
                System.IO.File.WriteAllBytes(imagePath, imageBytes);

                // Here you can also save the title and description to a database or file
                // For simplicity, we're just logging them

				Picture pic = picture.toPicture();
				pic.picturePath = imagePath;
                _context.Picture.Add(pic);
                _context.SaveChanges();

				int insertedId = pic.idPicture;
				picture.categories.ForEach(c => 
				_context.CategoryPicture.Add(new CategoryPicture { idCategory = c, idPicture = insertedId }));
				_context.SaveChanges();
                return Ok(picture);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error uploading image: " + ex.Message });
            }

			
		}

		[HttpGet("picture")]

		public IActionResult getPicture([FromQuery] int id)
		{
			try
			{
				Picture picture = _context.Picture.Find(id);
				if (picture == null)
				{
					return NotFound();
				}
				return Ok(picture);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}

		[HttpGet("pictures")]
		public IActionResult getAllPicturesWithRandomOrdered()
		{
            try
			{
                List<Picture> pictures = _context.Picture.ToList();
                return Ok(pictures);
            }
            catch (Exception e)
			{
                return BadRequest(e);
            }
        }

		[HttpGet("pictures-most-viewed-of-weeked")]
		public IActionResult getMostViewedPicturesOfWeek()
		{
            try
			{
                DateTime today = DateTime.Now;
                DateTime lastWeek = today.AddDays(-7).ToUniversalTime();
                List<Picture> pictures = _context.Picture.Where(p => p.publishDate >= lastWeek).OrderByDescending(p => p.views).ToList();
				Console.WriteLine(pictures.Count);
                return Ok(pictures);
            }
            catch (Exception e)
			{
                return BadRequest(e);
            }
        }

		[HttpGet("top-six-most-viewed-and-liked-pictures")]
		public IActionResult getTopSixMostViewedAndLikedPictures()
		{
            try
			{
                List<Picture> pictures = _context.Picture.OrderByDescending(p => p.views).ThenByDescending(p => p.likes.Count()).Take(6).ToList();
                return Ok(pictures);
            }
            catch (Exception e)
			{
                return BadRequest(e);
            }
        }

		[HttpGet("{id}/total-view-of-artwork-of-users")]
		public IActionResult getTotalViewOfArtworkOfUser([FromRoute] int id)
		{
            try
			{
                int totalViews = _context.Picture.Where(p => p.idUser == id).Sum(p => p.views);
                return Ok(totalViews);
            }
            catch (Exception e)
			{
                return BadRequest(e);
            }
        }



		[HttpPost("description-auto")]
		public async Task<string> generateDescription([FromBody] PictureDTO pictureDTO)
		{


			
            // Convert the byte array to a Base64 string
            var base64Image = pictureDTO.image;

                using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					var requestBody = new
					{
						contents = new[]
						{
					new
					{
						parts = new object[]
						{
							new { text = "Genereate a short description text of this work of art" },
							new
							{
								inline_data = new
								{
									mime_type = "image/jpeg",
									data = base64Image
								}
							}
						}
					}
				}
					};

					var jsonContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

					var response = await client.PostAsync(OpenAI_API_URL, jsonContent);
					if (response.IsSuccessStatusCode)
					{
						var responseContent = await response.Content.ReadAsStringAsync();
						return responseContent;
					}
					else
					{
						throw new Exception($"Error: {response.StatusCode}");
					}
				}
			


		}
	}
}
>>>>>>> origin/Dev
