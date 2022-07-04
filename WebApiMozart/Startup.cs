using ColegioMozart.Application;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Application.Common.Utils;
using ColegioMozart.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApiMozart.Filters;
using WebApiMozart.Models;
using WebApiMozart.Services;
using WebApiMozart.Services.TokenGenerators;
using WebApiMozart.Utils;

namespace WebApiMozart;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
        Configuration.Bind("Authentication", authenticationConfiguration);
        services.AddSingleton(authenticationConfiguration);

        services.AddSingleton<AccessTokenGenerator>();
        services.AddScoped<Authenticator>();
        services.AddSingleton<TokenGenerator>();


        services.AddApplication();
        services.AddInfrastructure(Configuration);

        services.AddHttpContextAccessor();

        services.AddSwaggerGen(options =>
        {
            string path = Assembly.GetEntryAssembly()!.GetName().Name + ".xml";
            SwaggerGenOptionsExtensions.IncludeXmlComments(options, Path.Combine(AppContext.BaseDirectory, path), false);

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });


            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                      In = ParameterLocation.Header,

                    },
                    new List<string>()
                  }
                });

        });

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();

        }).AddJsonOptions(j =>
        {
            j.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
            j.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());


            j.JsonSerializerOptions.Converters.Add(new TimeOnlyFormmatter());
        });

        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });
        });



    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();

        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseCors(options =>
        {
            options.AllowAnyOrigin();
            options.AllowAnyHeader();
            options.AllowAnyMethod();
        });

        app.UseRouting();

        app.UseAuthentication();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}");
        });

    }

}
