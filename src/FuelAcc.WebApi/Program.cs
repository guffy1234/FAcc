using FuelAcc.Application.UseCases;
using FuelAcc.Domain.Identity;
using FuelAcc.Persistence;
using FuelAcc.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using FuelAcc.WebApi;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using FuelAcc.Persistence.Repositories;
using FuelAcc.WebApi.Api;
using FuelAcc.WebApi.Filters;
using FuelAcc.Application.Interface;
using FuelAcc.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});
builder.Services.AddProblemDetails();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = ApiDef.DefaultVersion;
    options.AssumeDefaultVersionWhenUnspecified = false;
    options.ReportApiVersions = false;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = ApiDef.GroupNameFormat;
    options.SubstituteApiVersionInUrl = true;
    options.DefaultApiVersion = ApiDef.DefaultVersion;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v" + ApiDef.v1,
        Title = "Fuel Accounting API",
        Description = "Fuel Accounting API",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Login-Password based Bearer JWT token authorization",
        Name = "oauth2.0",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("/api/v1/login/form", UriKind.RelativeOrAbsolute),
                TokenUrl = new Uri("/api/v1/login/form", UriKind.RelativeOrAbsolute),
                Scopes = new Dictionary<string, string>
                    {
                        {  "default","Access API as User" }
                    }
            }
        },
        Scheme = "oauth2"
    });

    //options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    //  {
    //    {
    //      new OpenApiSecurityScheme
    //      {
    //        Reference = new OpenApiReference
    //          {
    //            Type = ReferenceType.SecurityScheme,
    //            Id = "oauth2"
    //          },
    //          Scheme = "oauth2",
    //          Name = "Bearer",
    //          In = ParameterLocation.Header,
    //        },
    //        new List<string>()
    //      }
    //    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(host => true);
    });
});

//Add methods Extensions
builder.Services.AddInjectionPersistence(builder.Configuration);
builder.Services.AddInjectionApplication();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });
builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthorizationChecker, AuthorizationChecker>();

builder.Services.AddIdentityCore<ApplicationUser>(o =>
{
    o.Stores.MaxLengthForKeys = 128;
}).AddRoles<ApplicationRole>().AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

{
    using var serviceScope = app.Services.CreateScope();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();

    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    await IdentitySeeder.SeedAsync(userManager, roleManager);
    await SystemSeeder.SeedAsync(dbContext);
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Fuel Accounting V.1");
    c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    //c.RoutePrefix = string.Empty;
});
//}

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseCors();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();