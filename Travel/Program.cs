using App.Services;
using Microsoft.Extensions.Configuration;
using Travel.Connection;
using Travel.Models;
using Travel.Repositories;
using Travel.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient(a => ConnectionDB.Conecction());

builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<IHotelRepository, HotelRepository>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IReserveService, ReserveService>();
builder.Services.AddTransient<IReserveRepository, ReserveRepository>();
builder.Services.AddTransient<IClientService, ClientService>();
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
