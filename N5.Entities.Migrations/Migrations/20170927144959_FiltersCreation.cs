using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace N5.Entities.Migrations.Migrations
{
    public partial class FiltersCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CustomerFieldId = table.Column<long>(type: "int8", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filters_customerfield_CustomerFieldId",
                        column: x => x.CustomerFieldId,
                        principalTable: "customerfield",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RangeDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DefinedRangeFilterId = table.Column<int>(type: "int4", nullable: false),
                    Max = table.Column<double>(type: "float8", nullable: true),
                    Min = table.Column<double>(type: "float8", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RangeDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RangeDefinitions_Filters_DefinedRangeFilterId",
                        column: x => x.DefinedRangeFilterId,
                        principalTable: "Filters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filters_CustomerFieldId",
                table: "Filters",
                column: "CustomerFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_RangeDefinitions_DefinedRangeFilterId",
                table: "RangeDefinitions",
                column: "DefinedRangeFilterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RangeDefinitions");

            migrationBuilder.DropTable(
                name: "Filters");
        }
    }
}
