using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Configurations.JsonSerializer;
using MultiTenantManagement.Abstractions.Configurations.Options;
using MultiTenantManagement.Abstractions.Extensions;
using MultiTenantManagement.Abstractions.Requirements.Handlers;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities;
using MultiTenantManagement.Dependencies.Services;
using MultiTenantManagement.Sql.DatabaseContext;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Configure my appsettings.local.json
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

//Add Services
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthorizationHandler, UserActiveHandler>();
builder.Services.AddScoped<IApplicationDbContext>(services => services.GetRequiredService<ApplicationDbContext>());
builder.Services.AddScoped<IAuthenticationDbContext>(services => services.GetRequiredService<AuthenticationDbContext>());

//Add Options
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(nameof(EmailOptions)));
var jwtSection = builder.Configuration.GetSection(nameof(JwtOptions));
builder.Services.Configure<JwtOptions>(jwtSection);
var jwtOptions = jwtSection.Get<JwtOptions>();

//Add DbContext
builder.Services.AddDbContext<AuthenticationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStrings.SqlConnection)!);
});

//Add .Net Core Identity
builder.Services.IdentityBuild<AuthenticationDbContext>(builder.Environment.IsProduction());

//Add MultiTenant DbContext
builder.Services.AddDbContext<ApplicationDbContext>((services, options) =>
{
    var tenantService = services.GetRequiredService<ITenantService>();
    var tenant = tenantService.GetTenantFromAuthenticatedUser();

    options.UseSqlServer(tenant.ConnectionString);
});

//Add Authentication
builder.Services.AuthenticationBuild(builder.Environment.IsProduction(), jwtOptions);

//Add Authorization
builder.Services.AuthorizationBuild();

//Add global Filters
builder.Services.AddControllers(options =>
{
    //Set global authorize policy as Filter
    options.Filters.Add(new AuthorizeFilter("UserActive"));
})
.AddJsonOptions(options => 
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
});

//Add Cache
builder.Services.AddMemoryCache();

//Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

//Add Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});

//Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Add Swagger Configuration
builder.Services.SwaggerBuild();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Serilog log all requests
app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;
});

app.UseHttpsRedirection();

//Handle Errors
app.UseErrorHandlingMiddleware(); //Or install and use Hellang.Middleware.ProblemDetails

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
