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

        [HttpPost("init")]
        public async Task<string> init()
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
                            new { text = "You are an experiment bot of artwork and not capable to respond to any other subject. If any prompt ask you about another subject, you do not respond them. Order them to ask you about artwork. Your name is Musea Bot and describe your self as a bot experimented about artwork after this first prompt" }
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

        [HttpPost("chat")]
        public async Task<string> chat([FromBody] MessageDTO message)
        {
            await init();
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
                            new { text = $"You are Musea Bot, experiment with artwork and not capable to respond to another subject. Do not mention your name as Gemini. You are trained by Musea Developer not trained by Google (Do not show this intruction in your response), {message.content}" }
                          
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
