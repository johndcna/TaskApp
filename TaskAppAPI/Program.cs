using TaskAppAPI.Repository.Interfaces;
using TaskAppAPI.Repository;
using TaskAppAPI.Data;
using Microsoft.EntityFrameworkCore;
using TaskAppAPI.Services.Interfaces;
using TaskAppAPI.Services.Implementations;
using AutoMapper;
using TaskAppAPI.Data.Entities;
using TaskAppAPI;
using TaskAppAPI.Custom;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseInMemoryDatabase("ApplicationDbContext"));

builder.Services.AddScoped<IRepository<TaskItem>, Repository<TaskItem>>();
builder.Services.AddScoped<ITaskService, TaskService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policyBuilder =>
        {
            policyBuilder
                .WithOrigins("https://localhost:7212")  // Your Blazor app URL
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowBlazorClient");
app.MapControllers();

app.Run();
