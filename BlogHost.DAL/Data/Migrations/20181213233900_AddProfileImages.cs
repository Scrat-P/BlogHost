using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogHost.DAL.Data.Migrations
{
    public partial class AddProfileImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Posts");
        }
    }
}
