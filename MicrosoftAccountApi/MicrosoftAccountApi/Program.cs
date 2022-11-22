using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using MicrosoftAccountApi.Configurations;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
IdentityModelEventSource.ShowPII = true;

// Add services to the container.

builder.Services.AddControllers();
var apiConfiguration = builder.Configuration.GetSection(nameof(ApiConfiguration))
    .Get<ApiConfiguration>();
builder.Services.AddSingleton(apiConfiguration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Host.ConfigureAppConfiguration((context, config) =>
// {
//     var env = context.HostingEnvironment;
//     config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//         .AddJsonFile($"ocelot.json", optional: false, reloadOnChange: true)
//         .AddEnvironmentVariables();
// });
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();


builder.Services.AddOcelot(configuration);
// var issuerUri = apiConfiguration.IssuerUri;
// var apiName = apiConfiguration.ApiName;
//
// // builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
// //     .AddIdentityServerAuthentication(opt =>
// //     {
// //         opt.Authority = issuerUri;
// //         opt.ApiName = apiName;
// //         opt.RequireHttpsMetadata = false;
// //         opt.SupportedTokens = SupportedTokens.Both;
// //     });

builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( "Bearer", x =>
{
    // x.SaveToken = true;
    // x.RequireHttpsMetadata = false;
    x.Authority = "https://dkxt-api.humg.edu.vn";
    // x.RequireHttpsMetadata = false;
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});

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
await app.UseOcelot();

app.Run();
