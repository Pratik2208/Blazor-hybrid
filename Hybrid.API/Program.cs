using Hybrid.Business.Services;
using Hybrid.Data;
using Hybrid.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CosmosDbContext>(optionsBuilder =>
    optionsBuilder
        .UseCosmos(
            connectionString: builder.Configuration.GetConnectionString("DefaultConnection")!,
            databaseName: "NoteApp"));

builder.Services.AddScoped<NoteRepository>();
builder.Services.AddScoped<INotesService, NotesService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CosmosDbContext>();
    await context.Database.EnsureCreatedAsync();
}

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
