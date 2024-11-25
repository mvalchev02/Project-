using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSpace.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserInfoId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_AspNetUsers_StudentId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_StudentId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserInfoId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Student_FirstName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Student_LastName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Student_Phone",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Student_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Student_LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Student_Phone",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserInfoId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudentId",
                table: "Subjects",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserInfoId",
                table: "Reservations",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserInfoId",
                table: "Reservations",
                column: "UserInfoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_AspNetUsers_StudentId",
                table: "Subjects",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
