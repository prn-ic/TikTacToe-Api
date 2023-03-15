using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Net.Http.Json;
using TikTakToe.Domain.Entities;

namespace TikTakToe.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostLogin()
        {
            HttpClient httpClient = new HttpClient();
            JsonContent content = JsonContent.Create(new Game());
            HttpResponseMessage response = await 
                httpClient
                .PostAsync("https://localhost:7241/api/Game", content);

            ResponseModel<Game>? game = await response.Content
                .ReadFromJsonAsync<ResponseModel<Game>>();

            Response.Cookies.Append("GameId", game.Data.Id.ToString());

            return Redirect("/Login");
        }
    }
}