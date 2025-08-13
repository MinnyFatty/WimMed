using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WimMed.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
