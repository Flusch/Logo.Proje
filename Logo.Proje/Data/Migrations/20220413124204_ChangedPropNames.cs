using Microsoft.EntityFrameworkCore.Migrations;

namespace Logo.Proje.Data.Migrations
{
    public partial class ChangedPropNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "identityNo",
                schema: "Identity",
                table: "Users",
                newName: "IdentityNo");

            migrationBuilder.RenameColumn(
                name: "hasCar",
                schema: "Identity",
                table: "Users",
                newName: "HasCar");

            migrationBuilder.RenameColumn(
                name: "carPlate",
                schema: "Identity",
                table: "Users",
                newName: "CarPlate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdentityNo",
                schema: "Identity",
                table: "Users",
                newName: "identityNo");

            migrationBuilder.RenameColumn(
                name: "HasCar",
                schema: "Identity",
                table: "Users",
                newName: "hasCar");

            migrationBuilder.RenameColumn(
                name: "CarPlate",
                schema: "Identity",
                table: "Users",
                newName: "carPlate");
        }
    }
}
