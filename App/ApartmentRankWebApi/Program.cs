using ApartmentRank.App.Interfaces;
using ApartmentRank.App.Interfaces.Infrastructure;
using ApartmentRank.App.Services;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.Services;
using ApartmentRank.Infrastructure.Api;
using ApartmentRank.Domain.Services.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRankingService, RankingService>();
builder.Services.AddTransient<ISearchApartmentsService, SearchApartmentsService>();
builder.Services.AddTransient<IAdapterFactory, IdealistaAdapterFactory>();
builder.Services.AddTransient<IConnector, Connector>();
builder.Services.AddScoped<IIdealistaApi, IdealistaApi>();


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