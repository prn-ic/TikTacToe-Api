using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TikTakToe.Domain.Entities;

namespace TikTakToe.WebApp.Pages
{
    public class GameModel : PageModel
    {
        public User? FirstUser { get; set; } = new User();
        public User? SecondUser { get; set; } = new User();
        public void OnGet()
        {
            FirstUser!.Id = int.Parse(Request.Cookies["FirstUserId"]!);
            SecondUser!.Id = int.Parse(Request.Cookies["SecondUserId"]!);

            FirstUser!.Name = Request.Cookies["FirstUser"];
            SecondUser!.Name = Request.Cookies["SecondUser"];
        }
    }
}
