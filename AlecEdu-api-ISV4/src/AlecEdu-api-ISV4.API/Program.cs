using AlecEdu_api.Application;
using AlecEdu_api.Domain.Extensions;
using AlecEdu_api.Infrastructure;
using AlecEdu_api.Infrastructure.Persistence;
using AlecEdu_api.Infrastructure.Persistence.Seeds;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var CorsPolicy = "_corsPolicy";
builder.Logging.AddFile(builder.Configuration.GetSection("Logging"));

// Add services to the container.
builder.Host.AddAppConfigurations();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy,
        builder =>
        {
            builder
                .WithOrigins(
                    "http://localhost:3000",
                    "https://localhost:3000"
                )
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(CorsPolicy);
app.AddApplicationBuilders();
app.UseIdentityServer();
app.UseHttpsRedirection();
app.UseAuthentication();  
app.UseAuthorization();

app.UseHangfireDashboardDomain(builder.Configuration);
app.AddInfrastructureApplication(builder);
app.MapControllers();
app.MigrateDatabase<AlecEduContext>((context, _) =>
{
    ConfigurationDbContextIdentityServer4Seed.IdentityServer4SeedAsync(context, builder.Configuration).Wait();
});
app.Run();
