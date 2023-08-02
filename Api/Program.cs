using Api;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddSingleton<IRepository>(
    new Repository(await InitializeCosmosClientInstanceAsync(builder.Configuration)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task<Container> InitializeCosmosClientInstanceAsync(IConfiguration configuration)
{
    var client = new CosmosClient(configuration.GetValue<string>("CosmosDB:Account"),
        configuration.GetValue<string>("CosmosDB:Key"));

    var dbName = configuration.GetValue<string>("CosmosDB:DatabaseName");
    var containerName = configuration.GetValue<string>("CosmosDB:ContainerName");
    
    var database = await client.CreateDatabaseIfNotExistsAsync(dbName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/user_id");

    return client.GetContainer(dbName, containerName);
}