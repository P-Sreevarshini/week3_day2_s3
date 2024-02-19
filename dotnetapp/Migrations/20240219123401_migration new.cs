using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class migrationnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Enquiries",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Enquiries_UserId",
                table: "Enquiries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enquiries_Users_UserId",
                table: "Enquiries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enquiries_Users_UserId",
                table: "Enquiries");

            migrationBuilder.DropIndex(
                name: "IX_Enquiries_UserId",
                table: "Enquiries");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Enquiries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
