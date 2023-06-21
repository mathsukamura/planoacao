using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiplanoacao.Migrations
{
    /// <inheritdoc />
    public partial class alterationinpropertynametableuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "tb_usuario",
                type: "Varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Varchar(24)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "tb_usuario",
                type: "Varchar(24)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Varchar(100)");
        }
    }
}
