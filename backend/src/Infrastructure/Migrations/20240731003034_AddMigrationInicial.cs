using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    crm = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.id);
                    table.UniqueConstraint("AK_Medico_documento_crm", x => new { x.documento, x.crm });
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.id);
                    table.UniqueConstraint("AK_Paciente_documento", x => x.documento);
                });

            migrationBuilder.CreateTable(
                name: "Agenda",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    data_agendamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status_agendamento = table.Column<int>(type: "int", nullable: false),
                    id_medico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_agendamento = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agenda", x => x.id);
                    table.UniqueConstraint("AK_Agenda_data_agendamento", x => x.data_agendamento);
                    table.ForeignKey(
                        name: "FK_Agenda_Medico_id_medico",
                        column: x => x.id_medico,
                        principalTable: "Medico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_paciente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_agenda = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    atualizado_em = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.id);
                    table.ForeignKey(
                        name: "FK_Agendamento_Agenda_id_agenda",
                        column: x => x.id_agenda,
                        principalTable: "Agenda",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "Paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_id_medico",
                table: "Agenda",
                column: "id_medico");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_id_agenda",
                table: "Agendamento",
                column: "id_agenda",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_id_paciente",
                table: "Agendamento",
                column: "id_paciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "Agenda");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Medico");
        }
    }
}
