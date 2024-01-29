using Service.Carting.WebApi.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddResponseCaching();

builder.Logging.AddApplicationInsights();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureControllers();
builder.Services.AddWebApiIocConfig();

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart API V1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Cart API V2");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseResponseCaching();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();