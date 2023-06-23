using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiplanoacao.Migrations
{
    /// <inheritdoc />
    public partial class alteratioinforkeycolaborador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_aprovador",
                table: "tb_planoacao");

            migrationBuilder.RenameColumn(
                name: "colaborador_aprovador",
                table: "tb_planoacao",
                newName: "colaborador_id");

            migrationBuilder.RenameIndex(
                name: "IX_tb_planoacao_colaborador_aprovador",
                table: "tb_planoacao",
                newName: "IX_tb_planoacao_colaborador_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_id",
                table: "tb_planoacao",
                column: "colaborador_id",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_id",
                table: "tb_planoacao");

            migrationBuilder.RenameColumn(
                name: "colaborador_id",
                table: "tb_planoacao",
                newName: "colaborador_aprovador");

            migrationBuilder.RenameIndex(
                name: "IX_tb_planoacao_colaborador_id",
                table: "tb_planoacao",
                newName: "IX_tb_planoacao_colaborador_aprovador");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_aprovador",
                table: "tb_planoacao",
                column: "colaborador_aprovador",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
