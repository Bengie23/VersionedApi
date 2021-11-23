using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
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
using VersionedApi.Infrastructure;
using VersionedApi.Services;

namespace VersionedApi
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
            services.AddHttpContextAccessor();
            services.AddTransient<IVersionedMessageService, Services.V1.MessageService>();
            services.AddTransient<IVersionedMessageService, Services.V2.MessageService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VersionedApi", Version = "v1" });
            });

            services.AddScoped<IMessageService>((sp) =>
               {
                   var contextAccesor = sp.GetRequiredService<IHttpContextAccessor>();
                   var version = contextAccesor.HttpContext.GetVersionVariableFromContext();

                   var versionedServices = sp.GetRequiredService<IEnumerable<IVersionedMessageService>>();

                   return versionedServices.SingleOrDefault(service => service.Version.AsTypedVersion() == version);
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VersionedApi v1"));
            }

            app.UseMiddleware<VersionInterceptorMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
