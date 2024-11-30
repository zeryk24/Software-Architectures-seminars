var builder = DistributedApplication.CreateBuilder(args);

// var kafka = builder.AddKafka("kafka").WithKafkaUI(
//     kafkaUI => kafkaUI.WithHostPort(9100));

builder.AddProject<Projects.FoodDelivery>("fooddelivery");//.WithReference(kafka);
builder.AddProject<Projects.Packing>("packing");//.WithReference(kafka);

builder.Build().Run();
