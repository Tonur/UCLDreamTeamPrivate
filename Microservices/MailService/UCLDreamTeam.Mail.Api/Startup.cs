using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RabbitMQ.IoC;
using UCLDreamTeam.Mail.Application.Interfaces;
using UCLDreamTeam.Mail.Application.Services;
using UCLDreamTeam.Mail.Data.Context;
using UCLDreamTeam.Mail.Data.Repository;
using UCLDreamTeam.Mail.Domain.CommandHandlers;
using UCLDreamTeam.Mail.Domain.Commands;
using UCLDreamTeam.Mail.Domain.EventHandlers;
using UCLDreamTeam.Mail.Domain.Events;
using UCLDreamTeam.Mail.Domain.Interfaces;
using UCLDreamTeam.Mail.Domain.Models;

namespace UCLDreamTeam.Mail.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMq();
            services.AddMediatR(typeof(Startup));
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericRepository<Resource>, GenericRepository<Resource>>();

            services.AddDbContext<MailDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MailDbConnection"));
            });

            //Command setup
            services.AddTransient<IRequestHandler<SendEmailCommand, bool>, SendEmailCommandHandler>();

            //Event setup
            services.AddTransient<ReservationCreatedEventHandler>();
            services.AddTransient<ResourceCreatedEventHandler>();
            services.AddTransient<ResourceUpdatedEventHandler>();
            services.AddTransient<ResourceDeletedEventHandler>();
            services.AddTransient<UserCreatedEventHandler>();
            services.AddTransient<UserUpdatedEventHandler>();
            services.AddTransient<UserDeletedEventHandler>();

            services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mail Microservice", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.
                    Enter 'Bearer' [space] {Token}.
                    Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //app.UseHttpsRedirection();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "Mail API V1"); });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            //Reservation
            app.Subscribe<ReservationCreatedEvent, ReservationCreatedEventHandler>();
            //User
            app.Subscribe<UserCreatedEvent, UserCreatedEventHandler>();
            app.Subscribe<UserUpdatedEvent, UserUpdatedEventHandler>();
            app.Subscribe<UserDeletedEvent, UserDeletedEventHandler>();
            //Resource
            app.Subscribe<ResourceCreatedEvent, ResourceCreatedEventHandler>();
            app.Subscribe<ResourceUpdatedEvent, ResourceUpdatedEventHandler>();
            app.Subscribe<ResourceDeletedEvent, ResourceDeletedEventHandler>();
        }
    }
}