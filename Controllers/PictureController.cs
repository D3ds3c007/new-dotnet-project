using Microsoft.AspNetCore.Mvc;
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
			string[] mediaExtension = new string[] { "jpg", "mp4", "png"};
            try
            {
                var base64String = picture.image;
                var imageBytes = Convert.FromBase64String(base64String);
                var imagePath = Path.Combine("Images", Guid.NewGuid().ToString() + $".{mediaExtension[picture.mediaType]}");
                System.IO.File.WriteAllBytes(imagePath, imageBytes);

                // Here you can also save the title and description to a database or file
                // For simplicity, we're just logging them

				Picture pic = picture.toPicture();
				pic.mediaType = picture.mediaType;
				pic.publishDate = DateTime.Now.ToUniversalTime();
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

		[HttpGet("two-random-pictures")]
		public IActionResult getTwoRandomPictures()
		{
            try
			{
				var images = _context.Picture.Where(p => p.mediaType == 0).ToList();
                List<Picture> pictures = images.OrderBy(p => Guid.NewGuid()).Take(2).ToList();
                return Ok(pictures);
            }
            catch (Exception e)
			{
                return BadRequest(e);
            }
        }

		[HttpGet("random-video")]
		public IActionResult getRandomVideo()
		{
            try
			{
				var videos = _context.Picture.Where(p => p.mediaType == 1).ToList();
                Picture picture = videos.OrderBy(p => Guid.NewGuid()).FirstOrDefault();
				Console.WriteLine("Hello" + picture);
                return Ok(picture);
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
                List<Picture> pictures = _context.Picture.Where(p => p.mediaType == 0).OrderByDescending(p => p.views).ThenByDescending(p => p.likes.Count()).Take(6).ToList();
                return Ok(pictures);
            }
            catch (Exception e)
			{
                return BadRequest(e);
            }
        }

		[HttpGet("top-six-most-viewed-and-liked-videos-of-week")]
		public IActionResult getTopSixMostViewedAndLikedVideosOfWeek()
		{
            try
			{
                DateTime today = DateTime.Now;
                DateTime lastWeek = today.AddDays(-7).ToUniversalTime();
                List<Picture> pictures = _context.Picture.Where(p => p.publishDate >= lastWeek && p.mediaType == 1).OrderByDescending(p => p.views).ThenByDescending(p => p.likes.Count()).Take(6).ToList();
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
			if (pictureDTO.mediaType == 0)
			{
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

			return "Media type not supported";
			


		}
	}
}
