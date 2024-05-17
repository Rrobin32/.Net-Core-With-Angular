using WebAPI.ExtensionHelper;
using ConfigurationUtilities.Utilities;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("Json/AppSettings.json");
builder.Configuration.AddJsonFile("Json/ValidationMessages.json");
builder.Configuration.AddEnvironmentVariables();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();


var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//Retrive app setting, jwt token and custom error message list
builder.Services.Configure(builder.Configuration, builder.Environment);

//User defined dependency injection
builder.Services.AddDependencyInjection();

var app = builder.Build();

//Swagger setting
app.UseSwagger();
app.UseSwaggerUI();

//Jwt token Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.WithMethods(HttpMethods.Get, HttpMethods.Post);
});

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.Run();
