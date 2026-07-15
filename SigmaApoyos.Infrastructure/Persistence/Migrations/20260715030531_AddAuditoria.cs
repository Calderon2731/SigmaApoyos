using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigmaApoyos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUDITORIA_TB",
                columns: table => new
                {
                    ID_AUDITORIA = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    USUARIO_NOMBRE = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ACCION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ENTIDAD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    REGISTRO_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    VALORES_ANTERIORES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VALORES_NUEVOS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FECHA_UTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DIRECCION_IP = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    RUTA = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUDITORIA_TB", x => x.ID_AUDITORIA);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AUDITORIA_TB_ENTIDAD_REGISTRO_ID",
                table: "AUDITORIA_TB",
                columns: new[] { "ENTIDAD", "REGISTRO_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_AUDITORIA_TB_FECHA_UTC",
                table: "AUDITORIA_TB",
                column: "FECHA_UTC");

            migrationBuilder.CreateIndex(
                name: "IX_AUDITORIA_TB_USUARIO_ID",
                table: "AUDITORIA_TB",
                column: "USUARIO_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUDITORIA_TB");
        }
    }
}
