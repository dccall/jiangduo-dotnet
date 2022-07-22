using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiangDuo.Database.Migrations.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 277, DateTimeKind.Unspecified).AddTicks(9448), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 277, DateTimeKind.Unspecified).AddTicks(9453), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 277, DateTimeKind.Unspecified).AddTicks(9455), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 277, DateTimeKind.Unspecified).AddTicks(9456), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 277, DateTimeKind.Unspecified).AddTicks(9457), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 277, DateTimeKind.Unspecified).AddTicks(9459), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 277, DateTimeKind.Unspecified).AddTicks(9461), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 277, DateTimeKind.Unspecified).AddTicks(9462), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 281, DateTimeKind.Unspecified).AddTicks(5316), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_user",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 22, 8, 16, 40, 283, DateTimeKind.Unspecified).AddTicks(6801), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
