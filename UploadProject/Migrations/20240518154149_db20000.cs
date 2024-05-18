using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadProject.Migrations
{
    /// <inheritdoc />
    public partial class db20000 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("53aa6ed9-bdf8-434e-ba29-daaee563f7e3"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("8cb23e6b-0b62-4ec8-b28d-f9e72913a137"), null, "moderator", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0a75efb0-269b-4422-8ee2-81414c194235", "AQAAAAIAAYagAAAAEPeWrHbH9lFSs1hfHc964buPOTYvakHK3xEPkwubENonf5yfje/0x72d2+XhBJnt5Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8cb23e6b-0b62-4ec8-b28d-f9e72913a137"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("53aa6ed9-bdf8-434e-ba29-daaee563f7e3"), null, "moderator", "MODERATOR" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "366a6907-aa73-45ec-99dd-7971ae7ab617", "AQAAAAIAAYagAAAAECpXzXKT9uuXwF3W4q7v9PZ/FcfyLN0riI5VocqwuJp4bh/RxuZ3NTyhRSDaRcUVSw==" });
        }
    }
}
