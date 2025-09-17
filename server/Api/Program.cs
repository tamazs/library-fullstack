using DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetValue<string>("Db"))
);

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi();
app.MapControllers();

app.Run();
