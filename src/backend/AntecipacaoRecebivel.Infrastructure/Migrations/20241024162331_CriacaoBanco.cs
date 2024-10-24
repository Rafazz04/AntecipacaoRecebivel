using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AntecipacaoRecebivel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Faturamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ramo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LimiteCredito = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualiza = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CARRINHO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorTotalBruto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorTotalLiquido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LimiteDeCreditoDisponivel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualiza = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARRINHO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CARRINHO_EMPRESA_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "EMPRESA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NOTAFISCAL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValorBruto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    CarrinhoId = table.Column<int>(type: "int", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualiza = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOTAFISCAL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NOTAFISCAL_CARRINHO_CarrinhoId",
                        column: x => x.CarrinhoId,
                        principalTable: "CARRINHO",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_NOTAFISCAL_EMPRESA_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "EMPRESA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CARRINHO_EmpresaId",
                table: "CARRINHO",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_NOTAFISCAL_CarrinhoId",
                table: "NOTAFISCAL",
                column: "CarrinhoId");

            migrationBuilder.CreateIndex(
                name: "IX_NOTAFISCAL_EmpresaId",
                table: "NOTAFISCAL",
                column: "EmpresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NOTAFISCAL");

            migrationBuilder.DropTable(
                name: "CARRINHO");

            migrationBuilder.DropTable(
                name: "EMPRESA");
        }
    }
}
