using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheFitnessApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SmallChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Sessions_SessionID",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_AspNetUsers_UserID",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AspNetUsers_UserID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Schedules_ScheduleID",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_AspNetUsers_UserID",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserID",
                table: "UserProfiles");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Sessions_SessionID",
                table: "Exercises",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_AspNetUsers_UserID",
                table: "Goals",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_AspNetUsers_UserID",
                table: "Schedules",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Schedules_ScheduleID",
                table: "Sessions",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ScheduleID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_AspNetUsers_UserID",
                table: "Statistics",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserID",
                table: "UserProfiles",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Sessions_SessionID",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_AspNetUsers_UserID",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_AspNetUsers_UserID",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Schedules_ScheduleID",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Statistics_AspNetUsers_UserID",
                table: "Statistics");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserID",
                table: "UserProfiles");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Sessions_SessionID",
                table: "Exercises",
                column: "SessionID",
                principalTable: "Sessions",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_AspNetUsers_UserID",
                table: "Goals",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_AspNetUsers_UserID",
                table: "Schedules",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Schedules_ScheduleID",
                table: "Sessions",
                column: "ScheduleID",
                principalTable: "Schedules",
                principalColumn: "ScheduleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statistics_AspNetUsers_UserID",
                table: "Statistics",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AspNetUsers_UserID",
                table: "UserProfiles",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
