using AtmMonitoring.Api;
using AtmMonitoring.Core;

var builder = WebApplication.CreateBuilder(args);

// Configure Controllers and System.Text.Json source generation to eliminate runtime reflection overhead and reduce memory allocations during HTTP response serialization.
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
