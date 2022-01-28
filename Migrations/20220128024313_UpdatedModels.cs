using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediiProgramareEntity.Migrations
{
    public partial class UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Movies",
                table: "Studios",
                newName: "MoviesCreated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MoviesCreated",
                table: "Studios",
                newName: "Movies");
        }
    }
}
