using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Marketer.Migrations
{
    public partial class changeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Person_PersonId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_PersonId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "ClassesId",
                table: "Person",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_ClassesId",
                table: "Person",
                column: "ClassesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Classes_ClassesId",
                table: "Person",
                column: "ClassesId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Classes_ClassesId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_ClassesId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ClassesId",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Classes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_PersonId",
                table: "Classes",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Person_PersonId",
                table: "Classes",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
