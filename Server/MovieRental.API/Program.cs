using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieRental.Application.Interfaces;
using MovieRental.Application.Services;
using MovieRental.Domain.Entities;
using MovieRental.Domain.Interfaces;
using MovieRental.Infrastructure.Persistence;
using MovieRental.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//SQL Server configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//

//Add repositories to the Dependency Injection Container
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

//Add services to the Dependency Injection Container
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Preparing database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var prepDatabase = new PrepDatabase(dbContext);
    prepDatabase.Seed();
}
//

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
