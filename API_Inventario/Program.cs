using API_Inventario.Db;
using API_Inventario.Repositorys;
using API_Inventario.Repositorys.Interfaces;
using API_Inventario.Services;
using API_Inventario.Services.Interfaces;
using API_Inventario.Utils.Mapper;
using API_Inventario.Validations;
using API_Inventario.Validations.CategoriaValidation;
using API_Inventario.Validations.ProveedorValidation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<Context>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);

builder.Services.AddAutoMapper(cfg => {}, typeof(MappingProfileProducto));

// Categoria Validator
builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoriaValidator>();

// Proveedor Validator
builder.Services.AddValidatorsFromAssemblyContaining<ProveedorValidator>();

// Producto Validator
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductoDTOValidator>();


builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
