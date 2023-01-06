using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlecEduapi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitIdentityServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "936fc8fb-4492-4b36-9e01-e2472cd10939");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a1832865-7ab1-447e-9e33-2bd2b17f4928");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "9681654e-ede3-4970-aafe-b8706692c596", new DateTime(2023, 1, 6, 9, 47, 48, 563, DateTimeKind.Utc).AddTicks(9420), "AQAAAAEAACcQAAAAEKlWvIQy2rR2TbrSC/rxXUvc/HGaB/1LEHNV2iyU/wTBrgAiay24RCn8AfMQcum0jw==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "0322f5c0-f252-44ba-b243-053d61d5bcb8", new DateTime(2023, 1, 6, 9, 47, 48, 564, DateTimeKind.Utc).AddTicks(1430), "AQAAAAEAACcQAAAAEN1zmfDn6tl693gZVcdmlwBCWGuRkYa0lJp9YjynzLXf4LgYk2ErlfMdsKtBOlTN1Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "31352285-0ae0-44d1-8362-723109f8dbc7");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "51748c76-05a4-4dad-b8de-26780c54db59");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "3c19cf43-31d5-40b6-8ab8-1add1d946994", new DateTime(2022, 12, 7, 12, 49, 19, 60, DateTimeKind.Utc).AddTicks(9050), "AQAAAAEAACcQAAAAEHIokiouRnK+o3++FmVtSlBa3U5ZFcI516cqWMQX0etLrEGQRJkyJwn0Sgl7W/uW1A==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "2016710c-2fda-4968-bc4f-1910f9dbb311", new DateTime(2022, 12, 7, 12, 49, 19, 61, DateTimeKind.Utc).AddTicks(330), "AQAAAAEAACcQAAAAECBlyat5+H0SslKbuG+/RXShQOB/XF727fdmINc2i9NUIuypwtXB6wxNqTSIMMSsnw==" });
        }
    }
}
