using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace N5Company.Migrations
{
    /// <inheritdoc />
    public partial class types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_PermissionTypes_PermissionTypesId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_PermissionTypesId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "PermissionTypesId",
                table: "Permissions");

            migrationBuilder.InsertData(
                table: "PermissionTypes",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_TipoPermiso",
                table: "Permissions",
                column: "TipoPermiso");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_PermissionTypes_TipoPermiso",
                table: "Permissions",
                column: "TipoPermiso",
                principalTable: "PermissionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_PermissionTypes_TipoPermiso",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_TipoPermiso",
                table: "Permissions");

            migrationBuilder.DeleteData(
                table: "PermissionTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PermissionTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "PermissionTypesId",
                table: "Permissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionTypesId",
                table: "Permissions",
                column: "PermissionTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_PermissionTypes_PermissionTypesId",
                table: "Permissions",
                column: "PermissionTypesId",
                principalTable: "PermissionTypes",
                principalColumn: "Id");
        }
    }
}
