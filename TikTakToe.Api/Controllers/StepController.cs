using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TikTakToe.DataAccess.EntityFramework;
using TikTakToe.Domain.Entities;

namespace TikTakToe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        public StepController(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            IEnumerable<Step>? steps = await context.Steps
                .Include(x => x.Game)
                .Include(x => x.User)
                .ToListAsync();

            return Ok(new { Data = steps, Success = true });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            Step? step = await context.Steps
                .Include(x => x.Game)
                .Include(x => x.User)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (step is null)
                return NotFound(new { Data = "Step is not found", Success = false });

            return Ok(new { Data = step, Success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Step step)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            if (await context.Steps
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StepIndex == step.StepIndex
                && x.UserId == step.UserId) is not null)
                return Conflict(new { Data = "Step is exist", Success = false });

            await context.Steps.AddAsync(step);
            await context.SaveChangesAsync();

            return Ok(new { Data = step, Success = true });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Step step)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            if (await context.Steps.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id) is null)
                return NotFound(new { Data = "Step is not found", Success = false });

            step.Id = id;
            context.Steps.Update(step);
            await context.SaveChangesAsync();

            return Ok(new { Data = step, Success = true });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await using AppDbContext context = await _contextFactory.CreateDbContextAsync();

            Step? step = await context.Steps.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (step is null)
                return NotFound(new { Data = "Step is not found", Success = false });

            context.Steps.Remove(step);
            await context.SaveChangesAsync();

            return Ok(new { Data = "Step succesfully removed", Success = true });
        }
    }
}
