using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TitanPhysiotherapy.Migrations
{
    public partial class treatmentchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "clinicLocation",
                table: "Treatment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "staffName",
                table: "Treatment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clinicLocation",
                table: "Treatment");

            migrationBuilder.DropColumn(
                name: "staffName",
                table: "Treatment");
        }
    }
}
