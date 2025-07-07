using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_State",
                table: "Employees",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Employees",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_State",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Employees");
        }
    }
}
