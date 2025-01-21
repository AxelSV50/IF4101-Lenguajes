using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISandbox.Migrations
{
    /// <inheritdoc />
    /// Se añade la entidad (o entidades) -> Add-Migration InitialChanges -> Se elimina lo innecesario -> Update-Database, Comando incial para crear el modelo https://codeshare.io/1VKnLm -> Scaffold-DbContext "Name=DefaultConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    /// Si se modifica algo en las tablas desde ese SQL (atributos), hay que eliminar el modelo que ya tengo aquí. Desde aquí se usa lo anterior. 
    /// Para borrar el modelo basta con borrar la carpeta models y migrations
    public partial class InitialChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            //Sólo creo la nueva entidad course del modelo, no es necesario volver a crear lo que ya está
            migrationBuilder.CreateTable(
            name: "Course",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Course", x => x.Id);
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInformation");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Nationality");
        }
    }
}
