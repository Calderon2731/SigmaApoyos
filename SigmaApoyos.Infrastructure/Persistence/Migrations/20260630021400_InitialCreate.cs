using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigmaApoyos.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ESTADO_TB",
                columns: table => new
                {
                    ID_ESTADO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ESTADO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADO_TB", x => x.ID_ESTADO);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_ADECUACION_TB",
                columns: table => new
                {
                    ID_TIPO_ADECUACION = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_ADECUACION_TB", x => x.ID_TIPO_ADECUACION);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_DOCUMENTOS",
                columns: table => new
                {
                    ID_TIPO_DOCUMENTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TIPO = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_DOCUMENTOS", x => x.ID_TIPO_DOCUMENTO);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EXPEDIENTES_TB",
                columns: table => new
                {
                    IDENTIFICACION_ESTUDIANTE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NOMBRE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PRIMER_APELLIDO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SEGUNDO_APELLIDO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FECHA_NACIMIENTO = table.Column<DateOnly>(type: "date", nullable: false),
                    NOMBRE_ENCARGADO = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TELEFONO_ENCARGADO = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ID_TIPO_ADECUACION = table.Column<int>(type: "int", nullable: false),
                    ID_ESTADO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXPEDIENTES_TB", x => x.IDENTIFICACION_ESTUDIANTE);
                    table.ForeignKey(
                        name: "FK_EXPEDIENTES_TB_ESTADO_TB_ID_ESTADO",
                        column: x => x.ID_ESTADO,
                        principalTable: "ESTADO_TB",
                        principalColumn: "ID_ESTADO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EXPEDIENTES_TB_TIPO_ADECUACION_TB_ID_TIPO_ADECUACION",
                        column: x => x.ID_TIPO_ADECUACION,
                        principalTable: "TIPO_ADECUACION_TB",
                        principalColumn: "ID_TIPO_ADECUACION",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DOCUMENTOS",
                columns: table => new
                {
                    ID_DOCUMENTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EXPEDIENTE_ID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TIPO_DOCUMENTO = table.Column<int>(type: "int", nullable: false),
                    USUARIO_ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CONSECUTIVO = table.Column<int>(type: "int", nullable: false),
                    RUTA_ARCHIVO = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FECHA_SUBIDA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_ESTADO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOCUMENTOS", x => x.ID_DOCUMENTO);
                    table.ForeignKey(
                        name: "FK_DOCUMENTOS_ESTADO_TB_ID_ESTADO",
                        column: x => x.ID_ESTADO,
                        principalTable: "ESTADO_TB",
                        principalColumn: "ID_ESTADO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DOCUMENTOS_EXPEDIENTES_TB_EXPEDIENTE_ID",
                        column: x => x.EXPEDIENTE_ID,
                        principalTable: "EXPEDIENTES_TB",
                        principalColumn: "IDENTIFICACION_ESTUDIANTE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DOCUMENTOS_TIPO_DOCUMENTOS_TIPO_DOCUMENTO",
                        column: x => x.TIPO_DOCUMENTO,
                        principalTable: "TIPO_DOCUMENTOS",
                        principalColumn: "ID_TIPO_DOCUMENTO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTOS_EXPEDIENTE_ID",
                table: "DOCUMENTOS",
                column: "EXPEDIENTE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTOS_ID_ESTADO",
                table: "DOCUMENTOS",
                column: "ID_ESTADO");

            migrationBuilder.CreateIndex(
                name: "IX_DOCUMENTOS_TIPO_DOCUMENTO",
                table: "DOCUMENTOS",
                column: "TIPO_DOCUMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_EXPEDIENTES_TB_ID_ESTADO",
                table: "EXPEDIENTES_TB",
                column: "ID_ESTADO");

            migrationBuilder.CreateIndex(
                name: "IX_EXPEDIENTES_TB_ID_TIPO_ADECUACION",
                table: "EXPEDIENTES_TB",
                column: "ID_TIPO_ADECUACION");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DOCUMENTOS");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EXPEDIENTES_TB");

            migrationBuilder.DropTable(
                name: "TIPO_DOCUMENTOS");

            migrationBuilder.DropTable(
                name: "ESTADO_TB");

            migrationBuilder.DropTable(
                name: "TIPO_ADECUACION_TB");
        }
    }
}
