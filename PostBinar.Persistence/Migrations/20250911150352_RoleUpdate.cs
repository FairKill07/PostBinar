using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostBinar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RoleUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_membership_roles_project_memberships_ProjectMembers~",
                table: "project_membership_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_project_membership_roles_roles_RoleId",
                table: "project_membership_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_membership_roles",
                table: "project_membership_roles");

            migrationBuilder.RenameTable(
                name: "project_membership_roles",
                newName: "project_roles");

            migrationBuilder.RenameIndex(
                name: "IX_project_membership_roles_RoleId",
                table: "project_roles",
                newName: "IX_project_roles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_roles",
                table: "project_roles",
                columns: new[] { "ProjectMembershipId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_project_memberships_ProjectMembershipId",
                table: "project_roles",
                column: "ProjectMembershipId",
                principalTable: "project_memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_roles_roles_RoleId",
                table: "project_roles",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_project_memberships_ProjectMembershipId",
                table: "project_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_project_roles_roles_RoleId",
                table: "project_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_project_roles",
                table: "project_roles");

            migrationBuilder.RenameTable(
                name: "project_roles",
                newName: "project_membership_roles");

            migrationBuilder.RenameIndex(
                name: "IX_project_roles_RoleId",
                table: "project_membership_roles",
                newName: "IX_project_membership_roles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_project_membership_roles",
                table: "project_membership_roles",
                columns: new[] { "ProjectMembershipId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_project_membership_roles_project_memberships_ProjectMembers~",
                table: "project_membership_roles",
                column: "ProjectMembershipId",
                principalTable: "project_memberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_membership_roles_roles_RoleId",
                table: "project_membership_roles",
                column: "RoleId",
                principalTable: "roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
