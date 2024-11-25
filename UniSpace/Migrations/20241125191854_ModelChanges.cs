using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSpace.Migrations
{
    /// <inheritdoc />
    public partial class ModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Specialties_SpecialtyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SpecialtyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudentId",
                table: "Subjects",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_AspNetUsers_StudentId",
                table: "Subjects",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_AspNetUsers_StudentId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_StudentId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SpecialtyId",
                table: "AspNetUsers",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Specialties_SpecialtyId",
                table: "AspNetUsers",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
