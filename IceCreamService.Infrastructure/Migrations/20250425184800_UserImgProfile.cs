using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IceCreamService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserImgProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserImgProfile",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserImgProfile",
                table: "Users");
        }
    }
}
