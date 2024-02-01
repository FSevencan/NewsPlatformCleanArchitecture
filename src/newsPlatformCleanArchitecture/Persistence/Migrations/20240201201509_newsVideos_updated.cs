using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newsVideos_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "NewsVideos");

            migrationBuilder.DropColumn(
                name: "ThumbnailImage",
                table: "NewsVideos");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 161, 14, 107, 254, 216, 255, 149, 67, 177, 0, 218, 20, 134, 118, 32, 130, 140, 64, 251, 191, 124, 10, 88, 177, 108, 242, 104, 118, 54, 52, 208, 36, 176, 237, 147, 249, 164, 90, 165, 7, 78, 105, 1, 81, 5, 79, 195, 214, 208, 23, 50, 93, 208, 47, 152, 187, 141, 149, 79, 98, 148, 138, 179, 64 }, new byte[] { 187, 74, 207, 170, 158, 130, 94, 187, 72, 31, 252, 182, 98, 38, 240, 239, 160, 147, 102, 19, 190, 226, 120, 20, 225, 45, 22, 55, 146, 235, 214, 133, 155, 250, 61, 68, 92, 56, 102, 42, 176, 211, 71, 223, 155, 125, 115, 247, 10, 224, 0, 41, 179, 9, 183, 10, 9, 234, 70, 160, 239, 107, 121, 146, 133, 134, 105, 111, 85, 166, 46, 219, 114, 172, 8, 147, 123, 143, 226, 98, 10, 94, 114, 53, 194, 0, 252, 224, 100, 192, 64, 25, 228, 30, 193, 161, 86, 7, 116, 111, 203, 148, 205, 195, 190, 75, 112, 219, 93, 231, 193, 201, 234, 113, 236, 233, 185, 108, 207, 117, 234, 154, 210, 199, 97, 42, 62, 146 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "NewsVideos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImage",
                table: "NewsVideos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 197, 144, 186, 181, 254, 99, 97, 99, 24, 130, 9, 113, 215, 119, 25, 220, 157, 11, 204, 58, 89, 129, 239, 221, 39, 204, 141, 5, 114, 17, 184, 90, 63, 215, 159, 200, 151, 46, 218, 70, 248, 17, 202, 36, 251, 159, 122, 18, 96, 211, 170, 67, 172, 33, 26, 210, 191, 15, 173, 130, 96, 245, 174, 94 }, new byte[] { 40, 179, 34, 5, 251, 139, 124, 216, 195, 200, 166, 120, 91, 5, 86, 162, 31, 70, 78, 171, 52, 140, 193, 90, 128, 215, 88, 181, 251, 55, 79, 239, 129, 53, 56, 134, 233, 112, 130, 195, 249, 122, 100, 148, 147, 75, 76, 136, 209, 9, 133, 162, 69, 207, 85, 15, 87, 180, 182, 149, 59, 140, 139, 36, 219, 204, 18, 174, 47, 169, 252, 66, 180, 101, 182, 135, 253, 155, 3, 0, 134, 217, 122, 76, 179, 108, 224, 182, 152, 212, 10, 158, 158, 95, 180, 113, 212, 97, 72, 114, 13, 160, 226, 27, 98, 151, 245, 110, 212, 56, 247, 151, 160, 222, 87, 114, 115, 142, 65, 188, 165, 88, 156, 1, 42, 195, 12, 57 } });
        }
    }
}
