using Microsoft.EntityFrameworkCore.Migrations;

namespace Nof.Migrations
{
    public partial class user12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetAppointmentByUserUd] @userId int
                AS
                BEGIN
                    select * from HaircutRecored where UserId = @userId
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
