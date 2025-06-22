using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA.ValueObjectsUsingComplexTypes.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Customers",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                table: "Customers",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "Customers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Customers",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Customers",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Customers",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Customers",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Customers",
                newName: "Address_City");
        }
    }
}
