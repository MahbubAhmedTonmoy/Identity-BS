using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingTransfer.Application.Service;
using BankingTransfer.Data.Context;
using BankingTransfer.Data.Repository;
using BankingTransfer.Domain.CommandHandlers;
using BankingTransfer.Domain.Commands;
using BankingTransfer.Domain.EventHandlers;
using BankingTransfer.Domain.Events;
using BusDomainCore.Bus;
using IOC;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace BankingTransfer
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
            services.AddDbContext<BankingDBContext_Transfer>(o => {
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddTransient<IRequestHandler<CrateLoanCommand, bool>, LoanApproveCommandHandler>();
            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<ITransferRepositoy, TransferRepositoy>();
            services.AddTransient<TransferEventHandler>();
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transfer MicroService", Version = "v1" });
            });
            services.AddMediatR(typeof(Startup));

            RegisterServices(services);
        }
        private void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
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

            app.UseAuthorization();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transfer MicroService V1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            ConfigureEventBus(app);
        }
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscriber<TransferCreatedEvent, TransferEventHandler>();
        }
    }
}
