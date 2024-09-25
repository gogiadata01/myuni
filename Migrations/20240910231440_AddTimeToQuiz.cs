using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyUni.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeToQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "MyQuiz",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "MyQuiz");
        }
    }
}
