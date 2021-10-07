using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppLib.EFCore.Migrations
{
    public partial class Mig0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InternalLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LogLevel = table.Column<string>(type: "TEXT", nullable: true),
                    EventId = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "TEXT", nullable: true),
                    InnerExceptionMessage = table.Column<string>(type: "TEXT", nullable: true),
                    RowTimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "datetime()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalLogs");
        }
    }
}
