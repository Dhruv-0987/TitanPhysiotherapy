using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TitanPhysiotherapy.Migrations
{
    public partial class userentityremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "Staff",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "Staff",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "Patients",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "Patients",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "Patients");
        }
    }
}
