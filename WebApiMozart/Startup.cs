using ColegioMozart.Application;
using ColegioMozart.Application.Common.Interfaces;
using ColegioMozart.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WebApiMozart.Filters;
using WebApiMozart.Services;

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

        services.AddSwaggerGen();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
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

        app.UseCors(options => options.AllowAnyOrigin());

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
