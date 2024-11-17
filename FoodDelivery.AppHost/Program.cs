var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FoodDelivery>("fooddelivery");

builder.Build().Run();
