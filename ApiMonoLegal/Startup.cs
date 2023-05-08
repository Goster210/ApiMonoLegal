using ApiMonoLegal.Models;
using ApiMonoLegal.Services;

using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMonoLegal;

public class Startup
{

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // Este método se utiliza para agregar servicios a la aplicación.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<ClienteSettings>(Configuration.GetSection(nameof(ClienteSettings)));
        //inyeccion
        services.AddSingleton<IClienteSettings>(d => d.GetRequiredService<IOptions<ClienteSettings>>().Value);
        services.AddSingleton<FacturaServices>();
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiMonoLegal", Version = "v1" });
        });
    }

    // Este método se utiliza para configurar la forma en que la aplicación responde a las solicitudes.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiMonoLegal v1"));
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
