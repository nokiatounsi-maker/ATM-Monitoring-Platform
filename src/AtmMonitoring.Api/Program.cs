using AtmMonitoring.Api;
using AtmMonitoring.Core;

var builder = WebApplication.CreateBuilder(args);

// Configure Controllers and register System.Text.Json source generation context.
// Adding AtmJsonContext.Default to TypeInfoResolverChain avoids reflection-based serialization,
// thereby reducing heap allocations and memory usage on every API request.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, AtmJsonContext.Default);
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAtmService, AtmService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
