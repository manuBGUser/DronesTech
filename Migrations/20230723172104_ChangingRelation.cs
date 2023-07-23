using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DronesTech.Migrations
{
    /// <inheritdoc />
    public partial class ChangingRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Drones_DroneId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_DroneId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "DroneId",
                table: "Medicines");

            migrationBuilder.CreateTable(
                name: "DroneMedicine",
                columns: table => new
                {
                    DronesId = table.Column<int>(type: "int", nullable: false),
                    MedicinesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneMedicine", x => new { x.DronesId, x.MedicinesId });
                    table.ForeignKey(
                        name: "FK_DroneMedicine_Drones_DronesId",
                        column: x => x.DronesId,
                        principalTable: "Drones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DroneMedicine_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DroneMedicine_MedicinesId",
                table: "DroneMedicine",
                column: "MedicinesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DroneMedicine");

            migrationBuilder.AddColumn<int>(
                name: "DroneId",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_DroneId",
                table: "Medicines",
                column: "DroneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Drones_DroneId",
                table: "Medicines",
                column: "DroneId",
                principalTable: "Drones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
