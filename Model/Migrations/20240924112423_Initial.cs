using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Docenten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Familienaam = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Wedde = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Docenten", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opleidingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AantalDagen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opleidingen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocentOpleidingen",
                columns: table => new
                {
                    DocentId = table.Column<int>(type: "int", nullable: false),
                    OpleidingId = table.Column<int>(type: "int", nullable: false),
                    Expertise = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocentOpleidingen", x => new { x.OpleidingId, x.DocentId });
                    table.ForeignKey(
                        name: "FK_DocentOpleidingen_Docenten_DocentId",
                        column: x => x.DocentId,
                        principalTable: "Docenten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocentOpleidingen_Opleidingen_OpleidingId",
                        column: x => x.OpleidingId,
                        principalTable: "Opleidingen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Docenten",
                columns: new[] { "Id", "Familienaam", "Voornaam", "Wedde" },
                values: new object[,]
                {
                    { 1, "Michelle", "Frederik", 5300m },
                    { 2, "Varaco", "Anatov", 6200m },
                    { 3, "Von Pieterson", "Pieter", 7900m },
                    { 4, "De Slaeger", "Sarah", 3900m },
                    { 5, "Vandenheuvel", "Karel", 6600m },
                    { 6, "Desmet", "Irma", 3850m },
                    { 7, "Copain", "Bart", 4200m },
                    { 8, "La Montana", "Valentino", 7300m }
                });

            migrationBuilder.InsertData(
                table: "Opleidingen",
                columns: new[] { "Id", "AantalDagen", "Naam" },
                values: new object[,]
                {
                    { 1, 90, "C# Development" },
                    { 2, 60, "Professioneel Koken" },
                    { 3, 90, "Java Development" },
                    { 4, 30, "Geavanceerde Algebra" },
                    { 5, 15, "Italiaans voor beginners" },
                    { 6, 30, "Cybersecurity in webapplicaties" },
                    { 7, 10, "Communicatie in professionele setting" },
                    { 8, 40, "Frans voor gevorderden" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocentOpleidingen_DocentId",
                table: "DocentOpleidingen",
                column: "DocentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocentOpleidingen");

            migrationBuilder.DropTable(
                name: "Docenten");

            migrationBuilder.DropTable(
                name: "Opleidingen");
        }
    }
}
