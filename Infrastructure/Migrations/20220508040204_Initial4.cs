using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColegioMozart.Infrastructure.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "framework");

            migrationBuilder.CreateTable(
                name: "entities",
                schema: "framework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "entity_fields",
                schema: "framework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    AllowUpdate = table.Column<bool>(type: "bit", nullable: false),
                    AllowInsert = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entity_fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entity_fields_entities_EntityId",
                        column: x => x.EntityId,
                        principalSchema: "framework",
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "views",
                schema: "framework",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AllowCreate = table.Column<bool>(type: "bit", nullable: false),
                    AllowDelete = table.Column<bool>(type: "bit", nullable: false),
                    AllowUpdate = table.Column<bool>(type: "bit", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_views_entities_EntityId",
                        column: x => x.EntityId,
                        principalSchema: "framework",
                        principalTable: "entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entity_fields_EntityId",
                schema: "framework",
                table: "entity_fields",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_views_EntityId",
                schema: "framework",
                table: "views",
                column: "EntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entity_fields",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "views",
                schema: "framework");

            migrationBuilder.DropTable(
                name: "entities",
                schema: "framework");
        }
    }
}
