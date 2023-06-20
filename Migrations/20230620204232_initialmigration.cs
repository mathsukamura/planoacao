using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace apiplanoacao.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "Varchar(24)", nullable: false),
                    email = table.Column<string>(type: "Varchar(64)", nullable: false),
                    senha = table.Column<string>(type: "Varchar(24)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_planoacao",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    id_colaborador_aprovador = table.Column<int>(type: "integer", nullable: false),
                    responsavel_tratativa = table.Column<int>(type: "integer", nullable: false),
                    descricao_acao = table.Column<string>(type: "varchar(240)", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "timestamp", nullable: false),
                    data_fim = table.Column<DateTime>(type: "timestamp", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_planoacao", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_planoacao_tb_usuario_id_colaborador_aprovador",
                        column: x => x.id_colaborador_aprovador,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_planoacao_tb_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanoAcao_Responsavel",
                columns: table => new
                {
                    id_planoacao = table.Column<int>(type: "integer", nullable: false),
                    id_responsavel = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoAcao_Responsavel", x => new { x.id_planoacao, x.id_responsavel });
                    table.ForeignKey(
                        name: "FK_PlanoAcao_Responsavel_tb_planoacao_id_responsavel",
                        column: x => x.id_responsavel,
                        principalTable: "tb_planoacao",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanoAcao_Responsavel_tb_usuario_id_planoacao",
                        column: x => x.id_planoacao,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanoAcao_Responsavel_id_responsavel",
                table: "PlanoAcao_Responsavel",
                column: "id_responsavel");

            migrationBuilder.CreateIndex(
                name: "IX_tb_planoacao_id_colaborador_aprovador",
                table: "tb_planoacao",
                column: "id_colaborador_aprovador");

            migrationBuilder.CreateIndex(
                name: "IX_tb_planoacao_id_usuario",
                table: "tb_planoacao",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanoAcao_Responsavel");

            migrationBuilder.DropTable(
                name: "tb_planoacao");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
