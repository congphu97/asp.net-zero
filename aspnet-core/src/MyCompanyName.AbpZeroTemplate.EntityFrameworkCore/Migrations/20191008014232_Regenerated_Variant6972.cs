using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompanyName.AbpZeroTemplate.Migrations
{
    public partial class Regenerated_Variant6972 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpadPro",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Iphone8",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Iphone8Plus",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IphoneX",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Ipad",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Iphone",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "MacbookPro",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "IphoneXs",
                table: "Products",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "Macbook",
                table: "Categories",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ProductID = table.Column<string>(nullable: true),
                    Price = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "MacbookPro");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Products",
                newName: "IphoneXs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Macbook");

            migrationBuilder.AddColumn<string>(
                name: "IpadPro",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Iphone8",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Iphone8Plus",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IphoneX",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ipad",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Iphone",
                table: "Categories",
                nullable: true);
        }
    }
}
