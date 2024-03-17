using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

<<<<<<< HEAD
builder.Services.AddDbContext<apiContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
=======

// Add services to the container.
builder.Services.AddDbContext<apiContext>(options =>
{
    IConfiguration configuration = builder.Configuration;
    options.UseNpgsql(configuration.GetConnectionString("apiAppCon"));
});
>>>>>>> origin/Dev

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD
=======
app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

>>>>>>> origin/Dev
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
