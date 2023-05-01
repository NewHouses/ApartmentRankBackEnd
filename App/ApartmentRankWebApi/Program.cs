using ApartmentRank.App.Interfaces;
using ApartmentRank.App.Interfaces.Infrastructure;
using ApartmentRank.App.Services;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.Services;
using ApartmentRank.Infrastructure.Api;
using ApartmentRank.Domain.Services.Factories;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using ApartmentRank.App.Interfaces.Infrastructure.Repositories;
using ApartmentRank.Infrastructure.Persistence.DataAccess.Repositories;
using ApartmentRank.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<DbApartmentRankContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRankingService, RankingService>();
builder.Services.AddTransient<ISearchApartmentsService, SearchApartmentsService>();
builder.Services.AddTransient<IAdapterFactory, IdealistaAdapterFactory>();
builder.Services.AddTransient<IConnector, Connector>();
builder.Services.AddScoped<IIdealistaApi, IdealistaApi>();
builder.Services.AddScoped<IOpenAIApi, OpenAIApi>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "ApartmentRankFrontendPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseResponseCompression();

app.UseAuthorization();

app.MapControllers();

app.Run();
