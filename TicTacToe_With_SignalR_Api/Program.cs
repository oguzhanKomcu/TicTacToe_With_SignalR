using TicTacToe_With_SignalR.Data;
using Microsoft.EntityFrameworkCore;
using TicTacToe_With_SignalR.Hubs;
using TicTacToe_With_SignalR_Api.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TicTacToeDb"));

 string ApiCorsPolicy = "_apiCorsPolicy";
builder.Services.AddCors(options => options.AddPolicy(ApiCorsPolicy, builder => {
    builder
        .WithOrigins("https://localhost:7243")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors(ApiCorsPolicy);

app.MapHub<UserHub>("/hubs/userHub");
app.MapHub<RoomHub>("/hubs/roomHub");
app.MapHub<UserRoomHub>("/hubs/userRoomHub");

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
