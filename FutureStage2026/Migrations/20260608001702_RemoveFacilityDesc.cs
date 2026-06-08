using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureStage2026.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFacilityDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {




            migrationBuilder.DropColumn(
                name: "FacilityDesc",
                table: "SchoolFacilities");

        } 

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           


            migrationBuilder.AddColumn<string>(
                name: "FacilityDesc",
                table: "SchoolFacilities",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            
        }
    }
}
