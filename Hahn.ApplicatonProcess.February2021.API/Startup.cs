using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.February2021.Domain.Validators;
using Hahn.ApplicatonProcess.February2021.Data;
using Microsoft.EntityFrameworkCore;
using Hahn.ApplicatonProcess.February2021.Data.Contracts;
using Hahn.ApplicatonProcess.February2021.Data.Repositories;
using Hahn.ApplicatonProcess.February2021.Domain.Contracts;
using Hahn.ApplicatonProcess.February2021.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Hahn.ApplicatonProcess.February2021.Models;
using Microsoft.Extensions.Logging;
using Hahn.ApplicatonProcess.February2021.API.Filters;
using System.IO;
using System.Reflection;
using System;

namespace Hahn.ApplicatonProcess.February2021.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(o => o.Filters.Add(typeof(AuditActionFilter)))
               .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AssetValidator>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                  Title = "Asset Management API", 
                  Version = "v1" ,
                  Description = "API's to manage Asset Information",
                  TermsOfService = new Uri("https://robyjj.com/terms"), // link does not work :)
                  Contact = new OpenApiContact
                  {
                      Name = "Roby James John",
                      Email = "robyjamesjohnr@gmail.com",
                      Url = new Uri("https://www.linkedin.com/in/roby-james-john/"),
                  }

                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<IRepository<Asset>, AssetRepository>();
            services.AddDbContext<AssetContext>(options =>
                    options.UseInMemoryDatabase("Asset"));

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
            });
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.February2021.API v1"));
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
        }
    }
}
