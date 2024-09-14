using BookStoreAPI.Endpoints;
using BookStoreAPI.Middleware;
using BookStoreAPI.Repositories;
using BookStoreAPI.Repositories.Interfaces;
using Serilog.Sinks.File;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
            .AddSingleton<IBookRepository, BookMysqlDB>()
            .AddSwaggerGen()
            .AddEndpointsApiExplorer()
            .AddExceptionHandler<GlobalExceptionHandling>()
            .AddControllers();

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/logs-.txt", rollingInterval: RollingInterval.Hour)
        .CreateLogger();
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { }) // adds exception handling to endpoints.
   .UseHttpsRedirection();

app.UseAuthorization();

app.MapBookEndpoints(); // adds endpoints from bookendpoints.cs

app.Run();
