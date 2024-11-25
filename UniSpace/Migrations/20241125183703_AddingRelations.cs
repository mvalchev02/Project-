using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniSpace.Migrations
{
    /// <inheritdoc />
    public partial class AddingRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_AspNetUsers_ProffesseurId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_ProffesseurId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "ProffesseurId",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "ProfessorSubjects",
                columns: table => new
                {
                    ProffesseurId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaughtSubjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorSubjects", x => new { x.ProffesseurId, x.TaughtSubjectsId });
                    table.ForeignKey(
                        name: "FK_ProfessorSubjects_AspNetUsers_ProffesseurId",
                        column: x => x.ProffesseurId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorSubjects_Subjects_TaughtSubjectsId",
                        column: x => x.TaughtSubjectsId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorSubjects_TaughtSubjectsId",
                table: "ProfessorSubjects",
                column: "TaughtSubjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessorSubjects");

            migrationBuilder.AddColumn<string>(
                name: "ProffesseurId",
                table: "Subjects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_ProffesseurId",
                table: "Subjects",
                column: "ProffesseurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_AspNetUsers_ProffesseurId",
                table: "Subjects",
                column: "ProffesseurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
