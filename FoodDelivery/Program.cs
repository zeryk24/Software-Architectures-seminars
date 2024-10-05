using FoodDelivery.BL.Installers;
using FoodDelivery.DAL.Installers;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using FoodDelivery.Installers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                        Enter 'Bearer' [space] and then your token in the text input below.
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
                Scheme = "0auth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
    
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Food Delivery", Version = "v1"});
});

var connectionString = builder.Configuration.GetConnectionString("DeployedDatabase");
var securityKey = builder.Configuration["AuthSettings:Key"];
builder.Services.DalInstall(connectionString);
builder.Services.BlInstall();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ApiInstall(securityKey);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
