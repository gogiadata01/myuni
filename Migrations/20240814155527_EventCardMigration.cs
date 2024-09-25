using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyUni.Migrations
{
    /// <inheritdoc />
    public partial class EventCardMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyEventCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyEventCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventType_MyEventCard_EventCardId",
                        column: x => x.EventCardId,
                        principalTable: "MyEventCard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventType_EventCardId",
                table: "EventType",
                column: "EventCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "MyEventCard");
        }
    }
}
