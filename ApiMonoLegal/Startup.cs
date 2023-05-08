using ApiMonoLegal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
        services.AddControllers();

        //inyeccion

        services.AddSingleton<IClienteSettings>(d => d.GetRequiredService<IOptions<ClienteSettings>>().Value);
        
    }

    // Este método se utiliza para configurar la forma en que la aplicación responde a las solicitudes.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
