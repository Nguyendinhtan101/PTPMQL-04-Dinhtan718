using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class httv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hethongphanphoi",
                table: "Hethongphanphoi");

            migrationBuilder.DropColumn(
                name: "TenHTPP",
                table: "Hethongphanphoi");

            migrationBuilder.RenameColumn(
                name: "MaHTPP",
                table: "Hethongphanphoi",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Hethongphanphoi",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hethongphanphoi",
                table: "Hethongphanphoi",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hethongphanphoi",
                table: "Hethongphanphoi");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Hethongphanphoi");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Hethongphanphoi",
                newName: "MaHTPP");

            migrationBuilder.AddColumn<string>(
                name: "TenHTPP",
                table: "Hethongphanphoi",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hethongphanphoi",
                table: "Hethongphanphoi",
                column: "MaHTPP");
        }
    }
}
