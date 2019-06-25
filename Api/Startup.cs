using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //Injeções de dependência
            IoCComunicacao.ConfigurarComunicacao(services);
            IoCRepositorio.ConfigurarRepositorio(services);
            IoCServico.ConfigurarServico(services);

            //Middlewares
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Nome Da API", Version = "v1" });
            });
            services.AddCors(options => {
                options.AddPolicy("PermissionamentoReact",
                    builder => builder.WithOrigins("http://localhost:3000")
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowAnyOrigin());
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("PermissionamentoReact");

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
