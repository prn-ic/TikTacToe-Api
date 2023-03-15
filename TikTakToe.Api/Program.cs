using Microsoft.EntityFrameworkCore;
using TikTakToe.DataAccess.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseSqlServer("Server=DESKTOP-7SBJM8F\\SQLEXPRESS; Database=TikTakToe; Trusted_Connection=true; Trust Server Certificate = true;",
        b => b.MigrationsAssembly("TikTakToe.Api"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
