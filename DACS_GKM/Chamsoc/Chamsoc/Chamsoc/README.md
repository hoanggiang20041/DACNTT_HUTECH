# Chamsoc - GKM Care System

Hệ thống chăm sóc người cao tuổi với ASP.NET Core MVC.

## Công nghệ sử dụng
- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- MariaDB/MySQL
- SignalR
- Identity Framework

## Cài đặt và chạy

### 1. Cài đặt dependencies
```bash
dotnet restore
```

### 2. Cấu hình database
- Tạo file `appsettings.json` từ `appsettings.example.json`
- Cập nhật connection string với thông tin database của bạn
- Cập nhật OpenRouter API key

### 3. Chạy migrations
```bash
dotnet ef database update
```

### 4. Chạy ứng dụng
```bash
dotnet run
```

Ứng dụng sẽ chạy tại: `https://localhost:7198`

### 5. Tạo tunnel để truy cập từ bên ngoài
```bash
cloudflared tunnel --url https://localhost:7198 --no-tls-verify
```

## Tài khoản mặc định
- **Admin**: `admin` / `Admin123!`
- **Email**: `admin@gkmcare.com`

## Cấu trúc dự án
- `Controllers/` - API Controllers
- `Models/` - Data Models
- `Views/` - Razor Views
- `Services/` - Business Logic Services
- `Data/` - Database Context
- `Hubs/` - SignalR Hubs

## Lưu ý bảo mật
- Không commit file `appsettings.json` chứa thông tin nhạy cảm
- Sử dụng User Secrets cho development
- Cấu hình CORS phù hợp cho production