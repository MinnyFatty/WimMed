using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WimMed.Migrations
{
    /// <inheritdoc />
    public partial class IDlimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientInfos_Patients_PatientId",
                table: "PatientInfos");

            migrationBuilder.DropIndex(
                name: "IX_PatientInfos_PatientId",
                table: "PatientInfos");

            migrationBuilder.AlterColumn<string>(
                name: "IdNumber",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdNumber",
                table: "Patients",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_PatientInfos_PatientId",
                table: "PatientInfos",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientInfos_Patients_PatientId",
                table: "PatientInfos",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
