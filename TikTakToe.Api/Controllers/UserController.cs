using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TikTakToe.DataAccess.EntityFramework;
using TikTakToe.Domain.Entities;

namespace TikTakToe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        public UserController(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            IEnumerable<User>? users = await context.Users.ToListAsync();

            return Ok(new { Data = users, Success = true });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            User? user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
                return NotFound(new { Data = "User is not found", Success = false });

            return Ok(new { Data = user, Success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return Ok(new { Data = user, Success = true });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            if (await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) is null)
                return NotFound(new { Data = "User is not found", Success = false });

            user.Id = id;
            context.Users.Update(user);
            await context.SaveChangesAsync();

            return Ok(new { Data = user, Success = true });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            User? user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (user is null)
                return NotFound(new { Data = "User is not found", Success = false });

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return Ok(new { Data = "User succesfully removed", Success = true });
        }
    }
}
