using ControleDeChamado.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionDB = 
    builder.Configuration.GetConnectionString("ConnectionDB");
builder.Services.AddDbContext<ExemploContext>(
    context => context.UseMySQL(connectionDB));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyPolicy",
    policy =>
    {
        policy.WithOrigins("https://192.168.0.139:7038/", "https://localhost:7038/")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });

}) ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
