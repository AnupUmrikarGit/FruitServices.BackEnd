using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Serilog;

using Microsoft.AspNetCore.ResponseCompression;

using Newtonsoft.Json;
using System.Net.Http.Headers;
using FruitServices.Application.DependencyResolver;
using FruitServices.Infrastructure.DependencyResolver;

Microsoft.AspNetCore.Builder.WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Todo: add Authentication Server Code
/*
builder.Services.AddAuthentication("Bearer")
              .AddJwtBearer("Bearer", opt =>
              {
                  opt.Authority = "";
                  opt.Audience = "";
                  opt.RequireHttpsMetadata = true;
              });
*/

// ResponseCompression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});


builder.Services.AddApplicationServices(configuration);
builder.Services.AddInfrastructureServices(configuration);

builder.Services.AddHttpClient("Fruityvice", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["HttpClients:Fruityvice:ApiUrl"]);
    httpClient.DefaultRequestHeaders.Clear();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpContextAccessor();

builder.Host.UseSerilog(((ctx, lc) =>
                                {                                
                                    lc.ReadFrom.Configuration(ctx.Configuration);

                                }));

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;

});
builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddCors(options =>
{
   string[] allowedOrigins = builder.Configuration.GetSection("AllowedOriginsKey").Get<string[]>();
   options.AddPolicy(name: "AllowOrigin",
       builder =>
       {
           builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
       });
});
builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FruitServices.API API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});


var app = builder.Build();

app.UseResponseCompression();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
//app.UseHttpsRedirection();


//app.UseAuthentication();
//app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllers();

app.Run();

