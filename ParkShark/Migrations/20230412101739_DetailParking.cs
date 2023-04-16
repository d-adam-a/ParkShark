using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace ParkShark.Migrations
{
    /// <inheritdoc />
    public partial class DetailParking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetailParking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ParkingId = table.Column<int>(type: "int", nullable: false),
                    HourlyRate = table.Column<int>(type: "int", nullable: false),
                    ParkingFee = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailParking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailParking_Parking_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DetailParking_ParkingId",
                table: "DetailParking",
                column: "ParkingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailParking");
        }
    }
}
