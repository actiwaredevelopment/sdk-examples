using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Module.Demo.Api.Processor.Demo.Extensions;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace Module.Demo.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            // Add singleton controller with default configuration
            services.AddTransient<Development.SDK.Logging.Controller.Logger>();

            services.AddProcessorDemoScope();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            // Add api controllers
            services.AddControllers()
                    .AddNewtonsoftJson();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(config =>
            {
                config.CustomSchemaIds(type => type.ToString());

                config.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Module: Demo",
                    Description = "",
                    TermsOfService = new Uri("https://www.company.com/terms"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Company AG",
                        Email = "info@company.com",
                        Url = new Uri("https://www.company.com")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://opensource.org/licenses/Apache-2.0")
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFiles = System.IO.Directory.GetFiles(AppContext.BaseDirectory, "*.xml");

                if (xmlFiles != null &&
                    xmlFiles.Length > 0)
                {
                    foreach (string xmlFile in xmlFiles)
                    {
                        if (System.IO.Path.GetFileNameWithoutExtension(xmlFile).StartsWith("Module.Demo.Service") == true)
                        {
                            config.IncludeXmlComments(xmlFile);
                        }
                    }
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(config =>
            {
                config.RouteTemplate = "docs/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/docs/api/v1/swagger.json", "Module: Demo - API V1");
                config.RoutePrefix = "docs/api";
            });

            app.UseCors("AllPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "..\\..\\configuration";
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
