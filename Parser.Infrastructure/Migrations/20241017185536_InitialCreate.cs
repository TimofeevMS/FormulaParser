using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parser.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSheets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DataSheetTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSheets_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Formula = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    DataSheetTemplateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateAttributes_Templates_DataSheetTemplateId",
                        column: x => x.DataSheetTemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataSheetValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataSheetId = table.Column<Guid>(type: "uuid", nullable: false),
                    TemplateAttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StringValue = table.Column<string>(type: "text", nullable: true),
                    NumericValue = table.Column<double>(type: "double precision", nullable: true),
                    BooleanValue = table.Column<bool>(type: "boolean", nullable: true),
                    FormulaValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSheetValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSheetValues_DataSheets_DataSheetId",
                        column: x => x.DataSheetId,
                        principalTable: "DataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSheetValues_TemplateAttributes_TemplateAttributeId",
                        column: x => x.TemplateAttributeId,
                        principalTable: "TemplateAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSheets_TemplateId",
                table: "DataSheets",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSheetValues_DataSheetId",
                table: "DataSheetValues",
                column: "DataSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSheetValues_TemplateAttributeId",
                table: "DataSheetValues",
                column: "TemplateAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateAttributes_DataSheetTemplateId",
                table: "TemplateAttributes",
                column: "DataSheetTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSheetValues");

            migrationBuilder.DropTable(
                name: "DataSheets");

            migrationBuilder.DropTable(
                name: "TemplateAttributes");

            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}
