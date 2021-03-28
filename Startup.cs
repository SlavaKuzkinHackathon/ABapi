using System;
using ABapi.Data;
using ABapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ABapi {
    public class Startup {
        public Startup (IConfiguration config) => Configuration = config;
        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddCors ();
            services.AddMvc ();
            services.AddControllersWithViews ();
            services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());
            services.AddTransient<IWebServiceRepository, WebServiceRepository> ();
            services.AddSwaggerGen (options => {
                options.SwaggerDoc (name: "v1", info : new OpenApiInfo { Title = "AB Service API", Version = "v1" });
            });

            string conString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext> (options =>
                options.UseNpgsql (conString));

            services.AddControllers (mvcOptions => mvcOptions.EnableEndpointRouting = false);
        }
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseCors (corsPolicyBuilder =>
                corsPolicyBuilder.WithOrigins ("http://localhost:3000")
                .AllowAnyMethod ()
                .AllowAnyHeader ());
            app.UseMvc ();
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Error");
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseDeveloperExceptionPage ();
            app.UseRouting ();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSwagger ();
            app.UseSwaggerUI (options => {
                options.SwaggerEndpoint ("/swagger/v1/swagger.json",
                    "AB Service API Version 1");
                options.SupportedSubmitMethods (new [] {
                    SubmitMethod.Get, SubmitMethod.Post,
                        SubmitMethod.Put, SubmitMethod.Delete
                });
            });
        }
    }
}