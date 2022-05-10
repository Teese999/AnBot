using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnBot.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clicks",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Count = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastActivity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    CityChoise = table.Column<bool>(type: "bit", nullable: true),
                    FollowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MenuState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clicks = table.Column<long>(type: "bigint", nullable: false),
                    CleanFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiarySubscription = table.Column<bool>(type: "bit", nullable: false),
                    NewsSubscription = table.Column<bool>(type: "bit", nullable: false),
                    MeetingsSubscription = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clicks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
