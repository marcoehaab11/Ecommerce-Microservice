using Discount.Application.Mapper;
using Discount.Application.Query;
using Discount.Core.Repositories;
using Discount.Infrastructure.Extentions;
using Discount.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAutoMapper(typeof(DiscoutProfile));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()
    , Assembly.GetAssembly(typeof(GetDiscountQuery))));
builder.Services.AddScoped<IDiscountRepository,DiscoutRepository>();
builder.Services.AddGrpc();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.MigrateDatabase<Program>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<Discount.API.Services.DiscountServices>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    });
});


app.Run();
