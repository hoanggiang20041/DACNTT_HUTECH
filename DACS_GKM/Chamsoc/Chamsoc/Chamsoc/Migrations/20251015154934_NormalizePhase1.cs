using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chamsoc.Migrations
{
    /// <inheritdoc />
    public partial class NormalizePhase1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
START TRANSACTION;

-- 1) notifications: drop FK (if any), move data, drop column CareJobId
SET @schema := DATABASE();
SELECT CONSTRAINT_NAME INTO @fk_name
FROM information_schema.KEY_COLUMN_USAGE
WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications'
  AND COLUMN_NAME='CareJobId' AND REFERENCED_TABLE_NAME IS NOT NULL
LIMIT 1;

SET @sql := IF(@fk_name IS NOT NULL,
  CONCAT('ALTER TABLE notifications DROP FOREIGN KEY ', @fk_name, ';'),
  'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

SET @has_col := (SELECT COUNT(*) FROM information_schema.COLUMNS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications' AND COLUMN_NAME='CareJobId');

-- move data only if column exists
SET @sql := IF(@has_col>0,
  'UPDATE notifications SET JobId = COALESCE(JobId, CareJobId) WHERE JobId IS NULL AND CareJobId IS NOT NULL;',
  'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

-- drop column if exists
SET @sql := IF(@has_col>0, 'ALTER TABLE notifications DROP COLUMN CareJobId;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

COMMIT;
            ");

            migrationBuilder.Sql(@"
START TRANSACTION;

-- 2) users.RoleId: ensure VARCHAR(36), cleanup orphans, index, FK -> roles(Id)
SET @schema := DATABASE();
SET @data_type := (
  SELECT DATA_TYPE FROM information_schema.COLUMNS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='users' AND COLUMN_NAME='RoleId' LIMIT 1
);
SET @sql := IF(@data_type IS NOT NULL AND @data_type <> 'varchar',
  'ALTER TABLE users MODIFY COLUMN RoleId VARCHAR(36) NULL;',
  'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

-- cleanup orphans (set to NULL if no matching roles.Id)
UPDATE users u
LEFT JOIN roles r ON r.Id = u.RoleId
SET u.RoleId = NULL
WHERE u.RoleId IS NOT NULL AND r.Id IS NULL;

-- index
SET @has_idx := (SELECT COUNT(*) FROM information_schema.STATISTICS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='users' AND INDEX_NAME='IX_users_RoleId');
SET @sql := IF(@has_idx=0, 'CREATE INDEX IX_users_RoleId ON users (RoleId);', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

-- FK
SET @has_fk := (SELECT COUNT(*) FROM information_schema.KEY_COLUMN_USAGE
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='users' AND COLUMN_NAME='RoleId'
    AND REFERENCED_TABLE_NAME IS NOT NULL);
SET @sql := IF(@has_fk=0,
  'ALTER TABLE users ADD CONSTRAINT FK_users_roles_RoleId FOREIGN KEY (RoleId) REFERENCES roles(Id) ON DELETE SET NULL ON UPDATE CASCADE;',
  'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

COMMIT;
            ");

            migrationBuilder.Sql(@"
START TRANSACTION;

-- 3) payments.TransactionId: change to VARCHAR(128) if not varchar, add index
SET @schema := DATABASE();
SET @data_type := (
  SELECT DATA_TYPE FROM information_schema.COLUMNS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='payments' AND COLUMN_NAME='TransactionId' LIMIT 1
);
SET @sql := IF(@data_type IS NOT NULL AND @data_type <> 'varchar',
  'ALTER TABLE payments MODIFY COLUMN TransactionId VARCHAR(128) NULL;',
  'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

SET @has_idx := (SELECT COUNT(*) FROM information_schema.STATISTICS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='payments' AND INDEX_NAME='IX_payments_TransactionId');
SET @sql := IF(@has_idx=0, 'CREATE INDEX IX_payments_TransactionId ON payments (TransactionId);', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

COMMIT;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
START TRANSACTION;

-- 3) payments.TransactionId rollback: drop index, attempt revert to TEXT
SET @schema := DATABASE();
SET @has_idx := (SELECT COUNT(*) FROM information_schema.STATISTICS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='payments' AND INDEX_NAME='IX_payments_TransactionId');
SET @sql := IF(@has_idx>0, 'DROP INDEX IX_payments_TransactionId ON payments;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

SET @data_type := (
  SELECT DATA_TYPE FROM information_schema.COLUMNS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='payments' AND COLUMN_NAME='TransactionId' LIMIT 1
);
SET @sql := IF(@data_type='varchar', 'ALTER TABLE payments MODIFY COLUMN TransactionId TEXT NULL;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

COMMIT;
            ");

            migrationBuilder.Sql(@"
START TRANSACTION;

-- 2) users.RoleId rollback: drop FK, drop index, attempt revert to TEXT
SET @schema := DATABASE();
-- drop FK if exists
SET @fk_name := (SELECT CONSTRAINT_NAME FROM information_schema.KEY_COLUMN_USAGE
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='users' AND COLUMN_NAME='RoleId' AND REFERENCED_TABLE_NAME='roles' LIMIT 1);
SET @sql := IF(@fk_name IS NOT NULL, CONCAT('ALTER TABLE users DROP FOREIGN KEY ', @fk_name, ';'), 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

-- drop index
SET @has_idx := (SELECT COUNT(*) FROM information_schema.STATISTICS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='users' AND INDEX_NAME='IX_users_RoleId');
SET @sql := IF(@has_idx>0, 'DROP INDEX IX_users_RoleId ON users;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

-- revert type
SET @data_type := (
  SELECT DATA_TYPE FROM information_schema.COLUMNS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='users' AND COLUMN_NAME='RoleId' LIMIT 1
);
SET @sql := IF(@data_type='varchar', 'ALTER TABLE users MODIFY COLUMN RoleId TEXT NULL;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

COMMIT;
            ");

            migrationBuilder.Sql(@"
START TRANSACTION;

-- 1) notifications rollback: re-add column CareJobId (NULL), best-effort restore FK
SET @schema := DATABASE();
SET @has_col := (SELECT COUNT(*) FROM information_schema.COLUMNS
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications' AND COLUMN_NAME='CareJobId');
SET @sql := IF(@has_col=0, 'ALTER TABLE notifications ADD COLUMN CareJobId INT NULL;', 'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

-- try restore FK if care_jobs exists
SET @has_fk := (SELECT COUNT(*) FROM information_schema.KEY_COLUMN_USAGE
  WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='notifications' AND COLUMN_NAME='CareJobId' AND REFERENCED_TABLE_NAME IS NOT NULL);
SET @sql := IF(@has_fk=0 AND EXISTS(SELECT 1 FROM information_schema.TABLES WHERE TABLE_SCHEMA=@schema AND TABLE_NAME='care_jobs'),
  'ALTER TABLE notifications ADD CONSTRAINT FK_notifications_care_jobs_CareJobId FOREIGN KEY (CareJobId) REFERENCES care_jobs(Id) ON DELETE SET NULL ON UPDATE CASCADE;',
  'SELECT 1');
PREPARE s FROM @sql; EXECUTE s; DEALLOCATE PREPARE s;

COMMIT;
            ");
        }
    }
}
