using Microsoft.EntityFrameworkCore;
using ApiNet6.Data;
using ApiNet6.Services;
using ApiNet6.Repositories;
using ApiNet6.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar servicios
builder.Services.AddScoped<RockitService>();
builder.Services.AddScoped<HarmonizedService>();
builder.Services.AddScoped<ProductService>();  

// Registrar repositorios
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMovementRepository, MovementRepository>();
builder.Services.AddScoped<IRepository<MovementItem>, Repository<MovementItem>>();    
builder.Services.AddScoped<IRepository<PaymentMethod>, Repository<PaymentMethod>>();  


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