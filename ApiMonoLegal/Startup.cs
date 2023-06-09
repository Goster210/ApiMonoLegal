﻿using ApiMonoLegal.Models;
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
using ApiMonoLegal.Repository;

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
        //inyeccion por medio de mi interfaz y los servicios
        services.AddSingleton<IFacturaServices, FacturaServices>();
        services.AddSingleton<IFacturaRepository, FacturaRepository>();
        services.AddSingleton<IEmailService, EmailService>();
        
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiMonoLegal", Version = "v1" });
        });
        // configuracion de cors para no tener conflictos con los puetos y direcciones de los endpoints
        services.AddCors(options => {
            options.AddPolicy("NuevaPolitica", app => {
                app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
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
        app.UseCors("NuevaPolitica");
        app.UseRouting();

        app.UseAuthorization();
        //peticiones a la api por medio del controlador (traigo los endpoints)
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
