using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiplanoacao.Migrations
{
    /// <inheritdoc />
    public partial class removepropertyresponsaveltratativaofplanaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "responsavel_tratativa",
                table: "tb_planoacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "responsavel_tratativa",
                table: "tb_planoacao",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
