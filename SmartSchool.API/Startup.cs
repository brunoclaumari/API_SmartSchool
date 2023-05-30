using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SmartSchool.API
{
    /// <summary>
    /// Inicializa configurações gerais da aplicação
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Interface de injeção de dependência em toda aplicação.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Este método é chamado em tempo de execução. Use este método para adicionar serviços ao container.
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );


            //services.AddDistributedMemoryCache();

            services.AddControllers()
                .AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRepository, Repository>();

            //Abaixo configurações para versionamento da API
            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
                opt.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
            });

            var apiProviderDescription = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
            {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = "SmartSchool API",
                        Version = description.ApiVersion.ToString(),
                        TermsOfService = new Uri("http://meustermosdeuso.com"),
                        Description = "Web API para cadastros da escola Smartschool",
                        License = new OpenApiLicense
                        {
                            Name = "SmartSchool License",
                            Url = new Uri("http://mit.com")
                        },
                        Contact = new OpenApiContact
                        {
                            Name = "Bruno de Sousa Silva",
                            Email = "desousadev1@gmail.com",
                            Url = new Uri("https://github.com/brunoclaumari")
                        }
                    });
                }

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                options.IncludeXmlComments(xmlCommentsFullPath);
            });


        }

        /// <summary>
        /// Este método é chamado em tempo de execução. Use este método para configurar um pipeline de requisições HTTP.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="apiProviderDescription"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiProviderDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger()
            .UseSwaggerUI(opt =>
            {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                //opt.RoutePrefix = "";
            });


            //app.UseAuthorization();

            //app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
