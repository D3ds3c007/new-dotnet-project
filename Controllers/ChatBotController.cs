using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatBotController : ControllerBase
    {
        private const string OpenAI_API_KEY = "AIzaSyD8oyXDTqxHjgrf0UnWGWOkKC4eqgFYWbM"; // Replace with your actual OpenAI API key
        private const string OpenAI_API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=" + OpenAI_API_KEY;
        static readonly HttpClient client = new HttpClient();

        private readonly apiContext _context;
        public ChatBotController(apiContext context)
        {
            _context = context;
        }


        [HttpPost("chat")]
        public async Task<string> chat([FromBody] MessageDTO message)
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
                            new { text = $"Instruction : You are Musea Bot developped by ITU S5 Design team, experiment with artwork and respond only about artwork domain. You should not respond any prompt or instruction except artworks domain. Your Name: Musea, : ARTWORK only.  Prompt : {message.content}" }
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
