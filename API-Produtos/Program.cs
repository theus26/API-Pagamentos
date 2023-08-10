using API_Produtos.DAL.DAO;
using API_Produtos.DAL.Entities;
using API_Produtos.Manager;
using API_Produtos.Manager.Interfaces;
using API_Produtos.Repository;
using API_Produtos.Repository.Interfaces;
using API_Produtos.Utils.IMapper;
using API_Produtos.Utils.Requests;
using API_Produtos.Utils.Requests.Interface;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var OpenCors = "_openCors";

// Configuring cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: OpenCors,
                        builder =>
                        {
                            builder.AllowAnyOrigin();
                            builder.WithMethods("PUT", "DELETE", "GET", "POST");
                            builder.AllowAnyHeader();
                        });
});

//Injen��o de Dependencia
builder.Services.AddScoped<IDAO<Produto>, BaseDAO<Produto>>();
builder.Services.AddScoped<IProdutosValidate, ProdutosValidate>();
builder.Services.AddScoped<IRequestPayament, RequestPayament>();
builder.Services.AddScoped<IProdutosRepository, ProdutosRepository>();
builder.Services.AddAutoMapper(typeof(Mappers));
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(OpenCors);

app.MapControllers();

app.Run();
