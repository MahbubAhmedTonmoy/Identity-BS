using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingSolution.Application.Service;
using BankingSolution.Data.Context;
using BankingSolution.Data.Repository;
using BankingSolution.Domain.CommandHandler;
using BankingSolution.Domain.Commands;
using BankingSolution.Domain.EventHandler;
using BankingSolution.Domain.Events;
using BankingSolution.Domain.Interface;
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
using Swashbuckle.AspNetCore.Swagger;

namespace BankingSolution
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
            services.AddDbContext<BankingDBContext>(o => {
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<LoanApproveEventHandler>();
            services.AddTransient<IEventHandler<LoanApproveEvent>, LoanApproveEventHandler>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Banking MicroService", Version = "v1" });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Banking MicroService V1");
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
            eventBus.Subscriber<LoanApproveEvent, LoanApproveEventHandler>();
        }
    }
}
