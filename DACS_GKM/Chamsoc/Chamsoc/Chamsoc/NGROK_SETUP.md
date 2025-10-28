# Hướng dẫn sử dụng Ngrok

## Bước 1: Download và cài đặt Ngrok

1. Truy cập https://ngrok.com/download
2. Download ngrok cho Windows
3. Giải nén file vào thư mục bất kỳ (ví dụ: `C:\ngrok`)
4. Đăng ký tài khoản miễn phí tại https://ngrok.com để lấy authtoken

## Bước 2: Setup Ngrok

Mở Command Prompt và chạy:

```bash
cd C:\ngrok
ngrok config add-authtoken YOUR_AUTHTOKEN
```

## Bước 3: Chạy ứng dụng

Chạy ứng dụng ASP.NET Core ở một terminal:

```bash
cd DACS_GKM\Chamsoc\Chamsoc\Chamsoc
dotnet run
```

Ứng dụng sẽ chạy tại: `https://localhost:7198`

## Bước 4: Chạy Ngrok

Mở terminal mới và chạy:

```bash
cd C:\ngrok
ngrok http 7198 --host-header=localhost:7198
```

Ngrok sẽ tạo một URL công khai như: `https://xxxx-xxxx-xxxx.ngrok-free.app`

## Bước 5: Test kết nối

1. Copy URL ngrok từ terminal (https://xxxx-xxxx-xxxx.ngrok-free.app)
2. Gửi URL này cho người bạn test
3. Truy cập URL trong trình duyệt
4. Đăng nhập và test tính năng video call

## Lưu ý quan trọng:

### 1. Ngrok Free Plan:
- URL thay đổi mỗi lần chạy (trừ khi dùng custom domain)
- Có thể bị giới hạn về traffic
- Có ngrok interstitial page

### 2. Bỏ qua interstitial page:
Nếu bị chặn bởi ngrok interstitial page, thêm header này vào code:

```bash
# Thêm option khi chạy ngrok
ngrok http 7198 --host-header=localhost:7198 --request-header-add="ngrok-skip-browser-warning:true"
```

### 3. Certificate SSL:
- Ngrok tự động cung cấp HTTPS
- Có thể gặp lỗi certificate warning (bấm Advanced -> Continue)

### 4. WebRTC:
- WebRTC cần HTTPS để access camera/micro
- Ngrok cung cấp HTTPS tự động
- Không cần config thêm

## Cấu hình JavaScript tự động phát hiện ngrok:

Tạo file `wwwroot/js/ngrok-config.js`:

```javascript
// Auto detect ngrok domain
(function() {
    const isNgrok = window.location.hostname.includes('ngrok');
    if (isNgrok) {
        console.log('Detected ngrok domain, updating base URL');
        // Update base URL if needed
    }
})();
```

## Troubleshooting:

### Lỗi CORS:
- CORS đã được cấu hình tự động trong Program.cs
- Nếu vẫn lỗi, kiểm tra console browser

### SignalR không kết nối:
- Đảm bảo ngrok URL được truy cập qua HTTPS
- Kiểm tra firewall không block SignalR port

### Camera/Micro không hoạt động:
- WebRTC yêu cầu HTTPS (ngrok đã cung cấp)
- Cho phép camera/micro trong browser
- Kiểm tra console browser để xem lỗi

## Upgrade lên ngrok Pro (Optional):

Nếu muốn URL cố định và không bị giới hạn:
1. Upgrade tại https://dashboard.ngrok.com/billing
2. Setup custom domain
3. Thêm domain vào CORS config trong Program.cs

