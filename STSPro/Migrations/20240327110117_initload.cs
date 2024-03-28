using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STSPro.Migrations
{
    /// <inheritdoc />
    public partial class initload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SimIDNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "simCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserModelId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    ProviderModelId = table.Column<int>(type: "int", nullable: false),
                    IsActiveUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    DevicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_simCards_devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_simCards_providers_ProviderModelId",
                        column: x => x.ProviderModelId,
                        principalTable: "providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_simCards_userModels_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "userModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_simCards_DevicesId",
                table: "simCards",
                column: "DevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_simCards_ProviderModelId",
                table: "simCards",
                column: "ProviderModelId");

            migrationBuilder.CreateIndex(
                name: "IX_simCards_UserModelId",
                table: "simCards",
                column: "UserModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "simCards");

            migrationBuilder.DropTable(
                name: "devices");

            migrationBuilder.DropTable(
                name: "providers");

            migrationBuilder.DropTable(
                name: "userModels");
        }
    }
}
