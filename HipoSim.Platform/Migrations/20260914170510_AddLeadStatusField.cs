using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HipoSim.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddLeadStatusField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "CreditLeads",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Pending");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CreditLeads");
        }
    }
}
