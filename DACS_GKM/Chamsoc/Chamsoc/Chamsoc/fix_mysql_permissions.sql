-- Fix MySQL Host Connection Error
-- Run this script in MySQL Workbench or MySQL Command Line as root user

-- Check current root user hosts
SELECT user, host FROM mysql.user WHERE user='root';

-- Grant permissions for root to connect from HOANGGIANG
GRANT ALL PRIVILEGES ON *.* TO 'root'@'HOANGGIANG' IDENTIFIED BY 'YOUR_PASSWORD_HERE' WITH GRANT OPTION;

-- Also ensure root can connect from localhost (backup)
GRANT ALL PRIVILEGES ON *.* TO 'root'@'localhost' IDENTIFIED BY 'YOUR_PASSWORD_HERE' WITH GRANT OPTION;

-- Flush privileges to apply changes
FLUSH PRIVILEGES;

-- Verify the changes
SELECT user, host FROM mysql.user WHERE user='root';

-- Test connection (optional - run from command line)
-- mysql -h HOANGGIANG -u root -p
-- mysql -h localhost -u root -p


