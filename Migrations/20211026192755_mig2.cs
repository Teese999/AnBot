using Microsoft.EntityFrameworkCore.Migrations;

namespace AnBot.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_Clicks",
                table: "Clicks",
                column: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Clicks",
                table: "Clicks");
        }
    }
}
