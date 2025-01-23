using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISandbox.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnInCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>("Description", "Course", "nvarchar(100)",
unicode: false, maxLength: 100, nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
