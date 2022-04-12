using HelloApi.Middleware;
using HelloApi.Services;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            //.Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Seq(serverUrl: "http://host.docker.internal:5341")
            .WriteTo.Console()
            .CreateLogger();

try
{
    Log.Information("Starting web host");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}


// Add services to the container.

builder.Host.UseSerilog();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("Db");
var simpleProperty = builder.Configuration.GetValue<string>("SimpleProperty");
var nestedProperty = builder.Configuration.GetValue<string>("Inventory:NestedProperty");

//Log.ForContext("ConnectionString", connectionString)
//    .ForContext("SimpleProperty", simpleProperty)
//    .ForContext("NestedProperty", nestedProperty)
//    .Information("Loaded config!", connectionString);

var dbgView = builder.Configuration.GetDebugView();
Log.ForContext("ConfigurationDebug",dbgView)
    .Information("Dump", connectionString);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
