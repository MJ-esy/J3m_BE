using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace J3m_BE.Migrations
{
    /// <inheritdoc />
    public partial class updateRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "Recipes");
        }
    }
}
