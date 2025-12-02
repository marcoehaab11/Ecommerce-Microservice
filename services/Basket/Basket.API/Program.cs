using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Mapper;
using Basket.Core.Repositories;
using Basket.Infrastructure.Repositories;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Common.Logging;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog(Logging.ConfigureLogger);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(BasketMappingProfile));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()
    , Assembly.GetAssembly(typeof(CreateShoppingCartCommand))));

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddGrpcClient<Discount.Grpc.Protos.DiscountProtoService.DiscountProtoServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://id-local.eshopping.com:44344";
        options.RequireHttpsMetadata = true;

        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://id-local.eshopping.com:44344",
            ValidateAudience = true,
            ValidAudience = "Basket",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
        //add this to docker host 
        options.BackchannelHttpHandler = new System.Net.Http.HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError("Authentication failed: {0}", context.Exception.Message);
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Token validated for {0}", context.Principal.Identity.Name);
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(confg =>

{
    confg.UsingRabbitMq((ct, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
    });
});
builder.Services.AddMassTransitHostedService();
var userPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(userPolicy));
});

builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    opt.ReportApiVersions = true;
}).AddApiExplorer(opt=>
{
    opt.GroupNameFormat = "'v'VVV";
    opt.SubstituteApiVersionInUrl = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Basket.API",
        Version = "v1",
        Description = "Catalog Microservice",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Marco Ehab",
            Email = "maroehab1235@gmail.com",
            Url = new Uri("https://marcoehab.com")
        }
    });
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Basket.API",
        Version = "v2",
        Description = "Catalog Microservice",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Marco Ehab",
            Email = "maroehab1235@gmail.com",
            Url = new Uri("https://marcoehab.com")
        }
    });

    options.DocInclusionPredicate((version, desc) =>
    {
        if(!desc.TryGetMethodInfo(out var methodInfo)) return false;

        var versions = methodInfo.DeclaringType?
            .GetCustomAttributes(true)
            .OfType<Asp.Versioning.ApiVersionAttribute>()
            .SelectMany(attr => attr.Versions);
        return versions?.Any(v => $"v{v.ToString()}" == version) ?? false;
    });
}


);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

var app = builder.Build();

// BEFORE UseSwagger / routing
app.Use((ctx, next) =>
{
    if (ctx.Request.Headers.TryGetValue("X-Forwarded-Prefix", out var p) && !string.IsNullOrEmpty(p))
        ctx.Request.PathBase = p.ToString();   // e.g., "/catalog"
    return next();
});

app.UseSwagger(c =>
{
    // Make the OpenAPI "servers" base path match the prefix so Try it out uses /catalog/...
    c.PreSerializeFilters.Add((doc, req) =>
    {
        var prefix = req.Headers["X-Forwarded-Prefix"].FirstOrDefault();
        if (!string.IsNullOrEmpty(prefix))
            doc.Servers = new List<Microsoft.OpenApi.Models.OpenApiServer>
            { new() { Url = prefix } };
    });
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Catalog.API v1"); // relative path (no leading '/')
    c.RoutePrefix = "swagger";
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
