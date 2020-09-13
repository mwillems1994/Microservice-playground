using Microsoft.EntityFrameworkCore.Migrations;

namespace Playground.Nl.OrderManagementAPI.Nl.Migrations
{
    public partial class RenamedCustomerFirstname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customer",
                newName: "Firstname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Customer",
                newName: "FirstName");
        }
    }
}
