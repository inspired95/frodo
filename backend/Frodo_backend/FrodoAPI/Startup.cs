using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrodoAPI.JourneyRepository;
using FrodoAPI.TicketRepository;
using FrodoAPI.UserRepository;

namespace FrodoAPI
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
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.SetPreflightMaxAge(TimeSpan.FromDays(1)).AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));

            services.AddControllers();
            services.AddSingleton<IStationRepository, StationsRepo>();
            services.AddSingleton<ITicketProvider, TicketProvider>();
            services.AddSingleton<ITicketRepository, DummyTicketRepository>();
            services.AddSingleton<IUserRepository, UserRepository.UserRepository>();
            services.AddSingleton<IJourneyRepository, JourneyRepository.JourneyRepository>();
            services.AddSingleton<ITransportCompanyRepo, TransportCompanyRepo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("AllowAll"); 
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
