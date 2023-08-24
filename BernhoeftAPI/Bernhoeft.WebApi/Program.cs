using Bernhoeft.Domain.Entities;
using Bernhoeft.Infra;
using Bernhoeft.Infra.Data;
using Bernhoeft.WebApi.Services.Interfaces;
using Bernhoeft.WebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

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

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDataContext>(options =>
    {
        options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Bernhoeft.Infra"));
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    var services = GetServiceCollection(builder);
}

IServiceCollection GetServiceCollection(WebApplicationBuilder builder)
{
    // Adicionando serviços
    var services = builder.Services;
    services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
    services.AddScoped<IReadRepository<Product>, ApplicationRepository<Product>>();
    services.AddScoped<IWriteRepository<Product>, ApplicationRepository<Product>>();
    services.AddScoped<IReadRepository<Category>, ApplicationRepository<Category>>();
    services.AddScoped<IWriteRepository<Category>, ApplicationRepository<Category>>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ICategoryService, CategoryService>();

    return services;
}