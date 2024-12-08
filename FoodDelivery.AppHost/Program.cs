var builder = DistributedApplication.CreateBuilder(args);

var kafka = builder.AddKafka("kafka").WithKafkaUI(
    kafkaUI => kafkaUI.WithHostPort(9100));

var postgres = builder.AddPostgres("postgress").WithPgAdmin();
var postgresdb = postgres.AddDatabase("postgres");

builder.AddProject<Projects.FoodDelivery>("fooddelivery").WithReference(postgresdb).WaitFor(postgresdb).WithReference(kafka);//.WithReference(kafka);
builder.AddProject<Projects.Packing>("packing");//.WithReference(kafka);

builder.Build().Run();
