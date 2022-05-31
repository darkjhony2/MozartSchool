using ColegioMozart.Application;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Application.Common.Utils;
using ColegioMozart.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebApiMozart.Filters;
using WebApiMozart.Services;
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
        services.AddHttpContextAccessor();

        services.AddSwaggerGen(options =>
        {
            string path = Assembly.GetEntryAssembly()!.GetName().Name + ".xml";
            SwaggerGenOptionsExtensions.IncludeXmlComments(options, Path.Combine(AppContext.BaseDirectory, path), false);
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

        services.AddApplication();

        services.AddInfrastructure(Configuration);

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
