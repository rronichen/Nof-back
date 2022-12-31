using Microsoft.EntityFrameworkCore.Migrations;

namespace Nof.Migrations
{
    public partial class user10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetAllAppointment]
                AS
                BEGIN
                    select * from HaircutRecored
                END";

                    migrationBuilder.Sql(sp);
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
