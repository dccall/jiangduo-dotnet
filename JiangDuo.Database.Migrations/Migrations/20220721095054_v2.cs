using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiangDuo.Database.Migrations.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 555, DateTimeKind.Unspecified).AddTicks(8921), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 555, DateTimeKind.Unspecified).AddTicks(8925), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 555, DateTimeKind.Unspecified).AddTicks(8927), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 555, DateTimeKind.Unspecified).AddTicks(8928), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 555, DateTimeKind.Unspecified).AddTicks(8929), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 555, DateTimeKind.Unspecified).AddTicks(8931), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 555, DateTimeKind.Unspecified).AddTicks(8932), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 555, DateTimeKind.Unspecified).AddTicks(8933), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 559, DateTimeKind.Unspecified).AddTicks(2123), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_user",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 50, 53, 561, DateTimeKind.Unspecified).AddTicks(3314), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 784, DateTimeKind.Unspecified).AddTicks(2569), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 784, DateTimeKind.Unspecified).AddTicks(2574), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 784, DateTimeKind.Unspecified).AddTicks(2575), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 784, DateTimeKind.Unspecified).AddTicks(2576), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 784, DateTimeKind.Unspecified).AddTicks(2577), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 784, DateTimeKind.Unspecified).AddTicks(2580), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 784, DateTimeKind.Unspecified).AddTicks(2581), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_menu",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 784, DateTimeKind.Unspecified).AddTicks(2582), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 787, DateTimeKind.Unspecified).AddTicks(3434), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "sys_user",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTimeOffset(new DateTime(2022, 7, 21, 9, 47, 48, 789, DateTimeKind.Unspecified).AddTicks(4167), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
