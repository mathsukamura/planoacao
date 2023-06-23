using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiplanoacao.Migrations
{
    /// <inheritdoc />
    public partial class alterationinpropertycolaborador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_aprovador",
                table: "tb_planoacao");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_id_usuario",
                table: "tb_planoacao");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_aprovador",
                table: "tb_planoacao",
                column: "colaborador_aprovador",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_id_usuario",
                table: "tb_planoacao",
                column: "id_usuario",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_aprovador",
                table: "tb_planoacao");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_id_usuario",
                table: "tb_planoacao");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_aprovador",
                table: "tb_planoacao",
                column: "colaborador_aprovador",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_id_usuario",
                table: "tb_planoacao",
                column: "id_usuario",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
