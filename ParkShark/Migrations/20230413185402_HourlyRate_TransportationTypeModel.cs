using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkShark.Migrations
{
    /// <inheritdoc />
    public partial class HourlyRate_TransportationTypeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HourlyRate",
                table: "TransportationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "TransportationTypes");
        }
    }
}
