using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SwaggerDemo.Data;
using System;
using System.IO;
using System.Reflection;

namespace SwaggerDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("MCS", "S1075:URIs não deve ser codigicada", Justification = "URL é estática")]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
             {
                 setupAction.Filters.Add(
                        new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                 setupAction.Filters.Add(
                        new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                 setupAction.Filters.Add(
                        new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                 setupAction.Filters.Add(
                        new ProducesDefaultResponseTypeAttribute());
             });


            services.AddControllers();

            services.AddDbContext<CustomerContext>(opt =>
                 opt.UseInMemoryDatabase("InMemoryCustomerDB")
              );

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "OpenAPISpecificationCustomer",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Customer API",
                        Version = "1",
                        Description = "Através desta API, você pode acessar os detalhes do cliente",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                        {
                            Email = "salinoit@gmail.com",
                            Name = "SalinoIT",
                            Url = new Uri("https://github.com/salinoit/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense()
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                setupAction.SwaggerDoc(
                  "OpenAPISpecificationWeatherDefault",
                  new Microsoft.OpenApi.Models.OpenApiInfo()
                  {
                      Title = "Weather default API",
                      Version = "1",
                      Description = "Através desta API, você pode acessar os detalhes do cliente",
                      Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                      {
                          Email = "salinoit@gmail.com",
                          Name = "SalinoIT",
                          Url = new Uri("https://github.com/salinoit/")
                      },
                      License = new Microsoft.OpenApi.Models.OpenApiLicense()
                      {
                          Name = "MIT License",
                          Url = new Uri("https://opensource.org/licenses/MIT")
                      }
                  });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
               {
                   setupAction.SwaggerEndpoint("/swagger/OpenAPISpecificationCustomer/swagger.json", "Customer API");
                   setupAction.SwaggerEndpoint("/swagger/OpenAPISpecificationWeatherDefault/swagger.json", "Weather default API");

                   setupAction.RoutePrefix = string.Empty;
               });
        }
    }
}
