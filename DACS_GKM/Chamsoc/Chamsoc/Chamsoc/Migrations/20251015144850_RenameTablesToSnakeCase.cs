using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chamsoc.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesToSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caregivers_AspNetUsers_UserId",
                table: "Caregivers");

            migrationBuilder.DropForeignKey(
                name: "FK_CareJobs_Caregivers_CaregiverId",
                table: "CareJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_CareJobs_Seniors_SeniorId",
                table: "CareJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_CareJobs_Services_ServiceId",
                table: "CareJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_CareJobs_JobId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Caregivers_CaregiverId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Seniors_SeniorId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CareJobs_CareJobId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_CareJobs_JobId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_CareJobs_JobId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Caregivers_CaregiverId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Seniors_SeniorId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_CareJobs_JobId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Caregivers_CaregiverId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Seniors_SeniorId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Seniors_AspNetUsers_UserId",
                table: "Seniors");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CareJobs_CareJobId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Caregivers_CaregiverId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Seniors_SeniorId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seniors",
                table: "Seniors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaints",
                table: "Complaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Caregivers",
                table: "Caregivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CareJobs",
                table: "CareJobs");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "transactions");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "services");

            migrationBuilder.RenameTable(
                name: "Seniors",
                newName: "seniors");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "ratings");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "payments");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "notifications");

            migrationBuilder.RenameTable(
                name: "Complaints",
                newName: "complaints");

            migrationBuilder.RenameTable(
                name: "Caregivers",
                newName: "caregivers");

            migrationBuilder.RenameTable(
                name: "CareJobs",
                newName: "care_jobs");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SeniorId",
                table: "transactions",
                newName: "IX_transactions_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CareJobId",
                table: "transactions",
                newName: "IX_transactions_CareJobId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CaregiverId",
                table: "transactions",
                newName: "IX_transactions_CaregiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Seniors_UserId",
                table: "seniors",
                newName: "IX_seniors_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_SeniorId",
                table: "ratings",
                newName: "IX_ratings_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_JobId",
                table: "ratings",
                newName: "IX_ratings_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_CaregiverId",
                table: "ratings",
                newName: "IX_ratings_CaregiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_SeniorId",
                table: "payments",
                newName: "IX_payments_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_JobId",
                table: "payments",
                newName: "IX_payments_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_CaregiverId",
                table: "payments",
                newName: "IX_payments_CaregiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "notifications",
                newName: "IX_notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_JobId",
                table: "notifications",
                newName: "IX_notifications_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_CareJobId",
                table: "notifications",
                newName: "IX_notifications_CareJobId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_SeniorId",
                table: "complaints",
                newName: "IX_complaints_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_JobId",
                table: "complaints",
                newName: "IX_complaints_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_CaregiverId",
                table: "complaints",
                newName: "IX_complaints_CaregiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Caregivers_UserId",
                table: "caregivers",
                newName: "IX_caregivers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CareJobs_ServiceId",
                table: "care_jobs",
                newName: "IX_care_jobs_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_CareJobs_SeniorId",
                table: "care_jobs",
                newName: "IX_care_jobs_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_CareJobs_CaregiverId",
                table: "care_jobs",
                newName: "IX_care_jobs_CaregiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_transactions",
                table: "transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_services",
                table: "services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seniors",
                table: "seniors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ratings",
                table: "ratings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payments",
                table: "payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_notifications",
                table: "notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_complaints",
                table: "complaints",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_caregivers",
                table: "caregivers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_care_jobs",
                table: "care_jobs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_care_jobs_caregivers_CaregiverId",
                table: "care_jobs",
                column: "CaregiverId",
                principalTable: "caregivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_care_jobs_seniors_SeniorId",
                table: "care_jobs",
                column: "SeniorId",
                principalTable: "seniors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_care_jobs_services_ServiceId",
                table: "care_jobs",
                column: "ServiceId",
                principalTable: "services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_caregivers_AspNetUsers_UserId",
                table: "caregivers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_complaints_care_jobs_JobId",
                table: "complaints",
                column: "JobId",
                principalTable: "care_jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_complaints_caregivers_CaregiverId",
                table: "complaints",
                column: "CaregiverId",
                principalTable: "caregivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_complaints_seniors_SeniorId",
                table: "complaints",
                column: "SeniorId",
                principalTable: "seniors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_AspNetUsers_UserId",
                table: "notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_care_jobs_CareJobId",
                table: "notifications",
                column: "CareJobId",
                principalTable: "care_jobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_notifications_care_jobs_JobId",
                table: "notifications",
                column: "JobId",
                principalTable: "care_jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_care_jobs_JobId",
                table: "payments",
                column: "JobId",
                principalTable: "care_jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_caregivers_CaregiverId",
                table: "payments",
                column: "CaregiverId",
                principalTable: "caregivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_seniors_SeniorId",
                table: "payments",
                column: "SeniorId",
                principalTable: "seniors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_care_jobs_JobId",
                table: "ratings",
                column: "JobId",
                principalTable: "care_jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_caregivers_CaregiverId",
                table: "ratings",
                column: "CaregiverId",
                principalTable: "caregivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_seniors_SeniorId",
                table: "ratings",
                column: "SeniorId",
                principalTable: "seniors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_seniors_AspNetUsers_UserId",
                table: "seniors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_care_jobs_CareJobId",
                table: "transactions",
                column: "CareJobId",
                principalTable: "care_jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_caregivers_CaregiverId",
                table: "transactions",
                column: "CaregiverId",
                principalTable: "caregivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_seniors_SeniorId",
                table: "transactions",
                column: "SeniorId",
                principalTable: "seniors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_care_jobs_caregivers_CaregiverId",
                table: "care_jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_care_jobs_seniors_SeniorId",
                table: "care_jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_care_jobs_services_ServiceId",
                table: "care_jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_caregivers_AspNetUsers_UserId",
                table: "caregivers");

            migrationBuilder.DropForeignKey(
                name: "FK_complaints_care_jobs_JobId",
                table: "complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_complaints_caregivers_CaregiverId",
                table: "complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_complaints_seniors_SeniorId",
                table: "complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_AspNetUsers_UserId",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_care_jobs_CareJobId",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_notifications_care_jobs_JobId",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_care_jobs_JobId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_caregivers_CaregiverId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_seniors_SeniorId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_care_jobs_JobId",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_caregivers_CaregiverId",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_seniors_SeniorId",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_seniors_AspNetUsers_UserId",
                table: "seniors");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_care_jobs_CareJobId",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_caregivers_CaregiverId",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_seniors_SeniorId",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_transactions",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_services",
                table: "services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seniors",
                table: "seniors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ratings",
                table: "ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payments",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_notifications",
                table: "notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_complaints",
                table: "complaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_caregivers",
                table: "caregivers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_care_jobs",
                table: "care_jobs");

            migrationBuilder.RenameTable(
                name: "transactions",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "services",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "seniors",
                newName: "Seniors");

            migrationBuilder.RenameTable(
                name: "ratings",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "payments",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "notifications",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "complaints",
                newName: "Complaints");

            migrationBuilder.RenameTable(
                name: "caregivers",
                newName: "Caregivers");

            migrationBuilder.RenameTable(
                name: "care_jobs",
                newName: "CareJobs");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_SeniorId",
                table: "Transactions",
                newName: "IX_Transactions_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_CareJobId",
                table: "Transactions",
                newName: "IX_Transactions_CareJobId");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_CaregiverId",
                table: "Transactions",
                newName: "IX_Transactions_CaregiverId");

            migrationBuilder.RenameIndex(
                name: "IX_seniors_UserId",
                table: "Seniors",
                newName: "IX_Seniors_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ratings_SeniorId",
                table: "Ratings",
                newName: "IX_Ratings_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_ratings_JobId",
                table: "Ratings",
                newName: "IX_Ratings_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_ratings_CaregiverId",
                table: "Ratings",
                newName: "IX_Ratings_CaregiverId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_SeniorId",
                table: "Payments",
                newName: "IX_Payments_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_JobId",
                table: "Payments",
                newName: "IX_Payments_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_payments_CaregiverId",
                table: "Payments",
                newName: "IX_Payments_CaregiverId");

            migrationBuilder.RenameIndex(
                name: "IX_notifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_notifications_JobId",
                table: "Notifications",
                newName: "IX_Notifications_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_notifications_CareJobId",
                table: "Notifications",
                newName: "IX_Notifications_CareJobId");

            migrationBuilder.RenameIndex(
                name: "IX_complaints_SeniorId",
                table: "Complaints",
                newName: "IX_Complaints_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_complaints_JobId",
                table: "Complaints",
                newName: "IX_Complaints_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_complaints_CaregiverId",
                table: "Complaints",
                newName: "IX_Complaints_CaregiverId");

            migrationBuilder.RenameIndex(
                name: "IX_caregivers_UserId",
                table: "Caregivers",
                newName: "IX_Caregivers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_care_jobs_ServiceId",
                table: "CareJobs",
                newName: "IX_CareJobs_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_care_jobs_SeniorId",
                table: "CareJobs",
                newName: "IX_CareJobs_SeniorId");

            migrationBuilder.RenameIndex(
                name: "IX_care_jobs_CaregiverId",
                table: "CareJobs",
                newName: "IX_CareJobs_CaregiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seniors",
                table: "Seniors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaints",
                table: "Complaints",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Caregivers",
                table: "Caregivers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CareJobs",
                table: "CareJobs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Caregivers_AspNetUsers_UserId",
                table: "Caregivers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CareJobs_Caregivers_CaregiverId",
                table: "CareJobs",
                column: "CaregiverId",
                principalTable: "Caregivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CareJobs_Seniors_SeniorId",
                table: "CareJobs",
                column: "SeniorId",
                principalTable: "Seniors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CareJobs_Services_ServiceId",
                table: "CareJobs",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_CareJobs_JobId",
                table: "Complaints",
                column: "JobId",
                principalTable: "CareJobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Caregivers_CaregiverId",
                table: "Complaints",
                column: "CaregiverId",
                principalTable: "Caregivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Seniors_SeniorId",
                table: "Complaints",
                column: "SeniorId",
                principalTable: "Seniors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CareJobs_CareJobId",
                table: "Notifications",
                column: "CareJobId",
                principalTable: "CareJobs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_CareJobs_JobId",
                table: "Notifications",
                column: "JobId",
                principalTable: "CareJobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_CareJobs_JobId",
                table: "Payments",
                column: "JobId",
                principalTable: "CareJobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Caregivers_CaregiverId",
                table: "Payments",
                column: "CaregiverId",
                principalTable: "Caregivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Seniors_SeniorId",
                table: "Payments",
                column: "SeniorId",
                principalTable: "Seniors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_CareJobs_JobId",
                table: "Ratings",
                column: "JobId",
                principalTable: "CareJobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Caregivers_CaregiverId",
                table: "Ratings",
                column: "CaregiverId",
                principalTable: "Caregivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Seniors_SeniorId",
                table: "Ratings",
                column: "SeniorId",
                principalTable: "Seniors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seniors_AspNetUsers_UserId",
                table: "Seniors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CareJobs_CareJobId",
                table: "Transactions",
                column: "CareJobId",
                principalTable: "CareJobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Caregivers_CaregiverId",
                table: "Transactions",
                column: "CaregiverId",
                principalTable: "Caregivers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Seniors_SeniorId",
                table: "Transactions",
                column: "SeniorId",
                principalTable: "Seniors",
                principalColumn: "Id");
        }
    }
}
