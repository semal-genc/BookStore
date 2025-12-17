using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApi.DBOperations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<BookStoreDbContext>(Options=>Options.UseInMemoryDatabase(databaseName:"BookStoreDB"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}

app.Run();