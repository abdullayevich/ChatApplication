using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApplication.Service.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommentToAzure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupMembers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "GroupMembers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
