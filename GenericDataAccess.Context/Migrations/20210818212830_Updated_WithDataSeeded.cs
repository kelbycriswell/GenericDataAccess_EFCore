using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GenericDataAccess.Context.Migrations
{
    public partial class Updated_WithDataSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Orders_ID",
                table: "LineItems");

            migrationBuilder.DropIndex(
                name: "IX_LineItems_ID",
                table: "LineItems");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "LineItems");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "ModifiedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2000, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 214, DateTimeKind.Unspecified).AddTicks(1746), new TimeSpan(0, -6, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "LineItems",
                keyColumns: new[] { "LineNo", "OrderID" },
                keyValues: new object[] { 1, 1 },
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 217, DateTimeKind.Unspecified).AddTicks(5483), new TimeSpan(0, -6, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "LineItems",
                keyColumns: new[] { "LineNo", "OrderID" },
                keyValues: new object[] { 2, 1 },
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 217, DateTimeKind.Unspecified).AddTicks(6025), new TimeSpan(0, -6, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 1,
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 217, DateTimeKind.Unspecified).AddTicks(2295), new TimeSpan(0, -6, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 216, DateTimeKind.Unspecified).AddTicks(8538), new TimeSpan(0, -6, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2,
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(2021, 8, 18, 15, 28, 29, 216, DateTimeKind.Unspecified).AddTicks(9922), new TimeSpan(0, -6, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Orders_OrderID",
                table: "LineItems",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Orders_OrderID",
                table: "LineItems");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "LineItems",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "DateOfBirth", "ModifiedOn" },
                values: new object[] { new DateTimeOffset(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -7, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "LineItems",
                keyColumns: new[] { "LineNo", "OrderID" },
                keyValues: new object[] { 1, 1 },
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "LineItems",
                keyColumns: new[] { "LineNo", "OrderID" },
                keyValues: new object[] { 2, 1 },
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "ID",
                keyValue: 1,
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1,
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2,
                column: "ModifiedOn",
                value: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_ID",
                table: "LineItems",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Orders_ID",
                table: "LineItems",
                column: "ID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
