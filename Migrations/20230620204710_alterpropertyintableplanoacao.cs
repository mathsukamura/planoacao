using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiplanoacao.Migrations
{
    /// <inheritdoc />
    public partial class alterpropertyintableplanoacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_id_colaborador_aprovador",
                table: "tb_planoacao");

            migrationBuilder.RenameColumn(
                name: "id_colaborador_aprovador",
                table: "tb_planoacao",
                newName: "colaborador_aprovador");

            migrationBuilder.RenameIndex(
                name: "IX_tb_planoacao_id_colaborador_aprovador",
                table: "tb_planoacao",
                newName: "IX_tb_planoacao_colaborador_aprovador");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_aprovador",
                table: "tb_planoacao",
                column: "colaborador_aprovador",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_aprovador",
                table: "tb_planoacao");

            migrationBuilder.RenameColumn(
                name: "colaborador_aprovador",
                table: "tb_planoacao",
                newName: "id_colaborador_aprovador");

            migrationBuilder.RenameIndex(
                name: "IX_tb_planoacao_colaborador_aprovador",
                table: "tb_planoacao",
                newName: "IX_tb_planoacao_id_colaborador_aprovador");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_id_colaborador_aprovador",
                table: "tb_planoacao",
                column: "id_colaborador_aprovador",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
