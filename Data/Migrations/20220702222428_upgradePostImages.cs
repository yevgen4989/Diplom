using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class upgradePostImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostPostImages");

            migrationBuilder.DropTable(
                name: "PostImages");

            migrationBuilder.DropColumn(
                name: "IdChatTelegram",
                table: "UserOptions");

            migrationBuilder.DropColumn(
                name: "TelegramAccountActive",
                table: "UserOptions");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "PostFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostPostFiles",
                columns: table => new
                {
                    PostFilesId = table.Column<int>(type: "int", nullable: false),
                    PostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPostFiles", x => new { x.PostFilesId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_PostPostFiles_PostFiles_PostFilesId",
                        column: x => x.PostFilesId,
                        principalTable: "PostFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostPostFiles_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostPostFiles_PostsId",
                table: "PostPostFiles",
                column: "PostsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostPostFiles");

            migrationBuilder.DropTable(
                name: "PostFiles");

            migrationBuilder.AddColumn<string>(
                name: "IdChatTelegram",
                table: "UserOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TelegramAccountActive",
                table: "UserOptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PostImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostPostImages",
                columns: table => new
                {
                    PostImagesId = table.Column<int>(type: "int", nullable: false),
                    PostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPostImages", x => new { x.PostImagesId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_PostPostImages_PostImages_PostImagesId",
                        column: x => x.PostImagesId,
                        principalTable: "PostImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostPostImages_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostPostImages_PostsId",
                table: "PostPostImages",
                column: "PostsId");
        }
    }
}
