using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crd.DataAccess.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallRecords",
                columns: table => new
                {
                    Reference = table.Column<string>(type: "TEXT", maxLength: 33, nullable: false),
                    CallerId = table.Column<long>(type: "INTEGER", nullable: false),
                    RecipientId = table.Column<long>(type: "INTEGER", nullable: false),
                    CallDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    DurationSeconds = table.Column<uint>(type: "INTEGER", nullable: false),
                    CostPence = table.Column<decimal>(type: "TEXT", precision: 11, scale: 3, nullable: false),
                    CurrencyIsoCode = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallRecords", x => x.Reference);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallRecords");
        }
    }
}
