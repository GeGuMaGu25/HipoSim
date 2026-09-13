using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HipoSim.Platform.Migrations
{
    /// <inheritdoc />
    public partial class InitialLeadManagementContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditLeads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PropertyValue = table.Column<decimal>(type: "numeric", nullable: false),
                    DownPayment = table.Column<decimal>(type: "numeric", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    MonthlyPayment = table.Column<decimal>(type: "numeric", nullable: false),
                    Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditLeads", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditLeads");
        }
    }
}
