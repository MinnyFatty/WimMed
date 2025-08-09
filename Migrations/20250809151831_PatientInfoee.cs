using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WimMed.Migrations
{
    /// <inheritdoc />
    public partial class PatientInfoee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientInfos_Patients_PatientId1",
                table: "PatientInfos");

            migrationBuilder.DropIndex(
                name: "IX_PatientInfos_PatientId1",
                table: "PatientInfos");

            migrationBuilder.DropColumn(
                name: "PatientInfoId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "PatientInfos");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientInfos_Patients_PatientId",
                table: "PatientInfos");

            migrationBuilder.DropIndex(
                name: "IX_PatientInfos_PatientId",
                table: "PatientInfos");

            migrationBuilder.AddColumn<int>(
                name: "PatientInfoId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId1",
                table: "PatientInfos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientInfos_PatientId1",
                table: "PatientInfos",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientInfos_Patients_PatientId1",
                table: "PatientInfos",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
