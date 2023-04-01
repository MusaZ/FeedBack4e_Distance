using FeedBack4e.Distance.Methods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICalculationDistance, CalculationDistance>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapControllerRoute(name: "default", "{controller}/{action}/");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();