using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace N5.Entities.Migrations.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "source",
                columns: table => new
                {
                    id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_source", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customerfield",
                columns: table => new
                {
                    id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    customerId = table.Column<long>(type: "int8", nullable: false),
                    fieldname = table.Column<string>(type: "text", nullable: true),
                    fieldtype = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerfield", x => x.id);
                    table.ForeignKey(
                        name: "FK_customerfield_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeField",
                columns: table => new
                {
                    Id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Fieldname = table.Column<string>(type: "text", nullable: true),
                    Fieldtype = table.Column<string>(type: "text", nullable: true),
                    ProductTypeId = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypeField_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sourcefield",
                columns: table => new
                {
                    id = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    fieldname = table.Column<string>(type: "text", nullable: true),
                    fieldtype = table.Column<string>(type: "text", nullable: true),
                    sourceId = table.Column<long>(type: "int8", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sourcefield", x => x.id);
                    table.ForeignKey(
                        name: "FK_sourcefield_source_sourceId",
                        column: x => x.sourceId,
                        principalTable: "source",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customerfieldsourcemap",
                columns: table => new
                {
                    CustomerFieldID = table.Column<long>(type: "int8", nullable: false),
                    SourceFieldID = table.Column<long>(type: "int8", nullable: false),
                    Order = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerfieldsourcemap", x => new { x.CustomerFieldID, x.SourceFieldID });
                    table.ForeignKey(
                        name: "FK_customerfieldsourcemap_customerfield_CustomerFieldID",
                        column: x => x.CustomerFieldID,
                        principalTable: "customerfield",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_customerfieldsourcemap_sourcefield_SourceFieldID",
                        column: x => x.SourceFieldID,
                        principalTable: "sourcefield",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeFieldSourceMap",
                columns: table => new
                {
                    ProductTypeFieldID = table.Column<long>(type: "int8", nullable: false),
                    SourceFieldID = table.Column<long>(type: "int8", nullable: false),
                    Order = table.Column<int>(type: "int4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeFieldSourceMap", x => new { x.ProductTypeFieldID, x.SourceFieldID });
                    table.ForeignKey(
                        name: "FK_ProductTypeFieldSourceMap_ProductTypeField_ProductTypeFieldID",
                        column: x => x.ProductTypeFieldID,
                        principalTable: "ProductTypeField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTypeFieldSourceMap_sourcefield_SourceFieldID",
                        column: x => x.SourceFieldID,
                        principalTable: "sourcefield",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customerfield_customerId",
                table: "customerfield",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_customerfieldsourcemap_SourceFieldID",
                table: "customerfieldsourcemap",
                column: "SourceFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeField_ProductTypeId",
                table: "ProductTypeField",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeFieldSourceMap_SourceFieldID",
                table: "ProductTypeFieldSourceMap",
                column: "SourceFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_sourcefield_sourceId",
                table: "sourcefield",
                column: "sourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerfieldsourcemap");

            migrationBuilder.DropTable(
                name: "ProductTypeFieldSourceMap");

            migrationBuilder.DropTable(
                name: "customerfield");

            migrationBuilder.DropTable(
                name: "ProductTypeField");

            migrationBuilder.DropTable(
                name: "sourcefield");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "source");
        }
    }
}
