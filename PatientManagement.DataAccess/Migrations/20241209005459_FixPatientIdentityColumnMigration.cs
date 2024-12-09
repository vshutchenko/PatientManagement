using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagement.DataAccess.Migrations
{
    public partial class FixPatientIdentityColumnMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(name: "Id",
                table: "Patients",
                type: "uniqueidentifier",
                defaultValueSql: "NewId()",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
