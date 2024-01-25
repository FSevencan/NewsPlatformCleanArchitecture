using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class article_and_articleReaction_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "ArticleReactions");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "ArticleReactions");

            migrationBuilder.AddColumn<int>(
                name: "TotalDislikes",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalLikes",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "ArticleReactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 197, 144, 186, 181, 254, 99, 97, 99, 24, 130, 9, 113, 215, 119, 25, 220, 157, 11, 204, 58, 89, 129, 239, 221, 39, 204, 141, 5, 114, 17, 184, 90, 63, 215, 159, 200, 151, 46, 218, 70, 248, 17, 202, 36, 251, 159, 122, 18, 96, 211, 170, 67, 172, 33, 26, 210, 191, 15, 173, 130, 96, 245, 174, 94 }, new byte[] { 40, 179, 34, 5, 251, 139, 124, 216, 195, 200, 166, 120, 91, 5, 86, 162, 31, 70, 78, 171, 52, 140, 193, 90, 128, 215, 88, 181, 251, 55, 79, 239, 129, 53, 56, 134, 233, 112, 130, 195, 249, 122, 100, 148, 147, 75, 76, 136, 209, 9, 133, 162, 69, 207, 85, 15, 87, 180, 182, 149, 59, 140, 139, 36, 219, 204, 18, 174, 47, 169, 252, 66, 180, 101, 182, 135, 253, 155, 3, 0, 134, 217, 122, 76, 179, 108, 224, 182, 152, 212, 10, 158, 158, 95, 180, 113, 212, 97, 72, 114, 13, 160, 226, 27, 98, 151, 245, 110, 212, 56, 247, 151, 160, 222, 87, 114, 115, 142, 65, 188, 165, 88, 156, 1, 42, 195, 12, 57 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDislikes",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "TotalLikes",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "ArticleReactions");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "ArticleReactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "ArticleReactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 206, 46, 201, 172, 166, 203, 247, 110, 158, 160, 89, 165, 65, 144, 86, 224, 144, 193, 230, 109, 255, 198, 3, 106, 30, 15, 219, 81, 60, 68, 102, 127, 91, 47, 111, 76, 25, 177, 152, 161, 189, 131, 190, 20, 43, 11, 88, 61, 44, 133, 120, 69, 92, 249, 24, 223, 171, 231, 199, 40, 111, 178, 116, 38 }, new byte[] { 20, 60, 98, 32, 227, 137, 203, 162, 73, 180, 41, 177, 155, 46, 40, 247, 216, 69, 66, 103, 220, 5, 207, 131, 252, 6, 21, 74, 247, 196, 11, 13, 19, 91, 141, 107, 161, 132, 85, 88, 226, 45, 159, 241, 17, 231, 10, 6, 227, 44, 15, 255, 163, 57, 160, 103, 99, 120, 180, 4, 219, 59, 237, 164, 0, 121, 25, 35, 6, 73, 113, 213, 215, 214, 31, 144, 119, 205, 137, 247, 9, 122, 8, 22, 34, 9, 173, 5, 155, 255, 156, 248, 210, 141, 133, 11, 226, 137, 233, 138, 175, 30, 98, 68, 4, 189, 39, 216, 130, 112, 67, 115, 10, 111, 3, 207, 57, 124, 21, 70, 38, 102, 154, 222, 239, 110, 247, 0 } });
        }
    }
}
