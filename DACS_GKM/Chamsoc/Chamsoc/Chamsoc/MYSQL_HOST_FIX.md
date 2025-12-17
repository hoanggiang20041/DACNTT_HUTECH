# MySQL Host Connection Error Fix

## Problem
```
MySqlException: Host 'HOANGGIANG' is not allowed to connect to this MySQL server
```

This error means your MySQL/MariaDB server has host-based access restrictions that prevent connections from your computer's hostname 'HOANGGIANG'.

## Root Cause
The MySQL user account (likely 'root') only allows connections from specific hosts (usually 'localhost' or '127.0.0.1'), but not from your machine's hostname.

## Solutions

### Solution 1: Use localhost/127.0.0.1 in Connection String (RECOMMENDED)

**This is the simplest and most secure solution.**

1. Open `appsettings.json` (or create it from `appsettings.example.json`)
2. Change the connection string from:
   ```
   Server=HOANGGIANG;Port=3306;Database=chamsoc;User Id=root;Password=YOUR_PASSWORD;
   ```
   To:
   ```
   Server=localhost;Port=3306;Database=chamsoc;User Id=root;Password=YOUR_PASSWORD;
   ```
   Or use IP address:
   ```
   Server=127.0.0.1;Port=3306;Database=chamsoc;User Id=root;Password=YOUR_PASSWORD;
   ```

3. Save and restart your application

---

### Solution 2: Grant MySQL User Access from Your Hostname

**If you need to use the hostname specifically:**

1. Open MySQL Command Line or MySQL Workbench
2. Connect as root user
3. Run these commands:

```sql
-- For root user
GRANT ALL PRIVILEGES ON chamsoc.* TO 'root'@'HOANGGIANG' IDENTIFIED BY 'YOUR_PASSWORD';
FLUSH PRIVILEGES;

-- Or if you want to allow from any host (less secure)
GRANT ALL PRIVILEGES ON chamsoc.* TO 'root'@'%' IDENTIFIED BY 'YOUR_PASSWORD';
FLUSH PRIVILEGES;
```

4. Verify the user has access:
```sql
SELECT user, host FROM mysql.user WHERE user='root';
```

You should see an entry like: `root | HOANGGIANG`

---

### Solution 3: Create a New MySQL User with Proper Permissions

**More secure approach - create a dedicated user:**

1. Open MySQL Command Line or MySQL Workbench
2. Connect as root user
3. Run these commands:

```sql
-- Create a new user
CREATE USER 'chamsoc_user'@'localhost' IDENTIFIED BY 'secure_password_here';

-- Grant privileges
GRANT ALL PRIVILEGES ON chamsoc.* TO 'chamsoc_user'@'localhost';
FLUSH PRIVILEGES;

-- Verify
SELECT user, host FROM mysql.user WHERE user='chamsoc_user';
```

4. Update your `appsettings.json`:
```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;Database=chamsoc;User Id=chamsoc_user;Password=secure_password_here;"
    }
}
```

---

## Troubleshooting

### Check MySQL User Privileges
```sql
-- View all users and their hosts
SELECT user, host FROM mysql.user;

-- View specific user privileges
SHOW GRANTS FOR 'root'@'localhost';
SHOW GRANTS FOR 'root'@'HOANGGIANG';
```

### Test Connection from Command Line
```bash
mysql -h localhost -u root -p
mysql -h 127.0.0.1 -u root -p
mysql -h HOANGGIANG -u root -p
```

### Check MySQL Server Status
```bash
# Windows
net start MySQL80
# or
net start MariaDB

# Check if running
netstat -an | findstr 3306
```

### Reset MySQL Root Password (if forgotten)
1. Stop MySQL service
2. Start with skip-grant-tables:
   ```bash
   mysqld --skip-grant-tables
   ```
3. Connect without password:
   ```bash
   mysql -u root
   ```
4. Reset password:
   ```sql
   FLUSH PRIVILEGES;
   ALTER USER 'root'@'localhost' IDENTIFIED BY 'new_password';
   ```

---

## Recommended Configuration

For development, use **Solution 1** (localhost):

**appsettings.json:**
```json
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;Database=chamsoc;User Id=root;Password=YOUR_PASSWORD;AllowLoadLocalInfile=true;"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    }
}
```

For production, use **Solution 3** (dedicated user with restricted permissions).

---

## Additional Notes

- Always use `localhost` or `127.0.0.1` for local development
- Never use `%` (all hosts) for root user in production
- Create dedicated database users with minimal required privileges
- Keep MySQL/MariaDB updated
- Use strong passwords for database users


