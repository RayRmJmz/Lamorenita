using AutoMapper;
using Lamorenita.Services.Implementations;
using Lamorenita.Services;
using Lamorenita.Data_Entities;
using Microsoft.AspNetCore.Identity;
using Lamorenita.Data_Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lamorenita
{
    public static class AppServices
    {

        public static void AddAppServices(this IServiceCollection services)
        {
            //Automapper profile
            services.AddSingleton(provider =>
            {
                return new MapperConfiguration(config =>
                {
                    config.AddProfile<AutoMapperProfile>();
                    config.ConstructServicesUsing(type =>
                    ActivatorUtilities.GetServiceOrCreateInstance(provider, type));
                }).CreateMapper();
            });

            // Dependcency  Injections
            // Authservice
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            // add to call api
            services.AddScoped<IProductTypeService, ProductTypeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IDirectionService, DirectionService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IPhoneNumberService, PhoneNumberService>();
        }

        /// <summary>
        /// Se agrega el servicio del identity y las opciones de la contraseña y bloqueo de la cuenta
        /// </summary>
        /// <param name="services"></param>
        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LamorenitaDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("Lamorenita");

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(31);
                options.Lockout.MaxFailedAccessAttempts = 8;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+&$ ";
                options.User.RequireUniqueEmail = false;
            });

        }
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var secret = config["AppSettings:JwtSecret"];
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opts =>
                {
                    opts.RequireHttpsMetadata = false;
                    opts.SaveToken = true;
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

    }
}
