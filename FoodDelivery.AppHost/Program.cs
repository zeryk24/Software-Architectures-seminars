var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FoodDelivery>("fooddelivery");
builder.AddProject<Projects.Packing>("packing");

builder.Build().Run();
