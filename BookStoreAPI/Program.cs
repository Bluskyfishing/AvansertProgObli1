using BookStoreAPI.Endpoints;
using BookStoreAPI.Repositories;
using BookStoreAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services
        .AddSingleton<IBookRepository, BookMysqlDB>()
        .AddSwaggerGen()
        .AddEndpointsApiExplorer()
        .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapBookEndpoints(); // adds endpoints from bookendpoints.cs

app.Run();
