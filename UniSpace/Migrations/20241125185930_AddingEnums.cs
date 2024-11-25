using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSpace.Migrations
{
    /// <inheritdoc />
    public partial class AddingEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Courses",
                table: "Specialties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Courses",
                table: "Specialties");
        }
    }
}
