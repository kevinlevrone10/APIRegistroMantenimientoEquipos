using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIRegistroMantenimientoEquipos.Migrations
{
    /// <inheritdoc />
    public partial class Modelado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosEquipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosEquipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosMantenimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosMantenimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosReparacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosReparacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadoTrabajador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoTrabajador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposMantenimiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMantenimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaAdquisicion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipos_EstadosEquipo_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadosEquipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trabajadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    FechaContratacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trabajadores_EstadoTrabajador_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadoTrabajador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipoId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TipoId = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_EstadosMantenimiento_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadosMantenimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_TiposMantenimiento_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TiposMantenimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Trabajadores_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reparaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipoId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DescripcionProblema = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Solucion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CostoReparacion = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reparaciones_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reparaciones_EstadosReparacion_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "EstadosReparacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reparaciones_Trabajadores_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_EstadoId",
                table: "Equipos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_NumeroSerie",
                table: "Equipos",
                column: "NumeroSerie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_EquipoId",
                table: "Mantenimientos",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_EstadoId",
                table: "Mantenimientos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_TipoId",
                table: "Mantenimientos",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_TrabajadorId",
                table: "Mantenimientos",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_EquipoId",
                table: "Reparaciones",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_EstadoId",
                table: "Reparaciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparaciones_TrabajadorId",
                table: "Reparaciones",
                column: "TrabajadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajadores_Correo",
                table: "Trabajadores",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trabajadores_EstadoId",
                table: "Trabajadores",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Reparaciones");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "EstadosMantenimiento");

            migrationBuilder.DropTable(
                name: "TiposMantenimiento");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "EstadosReparacion");

            migrationBuilder.DropTable(
                name: "Trabajadores");

            migrationBuilder.DropTable(
                name: "EstadosEquipo");

            migrationBuilder.DropTable(
                name: "EstadoTrabajador");
        }
    }
}
