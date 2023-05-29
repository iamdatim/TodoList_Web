using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoWeb.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoList_Users_UserId",
                table: "ToDoList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoList",
                table: "ToDoList");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "ToDoList");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ToDoList",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoList_UserId",
                table: "ToDoList",
                newName: "IX_ToDoList_UserID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "ToDoList",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoList",
                table: "ToDoList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoList_Users_UserID",
                table: "ToDoList",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoList_Users_UserID",
                table: "ToDoList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoList",
                table: "ToDoList");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "ToDoList",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoList_UserID",
                table: "ToDoList",
                newName: "IX_ToDoList_UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ToDoList",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "ToDoList",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoList",
                table: "ToDoList",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoList_Users_UserId",
                table: "ToDoList",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
