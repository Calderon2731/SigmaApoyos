using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigmaApoyos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddObservacionesToExpediente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OBSERVACIONES",
                table: "EXPEDIENTES_TB",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OBSERVACIONES",
                table: "EXPEDIENTES_TB");
        }
    }
}
