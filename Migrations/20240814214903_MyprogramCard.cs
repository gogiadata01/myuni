using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyUni.Migrations
{
    /// <inheritdoc />
    public partial class MyprogramCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyprogramCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyprogramCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_MyprogramCard_ProgramCardId",
                        column: x => x.ProgramCardId,
                        principalTable: "MyprogramCard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProgramNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    programname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramNames_Field_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Field",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CheckBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChackBoxName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramNamesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckBoxes_ProgramNames_ProgramNamesId",
                        column: x => x.ProgramNamesId,
                        principalTable: "ProgramNames",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckBoxes_ProgramNamesId",
                table: "CheckBoxes",
                column: "ProgramNamesId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_ProgramCardId",
                table: "Field",
                column: "ProgramCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramNames_FieldId",
                table: "ProgramNames",
                column: "FieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckBoxes");

            migrationBuilder.DropTable(
                name: "ProgramNames");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "MyprogramCard");
        }
    }
}
