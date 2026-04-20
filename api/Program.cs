using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using QuestPDF.Infrastructure;
using Resend;
using SampleApp.Extensions;
using SampleApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var featureFlags = builder.Configuration.GetSection("FeatureFlags");
bool enableAuth0 = featureFlags.GetValue<bool>("EnableAuth0", true);
bool enableEmailing = featureFlags.GetValue<bool>("EnableEmailing", true);
bool enableMigrations = featureFlags.GetValue<bool>("EnableMigrations", true);
bool enableDatabase = featureFlags.GetValue<bool>("EnableDatabase", true);

var isSwaggerCli = AppDomain.CurrentDomain
    .GetAssemblies()
    .Any(a => a.GetName().Name == "Swashbuckle.AspNetCore.Cli");

if (enableAuth0)
{
    var auth0Domain = builder.Configuration["AUTH0DOMAIN"]?.Trim()
        ?? builder.Configuration.GetConnectionString("AUTH0DOMAIN")?.Trim();
    if (string.IsNullOrWhiteSpace(auth0Domain))
    {
        throw new InvalidOperationException("AUTH0DOMAIN must be configured in appsettings.json, appsettings.{Environment}.json, or via environment variable AUTH0DOMAIN.");
    }
    var domain = auth0Domain.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
        ? auth0Domain.TrimEnd('/') + "/"
        : $"https://{auth0Domain.TrimEnd('/')}/";

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = "auth0audience";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = "permissions"
        };
    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("read:data", policy => policy.Requirements.Add(new HasScopeRequirement("read:data", domain)));
        options.AddPolicy("write:data", policy => policy.Requirements.Add(new HasScopeRequirement("write:data", domain)));
    });
}

if (enableDatabase)
{
    // Register CockroachDb connection provider
    builder.Services.AddSingleton<ICockroachDbConnectionProvider, CockroachDbConnectionProvider>();
}

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddOptions();

if (enableAuth0)
{
    builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
}
if(enableEmailing){
    builder.Services.Configure<ResendClientOptions>(o =>
    {
        o.ApiToken = builder.Configuration.GetValue<string>("RESEND_API_KEY")?.Trim() ?? "";
    });
    builder.Services.AddHttpClient<ResendClient>();
    builder.Services.AddTransient<IResend, ResendClient>();
}


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new() { Title = "API", Version = "v1" });
    });
    builder.Services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
}
QuestPDF.Settings.License = LicenseType.Community;


var app = builder.Build();

if (enableMigrations)
{
    // Run migrations before starting the application
    await app.RunMigrationsAsync();
}

app.UseHttpsRedirection();

app.UseRouting();

if(app.Environment.IsProduction() && !isSwaggerCli){
    var directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
    var finalPath = Path.Combine(directory!, "wwwroot");
    var serverConfig = new FileServerOptions
        {
            FileProvider = new PhysicalFileProvider(finalPath),
            RequestPath = "",
            RedirectToAppendTrailingSlash = false
        };
    app.UseFileServer(serverConfig);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapReverseProxy();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (enableAuth0)
{
    app.UseAuthentication();
    app.UseAuthorization();
}

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

if(app.Environment.IsProduction()){
    app.MapFallbackToFile("/index.html");
}
app.Run();