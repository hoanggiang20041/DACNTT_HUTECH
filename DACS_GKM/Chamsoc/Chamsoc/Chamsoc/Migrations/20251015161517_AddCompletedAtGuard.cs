using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chamsoc.Migrations
{
    /// <inheritdoc />
    public partial class AddCompletedAtGuard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
START TRANSACTION;
SET @schema := DATABASE();
-- Ensure users.RoleId is VARCHAR(36) nullable only if not already
SET @dt := (SELECT DATA_TYPE FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='users' AND COLUMN_NAME='RoleId' LIMIT 1);
SET @sql := IF(@dt IS NOT NULL AND (@dt <> 'varchar' OR (SELECT CHARACTER_MAXIMUM_LENGTH FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='users' AND COLUMN_NAME='RoleId' LIMIT 1) <> 36),
  'ALTER TABLE users MODIFY COLUMN RoleId VARCHAR(36) NULL;',
  'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
COMMIT;
");

            migrationBuilder.Sql(@"
START TRANSACTION;
SET @schema := DATABASE();
-- Add care_jobs.CompletedAt if missing
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='CompletedAt');
SET @sql := IF(@has=0, 'ALTER TABLE care_jobs ADD COLUMN CompletedAt DATETIME NULL;', 'SELECT 1');
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
-- Try revert RoleId to longtext (unsafe generally, but for rollback parity)
SET @dt := (SELECT DATA_TYPE FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='users' AND COLUMN_NAME='RoleId' LIMIT 1);
SET @sql := IF(@dt='varchar', 'ALTER TABLE users MODIFY COLUMN RoleId LONGTEXT NULL;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
COMMIT;
");

            migrationBuilder.Sql(@"
START TRANSACTION;
SET @schema := DATABASE();
-- Drop CompletedAt if we added it (best-effort)
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='CompletedAt');
SET @sql := IF(@has=1, 'ALTER TABLE care_jobs DROP COLUMN CompletedAt;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
COMMIT;
");
        }
    }
}
