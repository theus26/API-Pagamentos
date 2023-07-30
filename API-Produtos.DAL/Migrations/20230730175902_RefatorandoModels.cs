using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Produtos.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RefatorandoModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vendas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaVenda",
                table: "produto",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ValorUltimaVenda",
                table: "produto",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataUltimaVenda",
                table: "produto");

            migrationBuilder.DropColumn(
                name: "ValorUltimaVenda",
                table: "produto");

            migrationBuilder.CreateTable(
                name: "vendas",
                columns: table => new
                {
                    IdVenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    DataUltimaVenda = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ValorUltimaVenda = table.Column<float>(type: "float", nullable: false),
                    nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    qtde_estoque = table.Column<int>(type: "int", nullable: false),
                    valor_unitario = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendas", x => x.IdVenda);
                    table.ForeignKey(
                        name: "FK_vendas_produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "produto",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_vendas_IdProduto",
                table: "vendas",
                column: "IdProduto");
        }
    }
}
