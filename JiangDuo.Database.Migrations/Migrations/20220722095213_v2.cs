using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiangDuo.Database.Migrations.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Building");

            migrationBuilder.RenameColumn(
                name: "OldName",
                table: "sys_uploadfile",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "sys_uploadfile",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Building",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 476, DateTimeKind.Unspecified).AddTicks(9742), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 476, DateTimeKind.Unspecified).AddTicks(9749), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 476, DateTimeKind.Unspecified).AddTicks(9751), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 476, DateTimeKind.Unspecified).AddTicks(9752), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 476, DateTimeKind.Unspecified).AddTicks(9754), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 476, DateTimeKind.Unspecified).AddTicks(9799), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 476, DateTimeKind.Unspecified).AddTicks(9802), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 476, DateTimeKind.Unspecified).AddTicks(9804), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 483, DateTimeKind.Unspecified).AddTicks(421), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_user",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 9, 52, 13, 487, DateTimeKind.Unspecified).AddTicks(2879), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "sys_uploadfile");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Building");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "sys_uploadfile",
                newName: "OldName");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Building",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 832, DateTimeKind.Unspecified).AddTicks(7727), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 832, DateTimeKind.Unspecified).AddTicks(7731), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 832, DateTimeKind.Unspecified).AddTicks(7733), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 832, DateTimeKind.Unspecified).AddTicks(7734), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 832, DateTimeKind.Unspecified).AddTicks(7735), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 832, DateTimeKind.Unspecified).AddTicks(7761), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 832, DateTimeKind.Unspecified).AddTicks(7763), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 832, DateTimeKind.Unspecified).AddTicks(7764), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 835, DateTimeKind.Unspecified).AddTicks(9222), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_user",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 18, 34, 837, DateTimeKind.Unspecified).AddTicks(9859), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
