using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.IoC;
using UCLDreamTeam.Ticket.Data.Contexts;
using UCLDreamTeam.Ticket.Domain.EventHandlers;
using UCLDreamTeam.Ticket.Domain.Events;

namespace UCLDreamTeam.Ticket.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TicketDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TicketDbConnection")));

            services.AddRabbitMq();

            //Handler DI
            services.AddTransient<MessageSentEventHandler>();
            services.AddTransient<MessageSeenEventHandler>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            //Subscriptions
            app.Subscribe<MessageSentEvent, MessageSentEventHandler>();
            app.Subscribe<MessageSeenEvent, MessageSeenEventHandler>();
        }
    }
}
