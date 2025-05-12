using Presentation.Interfaces;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddScoped<IEventService, EventService>();


var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
