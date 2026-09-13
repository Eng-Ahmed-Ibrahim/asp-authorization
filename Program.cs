using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Configurations;
using WebApi.Data;
using WebApi.Models;
using WebApi.Services;
using WebApi.Constants;
using WebApi.Authorization;
using WebApi.Data.Seeder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthorizationHandler,PermissionAuthorizationHandler>();
builder.Services.AddSingleton<
    IAuthorizationPolicyProvider,
    PermissionAuthorizationPolicyProvider>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddIdentity<ApplicationUser, Role>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ISeeder, RolesSeeder>();
builder.Services.AddScoped<ISeeder, UserSeeder>();
builder.Services.AddScoped<ISeeder, ProductsSeeder>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JWT");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                jwtSettings["SecretKey"]!
            )
        )
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            Console.WriteLine("========== TOKEN RECEIVED ==========");
            Console.WriteLine(context.Token);
            return Task.CompletedTask;
        },

        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("========== JWT FAILED ==========");
            Console.WriteLine(context.Exception.ToString());
            return Task.CompletedTask;
        },

        OnTokenValidated = context =>
        {
            Console.WriteLine("========== JWT VALIDATED ==========");

            foreach (var claim in context.Principal!.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }

            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization(Options =>
{
    Options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin")
    );
});

var app = builder.Build();

// =========================
// Database Seeder
// =========================

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedAsync(scope.ServiceProvider);
}

// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider
//         .GetRequiredService<AppDbContext>();

//     await DatabaseSeeder.SeedAsync(context);
// }

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();
app.Run();