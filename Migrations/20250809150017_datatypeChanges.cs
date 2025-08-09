using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WimMed.Migrations
{
    /// <inheritdoc />
    public partial class datatypeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdNumber",
                table: "Patients",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 13);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "IdNumber",
                table: "Patients",
                type: "bigint",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);
        }
    }
}
