using System.Text.Json.Serialization;

using CerealAPI.Contexts;
using CerealAPI.Repositories;
using CerealAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

var controllers = services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
controllers.AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<CerealContext>();

services.AddTransient<ICerealRepository, CerealRepository>();
services.AddTransient<ICerealService, CerealService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
