using FoodDelivery.Installers;
using FoodDelivery.Presentation;
using Inventory;
using Inventory.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
using Packing;
using Wolverine;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(s => s.FullName.Replace("+", "."));
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
    
    c.SwaggerDoc("foodDelivery", new OpenApiInfo { Title = "Food Delivery", Version = "v1"});
    c.SwaggerDoc("inventory", new OpenApiInfo { Title = "Inventory", Version = "v1"});
    
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        var tags = apiDesc.ActionDescriptor.EndpointMetadata
            .OfType<TagsAttribute>()
            .SelectMany(t => t.Tags)
            .ToList();

        if (docName == "foodDelivery")
        {
            // Show only endpoints tagged with "main-api" on v1 Swagger document
            return tags.Any(tag => tag.StartsWith("FoodDelivery"));
        }
        if (docName == "inventory")
        {
            // Show only endpoints tagged with "new-api" on new-api Swagger document
            return tags.Any(tag => tag.StartsWith("Inventory"));
        }

        return false;
    });
});


//Old system
var connectionString = builder.Configuration.GetConnectionString("DeployedDatabase");
builder.Services.InstallPresentation(connectionString);

//New modules
//Inventory
var inventoryConnectionString = builder.Configuration.GetConnectionString("InventoryDatabase");
builder.Services.InstallInventory(inventoryConnectionString!);

//Packing
var packingConnectionString = builder.Configuration.GetConnectionString("PackingDatabase");
builder.Services.InstallPacking(packingConnectionString!);


var securityKey = builder.Configuration["AuthSettings:Key"];
builder.Services.ApiInstall(securityKey);

builder.Host.UseWolverine();

var app = builder.Build();

InventoryInstaller.SeedInventory(app.Services.GetService<InventoryDbContext>());

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Default Swagger page for main API
        c.SwaggerEndpoint("/swagger/foodDelivery/swagger.json", "Food Delivery v1");

        // Separate Swagger page for new API endpoints
        c.SwaggerEndpoint("/swagger/inventory/swagger.json", "Inventory v1");
        
        // Separate Swagger page for new API endpoints
        c.SwaggerEndpoint("/swagger/packing/swagger.json", "Packing v1");
    });
    
    app.Map("/new-api-docs", appBuilder =>
    {
        appBuilder.UseSwaggerUI(c =>
        {
            // Only show new API on this page
            c.SwaggerEndpoint("/swagger/inventory/swagger.json", "Inventory v1");
            c.RoutePrefix = string.Empty;  // Serve at this new route
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapWolverineEndpoints();

app.Run();
