using Oakton.Resources;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Packing;
using Packing.Infrastructure.Persistence;
using Wolverine;
using Wolverine.Kafka;

var builder = WebApplication.CreateBuilder(args);
const string serviceName = "Packing";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddOtlpExporter();
});
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddEntityFrameworkCoreInstrumentation()
        .AddSource("Wolverine")
        .AddOtlpExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddOtlpExporter());

builder.Services.InstallPacking("Data Source=Packing.db");

builder.Host.UseWolverine(opts =>
{
    opts.UseKafka("localhost:50553");
    
    opts.ListenToKafkaTopic("InventoryOrderProcessed").ProcessInline().BufferedInMemory();
    
    opts.Services.AddResourceSetupOnStartup();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

PackingInstaller.Configure(app.Services.GetService<PackingDbContext>());

app.Run();

