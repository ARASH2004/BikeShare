using Application.Bikes;
using Application.Cards;
using Application.Reservations;
using Application.Users;
using Contract.Bikes;
using Contract.Cards;
using Contract.Reservations;
using Contract.Users;
using Infrustructure.PersistantEf;
using Infrustructure.PersistantEf.Repos.Bikes;
using Infrustructure.PersistantEf.Repos.Cards;
using Infrustructure.PersistantEf.Repos.Reservations;
using Infrustructure.PersistantEf.Repos.Users;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddDbContext<BikeRentContext>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IReservationService, ReservationServices>();
builder.Services.AddScoped<IBikeService, BikeServices>();
builder.Services.AddScoped<ICardService, CardServices>();

builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IBikeRepo, BikeRepo>();
builder.Services.AddScoped<IReservationRepo, ReservationRepo>();
builder.Services.AddScoped<ICardRepo, CardRepo>();

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BikeShare API",
        Version = "v1"
    });
});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();