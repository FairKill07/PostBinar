using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostBinar.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigsCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Specializations_SpecializationId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskCategories",
                table: "TaskCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteCategories",
                table: "NoteCategories");

            migrationBuilder.RenameTable(
                name: "Specializations",
                newName: "specializations");

            migrationBuilder.RenameTable(
                name: "TaskCategories",
                newName: "task_categories");

            migrationBuilder.RenameTable(
                name: "NoteCategories",
                newName: "note_categories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "specializations",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "specializations",
                type: "character varying(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "task_categories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "task_categories",
                type: "character varying(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "note_categories",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "note_categories",
                type: "character varying(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_specializations",
                table: "specializations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_task_categories",
                table: "task_categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_note_categories",
                table: "note_categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_specializations_SpecializationId",
                table: "users",
                column: "SpecializationId",
                principalTable: "specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_specializations_SpecializationId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_specializations",
                table: "specializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_task_categories",
                table: "task_categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_note_categories",
                table: "note_categories");

            migrationBuilder.RenameTable(
                name: "specializations",
                newName: "Specializations");

            migrationBuilder.RenameTable(
                name: "task_categories",
                newName: "TaskCategories");

            migrationBuilder.RenameTable(
                name: "note_categories",
                newName: "NoteCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Specializations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "Specializations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TaskCategories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "TaskCategories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NoteCategories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ColorCode",
                table: "NoteCategories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Specializations",
                table: "Specializations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskCategories",
                table: "TaskCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteCategories",
                table: "NoteCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Specializations_SpecializationId",
                table: "users",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
