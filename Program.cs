using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventSource;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Services;
using TodoApi.Infrastructure.Data;
using TodoApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOutputCache();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlite("Data Source=todos.db"));
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddValidatorsFromAssemblyContaining<TodoApi.Application.Validators.CreateTodoValidator>();
// ---  SWAGGER SERVICES ---
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
// --------------------------------
var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); // If the database doesn't exist, it creates it; if it does, it leaves it untouched.
}
app.UseOutputCache();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi().CacheOutput(p => p.Expire(TimeSpan.FromMinutes(10)));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();