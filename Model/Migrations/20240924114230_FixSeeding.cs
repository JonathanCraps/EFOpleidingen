using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class FixSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DocentOpleidingen",
                columns: new[] { "DocentId", "OpleidingId", "Expertise" },
                values: new object[,]
                {
                    { 1, 1, 7 },
                    { 3, 2, 9 },
                    { 4, 2, 7 },
                    { 5, 2, 5 },
                    { 1, 3, 6 },
                    { 8, 3, 7 },
                    { 7, 4, 9 },
                    { 8, 4, 6 },
                    { 2, 5, 9 },
                    { 1, 6, 4 },
                    { 5, 7, 4 },
                    { 6, 7, 7 },
                    { 1, 8, 4 },
                    { 2, 8, 7 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 6, 7 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "DocentOpleidingen",
                keyColumns: new[] { "DocentId", "OpleidingId" },
                keyValues: new object[] { 2, 8 });
        }
    }
}
