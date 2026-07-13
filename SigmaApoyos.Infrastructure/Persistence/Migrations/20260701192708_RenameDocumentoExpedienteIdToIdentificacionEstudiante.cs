using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigmaApoyos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameDocumentoExpedienteIdToIdentificacionEstudiante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOCUMENTOS_EXPEDIENTES_TB_EXPEDIENTE_ID",
                table: "DOCUMENTOS");

            migrationBuilder.RenameColumn(
                name: "EXPEDIENTE_ID",
                table: "DOCUMENTOS",
                newName: "IDENTIFICACION_ESTUDIANTE");

            migrationBuilder.RenameIndex(
                name: "IX_DOCUMENTOS_EXPEDIENTE_ID",
                table: "DOCUMENTOS",
                newName: "IX_DOCUMENTOS_IDENTIFICACION_ESTUDIANTE");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FECHA_NACIMIENTO",
                table: "EXPEDIENTES_TB",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_DOCUMENTOS_EXPEDIENTES_TB_IDENTIFICACION_ESTUDIANTE",
                table: "DOCUMENTOS",
                column: "IDENTIFICACION_ESTUDIANTE",
                principalTable: "EXPEDIENTES_TB",
                principalColumn: "IDENTIFICACION_ESTUDIANTE",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOCUMENTOS_EXPEDIENTES_TB_IDENTIFICACION_ESTUDIANTE",
                table: "DOCUMENTOS");

            migrationBuilder.RenameColumn(
                name: "IDENTIFICACION_ESTUDIANTE",
                table: "DOCUMENTOS",
                newName: "EXPEDIENTE_ID");

            migrationBuilder.RenameIndex(
                name: "IX_DOCUMENTOS_IDENTIFICACION_ESTUDIANTE",
                table: "DOCUMENTOS",
                newName: "IX_DOCUMENTOS_EXPEDIENTE_ID");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FECHA_NACIMIENTO",
                table: "EXPEDIENTES_TB",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_DOCUMENTOS_EXPEDIENTES_TB_EXPEDIENTE_ID",
                table: "DOCUMENTOS",
                column: "EXPEDIENTE_ID",
                principalTable: "EXPEDIENTES_TB",
                principalColumn: "IDENTIFICACION_ESTUDIANTE",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
