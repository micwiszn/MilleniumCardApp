using Microsoft.Extensions.Caching.Memory;
using MilleniumCardApp.API.Interfaces;
using MilleniumCardApp.API.Providers;
using MilleniumCardApp.API.Services;
using MilleniumCardApp.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// enforce enums to be returned as strings
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<ICardActionsRepository, CardActionsRepository>();

builder.Services.AddSingleton<CardService>();
builder.Services.AddSingleton<CardController>();

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