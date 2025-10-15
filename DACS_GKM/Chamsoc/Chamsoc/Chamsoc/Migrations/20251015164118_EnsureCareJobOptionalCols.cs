using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chamsoc.Migrations
{
    /// <inheritdoc />
    public partial class EnsureCareJobOptionalCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
START TRANSACTION;
SET @schema := DATABASE();
-- Add HasComplained (TINYINT(1) NULL)
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='HasComplained');
SET @sql := IF(@has=0, 'ALTER TABLE care_jobs ADD COLUMN HasComplained TINYINT(1) NULL;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Add HasRated (TINYINT(1) NULL)
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='HasRated');
SET @sql := IF(@has=0, 'ALTER TABLE care_jobs ADD COLUMN HasRated TINYINT(1) NULL;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Add Rating (DECIMAL(65,30) NULL) to match snapshot
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='Rating');
SET @sql := IF(@has=0, 'ALTER TABLE care_jobs ADD COLUMN Rating DECIMAL(65,30) NULL;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Add Review (VARCHAR(500) NULL)
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='Review');
SET @sql := IF(@has=0, 'ALTER TABLE care_jobs ADD COLUMN Review VARCHAR(500) NULL;', 'SELECT 1');
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
-- Drop Review if exists
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='Review');
SET @sql := IF(@has=1, 'ALTER TABLE care_jobs DROP COLUMN Review;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Drop Rating if exists
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='Rating');
SET @sql := IF(@has=1, 'ALTER TABLE care_jobs DROP COLUMN Rating;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Drop HasRated if exists
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='HasRated');
SET @sql := IF(@has=1, 'ALTER TABLE care_jobs DROP COLUMN HasRated;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
-- Drop HasComplained if exists
SET @has := (SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs' AND COLUMN_NAME='HasComplained');
SET @sql := IF(@has=1, 'ALTER TABLE care_jobs DROP COLUMN HasComplained;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;
COMMIT;
");
        }
    }
}
