using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddCiftciRoleAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ciftci",
                columns: new[] { "CiftciID", "Ad", "Adres", "Email", "KayitTarihi", "Soyad", "TCKimlikNo", "Telefon" },
                values: new object[] { 1, "Sistem", null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Çiftçisi", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ciftci",
                keyColumn: "CiftciID",
                keyValue: 1);
        }
    }
}
