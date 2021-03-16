using Microsoft.EntityFrameworkCore.Migrations;

namespace Robson_Totvs_Test.Migrations
{
    public partial class Update_Profiles_Database_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_profileobject_users_accountid",
                table: "profileobject");

            migrationBuilder.DropPrimaryKey(
                name: "pk_profileobject",
                table: "profileobject");

            migrationBuilder.RenameTable(
                name: "profileobject",
                newName: "tb_profiles");

            migrationBuilder.RenameIndex(
                name: "ix_profileobject_accountid",
                table: "tb_profiles",
                newName: "ix_tb_profiles_accountid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_tb_profiles",
                table: "tb_profiles",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_tb_profiles_aspnetusers_accountid",
                table: "tb_profiles",
                column: "accountid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tb_profiles_aspnetusers_accountid",
                table: "tb_profiles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_tb_profiles",
                table: "tb_profiles");

            migrationBuilder.RenameTable(
                name: "tb_profiles",
                newName: "profileobject");

            migrationBuilder.RenameIndex(
                name: "ix_tb_profiles_accountid",
                table: "profileobject",
                newName: "ix_profileobject_accountid");

            migrationBuilder.AddPrimaryKey(
                name: "pk_profileobject",
                table: "profileobject",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_profileobject_users_accountid",
                table: "profileobject",
                column: "accountid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
