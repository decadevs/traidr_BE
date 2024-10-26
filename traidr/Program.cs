
using traidr.Domain.ExceptionHandling.Configuraion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using traidr.Application.IServices;
using traidr.Application.Services;
using traidr.Domain.Context;
using traidr.Domain.Models;
using traidr.Infrastructure.Cloudinary;
using traidr.Infrastructure.EmailServices;
using traidr.Domain.Context.PreSeeding;

namespace traidr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Swagger Authorization Configuration
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Traidr API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
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
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            }
                        },
                        new string[] { }                   
                    }
                });
            });

            // Postgres Server Configuration
            // Add the email sender service to the dependency injection container
            builder.Services.AddScoped<IEmailSendingService, EmailSendingService>();

            builder.Services.AddScoped<IPhotoService, PhotoService>();

            // Configure SMTP settings from appsettings.json
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));


            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            // Postgres Server Configuration
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Identity Framework Configuration
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            // Jwt Configuration
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                 options.DefaultChallengeScheme =
                  options.DefaultForbidScheme =
                   options.DefaultScheme =
                    options.DefaultSignInScheme =
                     options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
                        )
                };
            });

            // Identity Framework Configuration
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            // Jwt Configuration
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                 options.DefaultChallengeScheme =
                  options.DefaultForbidScheme =
                   options.DefaultScheme =
                    options.DefaultSignInScheme =
                     options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
                        )
                };
            });

          
            var app = builder.Build();

            try
            {
                if (app.Environment.IsDevelopment())
                {
                    using var scope = app.Services.CreateScope();
                    var serviceProvider = scope.ServiceProvider;
                    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    Seeding.SeedData(context, userManager, roleManager).Wait();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            app.UseAuthentication();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //globalerrorhandler
            app.AddGlobalErrorHandler();

            app.MapControllers();

            app.Run();
        }

    }

}
