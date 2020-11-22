using Microsoft.EntityFrameworkCore.Migrations;

namespace BookPrioritizerApi.Migrations
{
    public partial class AddRelevanceReferenceLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Relevance",
                table: "TagBooks");

            migrationBuilder.AddColumn<int>(
                name: "RelevanceId",
                table: "TagBooks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReferenceLinks",
                columns: table => new
                {
                    ReferenceLinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceLinks", x => x.ReferenceLinkId);
                });

            migrationBuilder.CreateTable(
                name: "Relevances",
                columns: table => new
                {
                    RelevanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relevances", x => x.RelevanceId);
                });

            migrationBuilder.InsertData(
                table: "Relevances",
                columns: new[] { "RelevanceId", "Level", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Not Relevant" },
                    { 2, 1, "Little Relevant" },
                    { 3, 2, "Somewhat Relevant" },
                    { 4, 3, "Mostly Relevant" },
                    { 5, 4, "Completely Relevant" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagBooks_RelevanceId",
                table: "TagBooks",
                column: "RelevanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagBooks_Relevances_RelevanceId",
                table: "TagBooks",
                column: "RelevanceId",
                principalTable: "Relevances",
                principalColumn: "RelevanceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagBooks_Relevances_RelevanceId",
                table: "TagBooks");

            migrationBuilder.DropTable(
                name: "ReferenceLinks");

            migrationBuilder.DropTable(
                name: "Relevances");

            migrationBuilder.DropIndex(
                name: "IX_TagBooks_RelevanceId",
                table: "TagBooks");

            migrationBuilder.DropColumn(
                name: "RelevanceId",
                table: "TagBooks");

            migrationBuilder.AddColumn<int>(
                name: "Relevance",
                table: "TagBooks",
                type: "int",
                nullable: true);
        }
    }
}
