using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySPSTApi.Migrations
{
    /// <inheritdoc />
    public partial class New_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "books");

            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_books_PublisherId",
                table: "books",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_publishers_PublisherId",
                table: "books",
                column: "PublisherId",
                principalTable: "publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_publishers_PublisherId",
                table: "books");

            migrationBuilder.DropIndex(
                name: "IX_books_PublisherId",
                table: "books");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "books");

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
