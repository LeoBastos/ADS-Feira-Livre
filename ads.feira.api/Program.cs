using ads.feira.domain.Interfaces.Categories;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.domain.Interfaces.Identities;
using ads.feira.domain.Interfaces.Products;
using ads.feira.domain.Interfaces.Reviews;
using ads.feira.domain.Interfaces.Stores;
using ads.feira.domain.Interfaces.UnitOfWorks;
using ads.feira.Infra.Context;
using ads.feira.Infra.Repositories.Categories;
using ads.feira.Infra.Repositories.Cupons;
using ads.feira.Infra.Repositories.Identities;
using ads.feira.Infra.Repositories.Products;
using ads.feira.Infra.Repositories.Reviews;
using ads.feira.Infra.Repositories.Stores;
using ads.feira.Infra.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace ads.feira.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            // Configure AWS Cognito
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["Aws:Authority"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = "cognito:groups"
                };
            });

            // Add authorization
            builder.Services.AddAuthorization();
            builder.Services.AddCognitoIdentity();
            builder.Services.AddAWSService<Amazon.CognitoIdentityProvider.IAmazonCognitoIdentityProvider>();


            //Injections            
            builder.Services.AddScoped<ICognitoUserRepository, CognitoUserRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICuponRepository, CuponRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
            });

            var app = builder.Build();

            // Configure o pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
