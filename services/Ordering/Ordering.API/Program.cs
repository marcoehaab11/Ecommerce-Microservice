using Asp.Versioning;
using Ordering.API.Extensions;
using Ordering.Infrastruture.Data;
using Ordering.Infrastruture.Extensions;
using Ordering.Application.Extensions;
namespace Ordering.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
