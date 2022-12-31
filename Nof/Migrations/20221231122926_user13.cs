using Microsoft.EntityFrameworkCore.Migrations;

namespace Nof.Migrations
{
    public partial class user13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[GetUserById] @userId int
                AS
                BEGIN
                    select * from Users where Id = @userId
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
