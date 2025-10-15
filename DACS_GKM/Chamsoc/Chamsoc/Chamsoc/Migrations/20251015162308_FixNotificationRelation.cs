using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chamsoc.Migrations
{
    /// <inheritdoc />
    public partial class FixNotificationRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
START TRANSACTION;
SET @schema := DATABASE();
-- Drop FK if exists
SET @fk := (SELECT CONSTRAINT_NAME FROM information_schema.KEY_COLUMN_USAGE WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications' AND COLUMN_NAME='CareJobId' AND REFERENCED_TABLE_NAME IS NOT NULL LIMIT 1);
SET @sql := IF(@fk IS NOT NULL, CONCAT('ALTER TABLE notifications DROP FOREIGN KEY ', @fk, ';'), 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Drop index if exists
SET @idx := (SELECT INDEX_NAME FROM information_schema.STATISTICS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications' AND INDEX_NAME='IX_notifications_CareJobId' LIMIT 1);
SET @sql := IF(@idx IS NOT NULL, 'DROP INDEX IX_notifications_CareJobId ON notifications;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Drop column if exists
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications' AND COLUMN_NAME='CareJobId');
SET @sql := IF(@has=1, 'ALTER TABLE notifications DROP COLUMN CareJobId;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
COMMIT;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
START TRANSACTION;
SET @schema := DATABASE();
-- Add column if missing
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications' AND COLUMN_NAME='CareJobId');
SET @sql := IF(@has=0, 'ALTER TABLE notifications ADD COLUMN CareJobId INT NULL;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Create index if missing
SET @idx := (SELECT COUNT(*) FROM information_schema.STATISTICS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications' AND INDEX_NAME='IX_notifications_CareJobId');
SET @sql := IF(@idx=0, 'CREATE INDEX IX_notifications_CareJobId ON notifications (CareJobId);', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Add FK if not exists
SET @fk := (SELECT CONSTRAINT_NAME FROM information_schema.KEY_COLUMN_USAGE WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications' AND COLUMN_NAME='CareJobId' AND REFERENCED_TABLE_NAME IS NOT NULL LIMIT 1);
SET @sql := IF(@fk IS NULL, 'ALTER TABLE notifications ADD CONSTRAINT FK_notifications_care_jobs_CareJobId FOREIGN KEY (CareJobId) REFERENCES care_jobs(Id);', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
COMMIT;
");
        }
    }
}
