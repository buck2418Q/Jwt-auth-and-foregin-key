using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STSPro.Migrations
{
    /// <inheritdoc />
    public partial class load1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "simCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "simCards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
