using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiplanoacao.Migrations
{
    /// <inheritdoc />
    public partial class alterationinforkeytableplanoacaoresponsavel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanoAcao_Responsavel_tb_planoacao_planoacaoid",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanoAcao_Responsavel_tb_usuario_responsaveltratativaid",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanoAcao_Responsavel",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.DropIndex(
                name: "IX_PlanoAcao_Responsavel_id_planoacao",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.DropIndex(
                name: "IX_PlanoAcao_Responsavel_responsaveltratativaid",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.DropColumn(
                name: "planoacaoid",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.DropColumn(
                name: "responsaveltratativaid",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanoAcao_Responsavel",
                table: "PlanoAcao_Responsavel",
                columns: new[] { "id_planoacao", "id_responsavel" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanoAcao_Responsavel",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.AddColumn<int>(
                name: "planoacaoid",
                table: "PlanoAcao_Responsavel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "responsaveltratativaid",
                table: "PlanoAcao_Responsavel",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanoAcao_Responsavel",
                table: "PlanoAcao_Responsavel",
                columns: new[] { "planoacaoid", "responsaveltratativaid" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanoAcao_Responsavel_id_planoacao",
                table: "PlanoAcao_Responsavel",
                column: "id_planoacao");

            migrationBuilder.CreateIndex(
                name: "IX_PlanoAcao_Responsavel_responsaveltratativaid",
                table: "PlanoAcao_Responsavel",
                column: "responsaveltratativaid");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanoAcao_Responsavel_tb_planoacao_planoacaoid",
                table: "PlanoAcao_Responsavel",
                column: "planoacaoid",
                principalTable: "tb_planoacao",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanoAcao_Responsavel_tb_usuario_responsaveltratativaid",
                table: "PlanoAcao_Responsavel",
                column: "responsaveltratativaid",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
