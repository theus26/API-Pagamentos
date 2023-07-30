using API_Produtos.DAL.DAO;
using API_Produtos.DAL.Entities;
using API_Produtos.Manager;
using API_Produtos.Manager.Interfaces;
using API_Produtos.Repository;
using API_Produtos.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Injen��o de Dependencia
builder.Services.AddScoped<IDAO<Produto>, BaseDAO<Produto>>();
builder.Services.AddScoped<IDAO<Vendas>, BaseDAO<Vendas>>();
builder.Services.AddScoped<IProdutosValidate, ProdutosValidate>();
builder.Services.AddScoped<IProdutosRepository, ProdutosRepository>();

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
