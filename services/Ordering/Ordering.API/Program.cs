using Asp.Versioning;
using Common.Logging;
using EventBus.Messages.Commons;
using MassTransit;
using Ordering.API.Extensions;
using Ordering.Application.EventBusConsumer;
using Ordering.Application.Extensions;
using Ordering.Infrastruture.Data;
using Ordering.Infrastruture.Extensions;
using Serilog;
namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog(Logging.ConfigureLogger);

            // Add services to the container.
            builder.Services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                opt.ReportApiVersions = true;
            });
            builder.Services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Ordering.API",
                    Version = "v1",
                    Description = "Ordering Microservice",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Marco Ehab",
                        Email = "maroehab1235@gmail.com",
                        Url = new Uri("https://marcoehab.com")
                    }
                })
            );
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfraServices(builder.Configuration);

            builder.Services.AddScoped<BasketOrderingConsumer>();
            builder.Services.AddScoped<BasketOrderingConsumerV2>();
            builder.Services.AddMassTransit(confg =>
            {
                //Mark this as Consumer
                confg.AddConsumer<BasketOrderingConsumer>();
                confg.UsingRabbitMq((ct, cfg) =>
                {
                    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);

                    //provide the queue name with consumer 
                    cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, ept =>
                    {
                        ept.ConfigureConsumer<BasketOrderingConsumer>(ct);
                    });
                    cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueueV2, ept =>
                    {
                        ept.ConfigureConsumer<BasketOrderingConsumerV2>(ct);
                    });
                });
            });
            builder.Services.AddMassTransitHostedService();

            var app = builder.Build();

            app.MigrateDatabase<OrderDbContext>((context, services) =>
            {
                var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();
                OrderContextSeed.SeedAsync(context, logger).Wait();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
