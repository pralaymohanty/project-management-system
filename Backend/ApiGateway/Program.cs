using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JwtAuthenticationmanager;

var builder = WebApplication.CreateBuilder(args);

//Ocelot Configuration

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddDependecyJwtAuthentication();

// Configuration ends

var app = builder.Build();

//Binding app to Ocelot
await app.UseOcelot();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
