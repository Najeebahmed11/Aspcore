using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Migrations
{
    public partial class VehiclelistAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VehiclesId",
                table: "Vehicles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehiclesId",
                table: "Vehicles",
                column: "VehiclesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Vehicles_VehiclesId",
                table: "Vehicles",
                column: "VehiclesId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Vehicles_VehiclesId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehiclesId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehiclesId",
                table: "Vehicles");
        }
    }
}
