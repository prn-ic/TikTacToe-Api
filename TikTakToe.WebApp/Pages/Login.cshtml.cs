using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TikTakToe.Domain.Entities;

namespace TikTakToe.WebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User? FirstUser { get; set; } = new User();
        [BindProperty]
        public User? SecondUser { get; set; } = new User();
        public void OnGet()
        {
        }
        public IActionResult OnPostGame()
        {
            Task.Run(async () =>
            {
                HttpClient httpClient = new HttpClient();
                JsonContent firstJson = JsonContent.Create(FirstUser);
                JsonContent secondJson = JsonContent.Create(SecondUser);
                HttpResponseMessage responseFirst = await
                    httpClient
                    .PostAsync("https://localhost:7241/api/User", firstJson);
                HttpResponseMessage responseSecond = await
                    httpClient
                    .PostAsync("https://localhost:7241/api/User", secondJson);

                ResponseModel<User>? firstUser = await responseFirst.Content
                .ReadFromJsonAsync<ResponseModel<User>>();

                ResponseModel<User>? secondUser = await responseSecond.Content
                .ReadFromJsonAsync<ResponseModel<User>>();

                Response.Cookies.Append("FirstUserId", firstUser!.Data!.Id.ToString());
                Response.Cookies.Append("SecondUserId", secondUser!.Data!.Id.ToString());
                Response.Cookies.Append("FirstUser", firstUser!.Data!.Name!);
                Response.Cookies.Append("SecondUser", secondUser!.Data.Name);

            }).Wait();
            

            return Redirect("/Game");
        }
    }
}
