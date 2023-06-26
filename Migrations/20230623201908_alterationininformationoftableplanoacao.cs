using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiplanoacao.Migrations
{
    /// <inheritdoc />
    public partial class alterationininformationoftableplanoacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanoAcao_Responsavel_tb_planoacao_id_planoacao",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanoAcao_Responsavel_tb_usuario_id_responsavel",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_id",
                table: "tb_planoacao");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_id_usuario",
                table: "tb_planoacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanoAcao_Responsavel",
                table: "PlanoAcao_Responsavel");

            migrationBuilder.RenameTable(
                name: "PlanoAcao_Responsavel",
                newName: "planoAcao_responsavel");

            migrationBuilder.RenameIndex(
                name: "IX_PlanoAcao_Responsavel_id_responsavel",
                table: "planoAcao_responsavel",
                newName: "IX_planoAcao_responsavel_id_responsavel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_planoAcao_responsavel",
                table: "planoAcao_responsavel",
                columns: new[] { "id_planoacao", "id_responsavel" });

            migrationBuilder.AddForeignKey(
                name: "FK_planoAcao_responsavel_tb_planoacao_id_planoacao",
                table: "planoAcao_responsavel",
                column: "id_planoacao",
                principalTable: "tb_planoacao",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_planoAcao_responsavel_tb_usuario_id_responsavel",
                table: "planoAcao_responsavel",
                column: "id_responsavel",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_id",
                table: "tb_planoacao",
                column: "colaborador_id",
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
                name: "FK_planoAcao_responsavel_tb_planoacao_id_planoacao",
                table: "planoAcao_responsavel");

            migrationBuilder.DropForeignKey(
                name: "FK_planoAcao_responsavel_tb_usuario_id_responsavel",
                table: "planoAcao_responsavel");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_id",
                table: "tb_planoacao");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_planoacao_tb_usuario_id_usuario",
                table: "tb_planoacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_planoAcao_responsavel",
                table: "planoAcao_responsavel");

            migrationBuilder.RenameTable(
                name: "planoAcao_responsavel",
                newName: "PlanoAcao_Responsavel");

            migrationBuilder.RenameIndex(
                name: "IX_planoAcao_responsavel_id_responsavel",
                table: "PlanoAcao_Responsavel",
                newName: "IX_PlanoAcao_Responsavel_id_responsavel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanoAcao_Responsavel",
                table: "PlanoAcao_Responsavel",
                columns: new[] { "id_planoacao", "id_responsavel" });

            migrationBuilder.AddForeignKey(
                name: "FK_PlanoAcao_Responsavel_tb_planoacao_id_planoacao",
                table: "PlanoAcao_Responsavel",
                column: "id_planoacao",
                principalTable: "tb_planoacao",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanoAcao_Responsavel_tb_usuario_id_responsavel",
                table: "PlanoAcao_Responsavel",
                column: "id_responsavel",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_planoacao_tb_usuario_colaborador_id",
                table: "tb_planoacao",
                column: "colaborador_id",
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
