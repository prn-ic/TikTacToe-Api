using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TikTakToe.DataAccess.EntityFramework;
using TikTakToe.Domain.Entities;

namespace TikTakToe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private List<int[]> Combination = new List<int[]>()
        {
            new int[] { 0, 1, 2 },
            new int[] { 3, 4, 5 },
            new int[] { 6, 7, 8 },
            new int[] { 0, 3, 6 },
            new int[] { 1, 4, 7 },
            new int[] { 2, 5, 8 },
            new int[] { 0, 4, 8 },
            new int[] { 2, 4, 6 }
        };
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        public GameController(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            IEnumerable<Game>? games = await context.Games
                .Include(x => x.Winner)
                .ToListAsync();

            return Ok(new { Data = games, Success = true });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            Game? game = await context.Games
                .Include(x => x.Winner)
               .FirstOrDefaultAsync(x => x.Id == id);

            if (game is null)
                return NotFound(new { Data = "Game is not found", Success = true });

            return Ok(new { Data = game, Success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            await context.Games.AddAsync(game);
            await context.SaveChangesAsync();

            return Ok(new { Data = game, Success = true });
        }
        [HttpPost("step")]
        public async Task<IActionResult> Step(int userId, int gameId, int step)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            List<Step> gameSteps = await context.Steps
                .Where(x => x.GameId == gameId)
                .ToListAsync();

            Game? game = await context.Games
                .Include(x => x.Winner)
               .FirstOrDefaultAsync(x => x.Id == gameId);

            if (game is null || game.WinnerId is not null)
                return NotFound(new { Data = "Game is not found or end", Success = false });

            if (await context.Steps
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StepIndex == step
                && x.GameId == gameId) is not null)
                return Conflict(new { Data = "Step is exist", Success = false });

            if (gameSteps.Count >= 9 && game!.WinnerId is null)
                return Conflict(new { Data = "Draw", Success = false });

            await context.Steps
                .AddAsync(new Step() { GameId = gameId, UserId = userId, StepIndex = step });
            await context.SaveChangesAsync();

            List<Step>? steps = await context.Steps
                .Where(x => x.UserId == userId && x.GameId == gameId).ToListAsync();

            if (steps.Count >= 3)
            {
                string stringSteps =
                    String.Join("", steps.OrderBy(x => x.StepIndex).Select(x => x.StepIndex.ToString()).ToArray());
                foreach (int[] combination in Combination)
                {
                    combination.OrderBy(x => x);
                    string stringCombination = String.Join("", combination);
                    if (stringSteps.Contains(stringCombination))
                    {
                        User? user = await context.Users.FindAsync(userId);
                        
                        game!.WinnerId = userId;
                        context.Games.Update(game);

                        await context.SaveChangesAsync();

                        return Ok(new { Data = $"Winner: {user!.Name}", Success = true });
                    }
                }
            }

            return Ok(new { Success = true });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Game game)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            if (await context.Games.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) is null)
                return NotFound(new { Data = "Game is not found", Success = false });

            game.Id = id;
            context.Games.Update(game);
            await context.SaveChangesAsync();

            return Ok(new { Data = game, Success = true });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            Game? game = await context.Games.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (game is null)
                return NotFound(new { Data = "Game is not found", Success = false });

            context.Games.Remove(game);
            await context.SaveChangesAsync();

            return Ok(new { Data = "Game succesfully removed", Success = true });
        }
    }
}
