using Microsoft.EntityFrameworkCore;
using POC.DataAccess;
using POC.DataAccess.Redis;
using POC.RedisExample;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<ExampleDbContext>(opt =>
    {
        opt.UseSqlServer("Server=localhost;Database=exampleDB;Integrated Security=True;");
    }, ServiceLifetime.Transient);

builder.Services.AddHostedService<ExampleHostedService>();

var multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

builder.Services.AddTransient<IRedisService, RedisService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ExampleDbContext>();
    //db.Database.EnsureDeleted();
    db.Database.Migrate();
}

app.Run();

