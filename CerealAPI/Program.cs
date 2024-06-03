using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Identity;

using CerealAPI.Contexts;
using CerealAPI.Repositories;
using CerealAPI.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceCollection services = builder.Services;

IMvcBuilder controllers = services.AddControllers();
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
IdentityBuilder idBuilder = services.AddIdentityApiEndpoints<IdentityUser>()
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

WebApplication app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGroup("user").MapIdentityApi<IdentityUser>();
app.MapControllers();
app.Run();
