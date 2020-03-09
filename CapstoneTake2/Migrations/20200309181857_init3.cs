using Microsoft.EntityFrameworkCore.Migrations;

namespace CapstoneTake2.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Requests_RequestsId",
                table: "RequestLine");

            migrationBuilder.DropIndex(
                name: "IX_RequestLine_RequestsId",
                table: "RequestLine");

            migrationBuilder.DropColumn(
                name: "RequestsId",
                table: "RequestLine");

            migrationBuilder.CreateIndex(
                name: "IX_RequestLine_RequestId",
                table: "RequestLine",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Requests_RequestId",
                table: "RequestLine",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestLine_Requests_RequestId",
                table: "RequestLine");

            migrationBuilder.DropIndex(
                name: "IX_RequestLine_RequestId",
                table: "RequestLine");

            migrationBuilder.AddColumn<int>(
                name: "RequestsId",
                table: "RequestLine",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestLine_RequestsId",
                table: "RequestLine",
                column: "RequestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestLine_Requests_RequestsId",
                table: "RequestLine",
                column: "RequestsId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
