using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRegistration.Infrastructure.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime_Miniute",
                table: "Slot",
                newName: "StartMinute");

            migrationBuilder.RenameColumn(
                name: "StartTime_Hour",
                table: "Slot",
                newName: "StartHour");

            migrationBuilder.RenameColumn(
                name: "EndTime_Miniute",
                table: "Slot",
                newName: "EndMinute");

            migrationBuilder.RenameColumn(
                name: "EndTime_Hour",
                table: "Slot",
                newName: "EndHour");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartMinute",
                table: "Slot",
                newName: "StartTime_Miniute");

            migrationBuilder.RenameColumn(
                name: "StartHour",
                table: "Slot",
                newName: "StartTime_Hour");

            migrationBuilder.RenameColumn(
                name: "EndMinute",
                table: "Slot",
                newName: "EndTime_Miniute");

            migrationBuilder.RenameColumn(
                name: "EndHour",
                table: "Slot",
                newName: "EndTime_Hour");
        }
    }
}
