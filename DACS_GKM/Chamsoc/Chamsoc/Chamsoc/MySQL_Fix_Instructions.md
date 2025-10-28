# Hướng dẫn sửa lỗi kết nối MySQL

## Lỗi: "Host 'HOANGGIANG' is not allowed to connect to this MySQL server"

### Cách 1: Thêm quyền truy cập cho user (Khuyên dùng)

Mở MySQL Command Line Client hoặc MySQL Workbench và chạy lệnh sau:

```sql
-- Kết nối MySQL với quyền root
mysql -u root -p

-- Tạo user mới hoặc cập nhật user hiện tại
CREATE USER IF NOT EXISTS 'root'@'localhost' IDENTIFIED BY '';
CREATE USER IF NOT EXISTS 'root'@'%' IDENTIFIED BY '';

-- Cấp quyền truy cập
GRANT ALL PRIVILEGES ON chamsoc.* TO 'root'@'localhost';
GRANT ALL PRIVILEGES ON chamsoc.* TO 'root'@'%';

-- Làm mới quyền
FLUSH PRIVILEGES;
```

### Cách 2: Kết nối bằng host IP thay vì localhost

1. Mở file `appsettings.json`
2. Thay đổi connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=chamsoc;User Id=root;Password=YOUR_PASSWORD;"
  }
}
```

### Cách 3: Kiểm tra MySQL đang chạy

1. Mở Services (Windows + R, gõ `services.msc`)
2. Tìm MySQL80 hoặc MySQL
3. Đảm bảo trạng thái là "Running"

### Cách 4: Tạo database nếu chưa có

```sql
-- Tạo database
CREATE DATABASE IF NOT EXISTS chamsoc CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- Sử dụng database
USE chamsoc;
```

### Kiểm tra kết nối:

```sql
SELECT USER(), HOST();
```

Nếu hiển thị 'root'@'%', nghĩa là đã cấu hình đúng.

### Nếu vẫn lỗi:

1. Kiểm tra MySQL đang chạy trên port nào:
   - Mở MySQL Command Line
   - Chạy: `SHOW VARIABLES LIKE 'port';`

2. Cập nhật file `appsettings.json` với port đúng:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=chamsoc;User Id=root;Password=YOUR_PASSWORD;"
  }
}
```

3. Nếu dùng XAMPP, thử:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=chamsoc;User Id=root;Password=;"
  }
}
```
