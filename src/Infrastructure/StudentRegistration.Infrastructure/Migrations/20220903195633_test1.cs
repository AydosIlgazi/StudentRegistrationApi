using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentRegistration.Infrastructure.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    IsEnrollmentActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    SemesterType = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailySlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    TermWeeklySlotsTermId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailySlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailySlots_Terms_TermWeeklySlotsTermId",
                        column: x => x.TermWeeklySlotsTermId,
                        principalTable: "Terms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    DailySlotsId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    SlotName = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime_Hour = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime_Miniute = table.Column<int>(type: "INTEGER", nullable: false),
                    EndTime_Hour = table.Column<int>(type: "INTEGER", nullable: false),
                    EndTime_Miniute = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => new { x.DailySlotsId, x.Id });
                    table.ForeignKey(
                        name: "FK_Slot_DailySlots_DailySlotsId",
                        column: x => x.DailySlotsId,
                        principalTable: "DailySlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailySlots_TermWeeklySlotsTermId",
                table: "DailySlots",
                column: "TermWeeklySlotsTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "DailySlots");

            migrationBuilder.DropTable(
                name: "Terms");
        }
    }
}
