using ads.feira.api.Controllers;
using ads.feira.api.Helpers.EmailSender;
using ads.feira.api.Helpers.SeriLogs;
using ads.feira.api.Helpers.Settings;
using ads.feira.application.CQRS.Categories.Handlers.Queries;
using ads.feira.application.CQRS.Cupons.Handlers.Queries;
using ads.feira.application.CQRS.Products.Handlers.Queries;
using ads.feira.application.CQRS.Reviews.Handlers.Queries;
using ads.feira.application.CQRS.Stores.Handlers.Queries;
using ads.feira.application.Interfaces.Accounts;
using ads.feira.application.Interfaces.Categories;
using ads.feira.application.Interfaces.Cupons;
using ads.feira.application.Interfaces.ProductPlanServices;
using ads.feira.application.Interfaces.Products;
using ads.feira.application.Interfaces.Reviews;
using ads.feira.application.Interfaces.Stores;
using ads.feira.application.Mappings;
using ads.feira.application.Services.Accounts;
using ads.feira.application.Services.Categories;
using ads.feira.application.Services.Cupons;
using ads.feira.application.Services.ProductPlanServices;
using ads.feira.application.Services.Products;
using ads.feira.application.Services.Reviews;
using ads.feira.application.Services.Stores;
using ads.feira.application.Validators.Categories;
using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Interfaces.Accounts;
using ads.feira.domain.Interfaces.Categories;
using ads.feira.domain.Interfaces.Cupons;
using ads.feira.domain.Interfaces.Products;
using ads.feira.domain.Interfaces.Reviews;
using ads.feira.domain.Interfaces.Stores;
using ads.feira.domain.Interfaces.UnitOfWorks;
using ads.feira.domain.Seeds;
using ads.feira.Infra.Context;
using ads.feira.Infra.DataSeeds;
using ads.feira.Infra.Repositories.Accounts;
using ads.feira.Infra.Repositories.Categories;
using ads.feira.Infra.Repositories.Cupons;
using ads.feira.Infra.Repositories.Products;
using ads.feira.Infra.Repositories.Reviews;
using ads.feira.Infra.Repositories.Stores;
using ads.feira.Infra.UnitOfWorks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;


namespace ads.feira.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Serialog
            builder.Host.SerilogConfiguration();

            builder.Services.AddHttpContextAccessor();

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            builder.Services.AddIdentity<Account, IdentityRole>()
                           .AddEntityFrameworkStores<ApplicationDbContext>()
                           .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            });

            //S3BUCKET
            //var awsOptions = builder.Configuration.GetAWSOptions();
            //awsOptions.Region = RegionEndpoint.SAEast1; // Defina explicitamente a regi�o
            //builder.Services.AddDefaultAWSOptions(awsOptions);
            //builder.Services.AddAWSService<IAmazonS3>();
            //builder.Services.Configure<AWSConfiguration>(builder.Configuration.GetSection("AWSConfiguration"));
            //builder.Services.AddScoped<S3Service>();



            // Configure Auth - Secrets            
            builder.Configuration.AddUserSecrets<Program>();

            // Configura��o do JWT
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Configura��o do servi�o de email
            var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            builder.Services.AddSingleton<IEmailSender>(new EmailSender(
                smtpServer: emailSettings.SmtpServer,
                smtpPort: emailSettings.SmtpPort,
                smtpUser: emailSettings.SmtpUser,
                smtpPass: emailSettings.SmtpPass
            ));

            //Automapper
            builder.Services.AddAutoMapper(typeof(ApplicationServiceMappings));

            // MediatR
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetCategoryByIdQueryHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetCuponByIdQueryHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetProductByIdQueryHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetStoreByIdQueryHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetReviewByIdQueryHandler).Assembly);
            });

            //Services
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IAccountServices, AccountService>();
            builder.Services.AddScoped<ICuponService, CuponServices>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<IStoreServices, StoreServices>();
            builder.Services.AddScoped<IReviewServices, ReviewServices>();
            builder.Services.AddScoped<IProductPlanService, ProductPlanService>();


            //Repositories
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICuponRepository, CuponRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();

            //UnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            ////Validators
            builder.Services.AddFluentValidation(
                fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryValidator>());


            //Roles
            builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            //Seed Data
            builder.Services.AddScoped<ISeedDatabase, SeedDatabase>();


            builder.Services.AddControllers();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.SchemaFilter<EnumSchemaFilter>();
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Sistema de Cadastro de Feirantes",
                    Description = "Sistema desenvolvido em Asp.Net Core 8",
                    TermsOfService = new Uri("https://ads-terms-of-service.com.br"),
                    Contact = new OpenApiContact
                    {
                        Name = "Leonardo",
                        Email = "leonardobastos04@gmail.com",
                        Url = new Uri("https://leonardo-bastos.com.br")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://creativecommons.org/licenses/by/4.0")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

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

            builder.Services.AddOutputCache();


            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });


            var app = builder.Build();

            //Register Seeds
            //CriarPerfisUsuariosAsync(app);

            //Teste
            //PopulateDBAsync(app);


            // Configure o pipeline HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

            }
            //app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseOutputCache();

            app.Run();

            async Task CriarPerfisUsuariosAsync(WebApplication app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory?.CreateScope())
                {
                    var service = scope?.ServiceProvider.GetService<ISeedUserRoleInitial>();

                    await service.SeedRolesAsync();
                    await service.SeedUsersAsync();
                }
            }

            async Task PopulateDBAsync(WebApplication app)
            {
                using var scope = app.Services.CreateScope();
                var service = scope.ServiceProvider.GetService<ISeedDatabase>();
                if (service != null)
                {
                    await service.SeedDataDB(forceReseed: true);
                }
                else
                {
                    // Log um aviso ou lance uma exce��o
                    Console.WriteLine("ISeedDatabase service not found.");
                }
            }
        }
    }
}
