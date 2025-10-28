# Hướng dẫn lấy Ngrok AuthToken

## Bước 1: Đăng ký tài khoản Ngrok

1. Truy cập: https://ngrok.com/signup
2. Đăng ký tài khoản miễn phí bằng:
   - Email: `your-email@example.com`
   - Password: Tạo password mạnh
   - Hoặc đăng nhập bằng Google/GitHub

## Bước 2: Lấy Authtoken

### Cách 1: Từ Dashboard Web
1. Đăng nhập tại: https://dashboard.ngrok.com/
2. Vào mục **"Your Authtoken"** hoặc **"Getting Started"**
3. Copy Authtoken (dạng: `2xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx`)

### Cách 2: Từ Getting Started Page
1. Truy cập: https://dashboard.ngrok.com/get-started/your-authtoken
2. Copy token hiển thị trên trang

## Bước 3: Setup Token

### Option 1: Setup tự động (Recommended)
Mở Command Prompt và chạy:

```bash
ngrok config add-authtoken YOUR_AUTHTOKEN_HERE
```

Ví dụ:
```bash
ngrok config add-authtoken 2abcdefghijklmnopqrstuvwxyz1234567
```

### Option 2: Setup thủ công
1. Tìm file config: `C:\Users\YOUR_USERNAME\AppData\Local\ngrok\ngrok.yml`
2. Mở file bằng Notepad
3. Thêm hoặc sửa dòng:
```yaml
authtoken: YOUR_AUTHTOKEN_HERE
```

## Bước 4: Verify Token đã được setup

Chạy lệnh để kiểm tra:
```bash
ngrok config check
```

Nếu thấy dòng `✓ authtoken` màu xanh là OK!

## Lưu ý:

⚠️ **KHÔNG** share authtoken với ai, đây là thông tin bảo mật!
⚠️ Authtoken chỉ cần setup 1 lần, sau đó dùng mãi mãi
⚠️ Nếu mất token, có thể reset tại dashboard

## Troubleshooting:

### "authtoken is required" 
→ Chưa setup authtoken, làm theo Bước 3

### "Invalid authtoken"
→ Token sai hoặc đã bị revoke, lấy token mới

### "Your account is limited"
→ Free plan có giới hạn, nhưng đủ để test video call

---

**TL;DR:**
1. Đăng ký tại https://ngrok.com/signup
2. Copy token tại https://dashboard.ngrok.com/get-started/your-authtoken
3. Chạy: `ngrok config add-authtoken PASTE_TOKEN_HERE`
4. Done! ✅

