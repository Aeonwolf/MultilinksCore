using Microsoft.EntityFrameworkCore.Migrations;

namespace Multilinks.Core.Services.Migrations
{
    public partial class MinorFieldNameChange_ConnectionID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConnectionID",
                table: "HubConnections",
                newName: "ConnectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConnectionId",
                table: "HubConnections",
                newName: "ConnectionID");
        }
    }
}
