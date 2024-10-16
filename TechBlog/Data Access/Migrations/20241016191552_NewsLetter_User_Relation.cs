using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access.Migrations
{
    /// <inheritdoc />
    public partial class NewsLetter_User_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authors",
                table: "NewsLetterUsers");

            migrationBuilder.CreateTable(
                            name: "NewsLetterUser",
                            columns: table => new
                            {
                                AuthorsId = table.Column<int>(type: "int", nullable: false),
                                NewsLettersEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                            },
                            constraints: table =>
                            {
                                table.PrimaryKey("PK_NewsLetterUser", x => new { x.AuthorsId, x.NewsLettersEmail });
                                table.ForeignKey(
                                    name: "FK_NewsLetterUser_NewsLetterUsers_NewsLettersEmail",
                                    column: x => x.NewsLettersEmail,
                                    principalTable: "NewsLetterUsers",
                                    principalColumn: "Email",
                                    onDelete: ReferentialAction.Cascade);
                                table.ForeignKey(
                                    name: "FK_NewsLetterUser_Users_AuthorsId",
                                    column: x => x.AuthorsId,
                                    principalTable: "Users",
                                    principalColumn: "Id",
                                    onDelete: ReferentialAction.Cascade);
                            });

            migrationBuilder.CreateIndex(
                name: "IX_NewsLetterUser_NewsLettersEmail",
                table: "NewsLetterUser",
                column: "NewsLettersEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsLetterUser");

            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "NewsLetterUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
