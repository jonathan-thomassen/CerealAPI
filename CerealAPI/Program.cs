using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Identity;

using CerealAPI.Contexts;
using CerealAPI.Repositories;
using CerealAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

var controllers = services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at
// https://aka.ms/aspnetcore/swashbuckle
controllers.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<CerealContext>();
services.AddDbContext<ImageContext>();
services.AddDbContext<UserContext>();

services.AddAuthorization();
var idBuilder = services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<UserContext>();

services.AddTransient<ICerealRepository, CerealRepository>();
services.AddTransient<IImageRepository, ImageRepository>();
services.AddTransient<ICerealService, CerealService>();
services.AddTransient<IImageService, ImageService>();

services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();
app.MapControllers();
app.Run();
