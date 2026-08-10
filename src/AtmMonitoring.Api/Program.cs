using AtmMonitoring.Core;
using AtmMonitoring.Api;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Register source-generated JSON serializer context to avoid reflection overhead and heap allocations during serialization
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.TypeInfoResolverChain.Insert(0, AtmJsonSerializerContext.Default);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAtmService, AtmService>();
var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
