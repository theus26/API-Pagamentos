using API_Produtos.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Produtos.DAL
{
    public class ProdutosContext : DbContext
    {
        public DbSet<Produto> produto { get; set; }
        public DbSet<Vendas> vendas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("PRODUTOS_CONNECTION");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }
    }
}
