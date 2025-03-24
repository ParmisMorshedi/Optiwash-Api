using Microsoft.EntityFrameworkCore;
using OptiWash.Services.IServices;
using OptiWash.Services;
using System;
using OptiWash.Repositories.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OptiWash
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseKestrel();






            builder.Services.AddDbContext<OptiWashDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure Identity
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<OptiWashDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IWashRecordService, WashRecordService>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IWashRecordRepository, WashRecordRepository>();
          
            // Lägg till autentisering/autorisation efteråt
            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();

            // Configure Authentication (JWT)
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // Configure Swagger with JWT Authentication
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Enter 'Bearer <token>' to authenticate"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] { }
        }
    });
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowMobile",
                policy =>
                {
                    policy.WithOrigins("http://localhost:8081", "http://192.168.1.100:8081") 
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                 SeedAdminUser(services);
            }

            app.Run();

            async Task SeedAdminUser(IServiceProvider services)
            {
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                // Kontrollera om admin-rollen finns, annars skapa den
                string adminRole = "Admin";
                if (!await roleManager.RoleExistsAsync(adminRole))
                {
                    await roleManager.CreateAsync(new IdentityRole(adminRole));
                }

                // Skapa admin-användaren om den inte finns
                string adminEmail = "admin@example.com";
                string adminPassword = "Admin@123"; // OBS! Använd en starkare lösning i produktion

                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new User
                    {
                        UserName = "admin",
                        Email = adminEmail,
                        FullName = "Admin User",
                        IsAdmin = true,
                        EmailConfirmed = true
                    };
                    Console.WriteLine($"Creating admin user: {adminEmail} with password: {adminPassword}");

                    var result = await userManager.CreateAsync(adminUser, adminPassword);
                    if (result.Succeeded)
                    {
                        // Lägg till användaren i admin-rollen
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                        Console.WriteLine("Admin user created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Error creating admin user:");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine(error.Description);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Admin user already exists.");
                }
            }
        }
    }
}
