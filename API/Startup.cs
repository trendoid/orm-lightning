using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NJsonSchema;
using NSwag.AspNetCore;
using EFData;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = " Lightning API";
                    document.Info.Description = "A simple ASP.NET Core web API for demo purposes only";
                    document.Info.TermsOfService = "https://github.com/trendoid/orm-lightning/blob/master/README.md";
                    document.Info.Contact = new NSwag.SwaggerContact
                    {
                        Name = "Aaron Stanley King",
                        Email = "trendoid@gmail.com",
                        Url = "https://twitter.com/trendoid"
                    };
                    document.Info.License = new NSwag.SwaggerLicense
                    {
                        Name = "MIT",
                        Url = "https://github.com/trendoid/orm-lightning/blob/master/LICENSE"
                    };
                };
            });

            var connectionString = Configuration.GetConnectionString("LightningDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<LightningContext>();
            optionsBuilder.UseSqlServer(connectionString);

            services.AddScoped<LightningContext>(_ => new LightningContext(optionsBuilder.Options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseSwagger();
            app.UseSwaggerUi3();

            app.UseMvc();
        }
    }
}
