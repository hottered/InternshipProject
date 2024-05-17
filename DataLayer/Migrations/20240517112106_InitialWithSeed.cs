using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeUserRequest");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_EmployeeID",
                table: "Requests",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_EmployeeID",
                table: "Requests",
                column: "EmployeeID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_EmployeeID",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_EmployeeID",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "Requests");

            migrationBuilder.CreateTable(
                name: "EmployeeUserRequest",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeUserRequest", x => new { x.EmployeeId, x.RequestId });
                    table.ForeignKey(
                        name: "FK_EmployeeUserRequest_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeUserRequest_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUserRequest_RequestId",
                table: "EmployeeUserRequest",
                column: "RequestId");
        }
    }
}
