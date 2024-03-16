﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PictureController : ControllerBase
	{
		private const string OpenAI_API_KEY = "AIzaSyD8oyXDTqxHjgrf0UnWGWOkKC4eqgFYWbM"; // Replace with your actual OpenAI API key
		private const string OpenAI_API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro-vision:generateContent?key=" + OpenAI_API_KEY;

		private readonly apiContext _context;
		public PictureController(apiContext context)
		{
			_context = context;
		}

		[HttpGet("categories")]
		public IActionResult getCategories()
		{
			try
			{
				List<Category> categories = _context.Category.ToList();
				categories.ForEach(c => c.categoryPictures = null);
				return Ok(categories);
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
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
                return Ok(picture);
                Console.WriteLine($"Title: {picture.title}, Description: {picture.description}");

                return Ok(new { message = "Image uploaded successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error uploading image: " + ex.Message });
            }

			
		}

		[HttpPost("picture")]

		public IActionResult getPicture(int id)
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

		[HttpGet("description-auto")]
		public async Task<string> generateDescription()
		{
			string imagePath = "C:/Untitled.png";

			// Initialize a MemoryStream to hold the image data
			using (MemoryStream memoryStream = new MemoryStream())
			{
				// Open the image file
				using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
				{
					// Copy the file stream to the memory stream
					fileStream.CopyTo(memoryStream);
				}

				// Convert the memory stream to a byte array
				byte[] imageBytes = memoryStream.ToArray();

				// Convert the byte array to a Base64 string
				string base64Image = Convert.ToBase64String(imageBytes);
				using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					Console.WriteLine(base64Image);
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
}
