using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnticipationOfReceivables.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AnticipationOfReceivables");

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "AnticipationOfReceivables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BusinessSector = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Revenue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                schema: "AnticipationOfReceivables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrossTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetTotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvailableCreditLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "AnticipationOfReceivables",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "AnticipationOfReceivables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Cart_CartId",
                        column: x => x.CartId,
                        principalSchema: "AnticipationOfReceivables",
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Invoices_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "AnticipationOfReceivables",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CompanyId",
                schema: "AnticipationOfReceivables",
                table: "Cart",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CartId",
                schema: "AnticipationOfReceivables",
                table: "Invoices",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CompanyId",
                schema: "AnticipationOfReceivables",
                table: "Invoices",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "AnticipationOfReceivables");

            migrationBuilder.DropTable(
                name: "Cart",
                schema: "AnticipationOfReceivables");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "AnticipationOfReceivables");
        }
    }
}
