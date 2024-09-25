using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyUni.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyUniCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForPupil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScholarshipAndFunding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangePrograms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Labs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentsLife = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethods = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUniCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchevitiSavaldebuloSagani",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchevitiSavaldebuloSagani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchevitiSavaldebuloSagani_MyUniCard_UniCardId",
                        column: x => x.UniCardId,
                        principalTable: "MyUniCard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_MyUniCard_UniCardId",
                        column: x => x.UniCardId,
                        principalTable: "MyUniCard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section_MyUniCard_UniCardId",
                        column: x => x.UniCardId,
                        principalTable: "MyUniCard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Section2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniCardId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section2_MyUniCard_UniCardId",
                        column: x => x.UniCardId,
                        principalTable: "MyUniCard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArchevitiSavaldebuloSagnebi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SagnisSaxeli = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Koeficienti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimaluriZgvari = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioriteti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdgilebisRaodenoba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArchevitiSavaldebuloSaganiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchevitiSavaldebuloSagnebi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchevitiSavaldebuloSagnebi_ArchevitiSavaldebuloSagani_ArchevitiSavaldebuloSaganiId",
                        column: x => x.ArchevitiSavaldebuloSaganiId,
                        principalTable: "ArchevitiSavaldebuloSagani",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Programname",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Jobs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SwavlebisEna = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kvalifikacia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dafinanseba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KreditebisRaodenoba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdgilebisRaodenoba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fasi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kodi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramisAgwera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programname", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programname_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SavaldebuloSagnebi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SagnisSaxeli = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Koeficienti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimaluriZgvari = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prioriteti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdgilebisRaodenoba = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section2Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavaldebuloSagnebi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavaldebuloSagnebi_Section2_Section2Id",
                        column: x => x.Section2Id,
                        principalTable: "Section2",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchevitiSavaldebuloSagani_UniCardId",
                table: "ArchevitiSavaldebuloSagani",
                column: "UniCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchevitiSavaldebuloSagnebi_ArchevitiSavaldebuloSaganiId",
                table: "ArchevitiSavaldebuloSagnebi",
                column: "ArchevitiSavaldebuloSaganiId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_UniCardId",
                table: "Event",
                column: "UniCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Programname_SectionId",
                table: "Programname",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SavaldebuloSagnebi_Section2Id",
                table: "SavaldebuloSagnebi",
                column: "Section2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Section_UniCardId",
                table: "Section",
                column: "UniCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Section2_UniCardId",
                table: "Section2",
                column: "UniCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchevitiSavaldebuloSagnebi");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Programname");

            migrationBuilder.DropTable(
                name: "SavaldebuloSagnebi");

            migrationBuilder.DropTable(
                name: "ArchevitiSavaldebuloSagani");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Section2");

            migrationBuilder.DropTable(
                name: "MyUniCard");
        }
    }
}
