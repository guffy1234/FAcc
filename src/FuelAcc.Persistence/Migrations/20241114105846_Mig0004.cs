using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelAcc.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig0004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreviousId",
                table: "ReplictionPackets",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousId",
                table: "ReplictionPackets");
        }
    }
}
