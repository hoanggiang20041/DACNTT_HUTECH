# PROMPT CHUYÊN NGHIỆP - SỬA LỖI SIDEBAR ACTIVE STATE FILL

## 🎯 VẤN ĐỀ

Sidebar admin panel có lỗi hiển thị box màu (active state) cho menu item được chọn. Box màu xanh dương không fill đầy đủ hoặc không hiển thị đúng như thiết kế.

### Mô tả lỗi:
- Menu item active (được chọn) có background gradient xanh dương (#007bff → #0d6efd)
- Box màu không fill đầy đủ từ trái sang phải
- Có thể bị thiếu padding, margin không đúng, hoặc border-radius làm mất góc
- Active state không nổi bật so với các item khác

### Ảnh minh họa:
- Icon "users" (3 người) đang active với background xanh dương
- Các icon khác có màu xám (inactive)
- Box màu xanh cần fill đầy đủ và đẹp

---

## 📋 THÔNG TIN KỸ THUẬT

### File liên quan:
1. **CSS:** `wwwroot/css/admin/admin-sidebar.css`
2. **HTML:** `Views/Shared/_AdminLayout.cshtml`
3. **Framework:** ASP.NET Core MVC, Bootstrap 5.3
4. **Icon:** Font Awesome 6

### Selector CSS hiện tại:
```css
#sidebar .nav-link.active,
#sidebar .nav-link.active:hover {
    background: linear-gradient(135deg, var(--medical-blue) 0%, var(--medical-blue-light) 100%) !important;
    color: var(--medical-white) !important;
    box-shadow: var(--medical-shadow-md) !important;
    font-weight: 600 !important;
    transform: none !important;
}
```

### HTML structure:
```html
<li class="nav-item">
    <a class="nav-link active" href="...">
        <i class="fas fa-users"></i>
        <span class="menu-title">Người dùng</span>
    </a>
</li>
```

---

## ✅ YÊU CẦU SỬA

### 1. Box màu active phải fill đầy đủ:
- **Width:** Fill từ mép trái đến mép phải của sidebar (trừ margin/padding hợp lý)
- **Height:** Đủ cao để chứa icon + text, padding vertical hợp lý
- **Border-radius:** 12px (giữ nguyên thiết kế bo góc mềm)
- **Padding:** `0.875rem 1.25rem` (đã có, nhưng cần đảm bảo fill đầy đủ)

### 2. Màu sắc và hiệu ứng:
- **Background:** Gradient xanh dương `linear-gradient(135deg, #007bff 0%, #0d6efd 100%)`
- **Text color:** Trắng `#ffffff`
- **Icon color:** Trắng `#ffffff`
- **Box-shadow:** `0 4px 16px rgba(0, 123, 255, 0.12)` để tạo chiều sâu
- **Indicator bar:** Thanh màu cam `#fd7e14` bên trái (4px width, 60% height)

### 3. Spacing và alignment:
- **Margin:** `0.25rem 0.75rem` (đã có)
- **Gap giữa icon và text:** `0.875rem`
- **Alignment:** Icon và text căn giữa theo chiều dọc
- **Text alignment:** Căn trái

### 4. Responsive:
- Desktop: Box fill đầy đủ
- Tablet: Box fill đầy đủ
- Mobile: Box fill đầy đủ khi sidebar mở

---

## 🔧 CODE CẦN SỬA

### File: `wwwroot/css/admin/admin-sidebar.css`

**Vị trí:** Khoảng dòng 248-284 (phần Active State)

**Code hiện tại cần kiểm tra:**
```css
/* Active State - Tăng specificity để override CSS khác */
#sidebar .nav-link.active,
#sidebar .nav-link.active:hover {
    background: linear-gradient(135deg, var(--medical-blue) 0%, var(--medical-blue-light) 100%) !important;
    color: var(--medical-white) !important;
    box-shadow: var(--medical-shadow-md) !important;
    font-weight: 600 !important;
    transform: none !important;
}

#sidebar .nav-link.active i,
#sidebar .nav-link.active:hover i {
    color: var(--medical-white) !important;
    transform: none !important;
}

#sidebar .nav-link.active::before {
    content: '';
    position: absolute;
    left: 0;
    top: 50%;
    transform: translateY(-50%);
    width: 4px;
    height: 60%;
    background: var(--medical-orange);
    border-radius: 0 4px 4px 0;
    z-index: 1;
}

#sidebar .nav-link.active span,
#sidebar .nav-link.active:hover span {
    color: var(--medical-white) !important;
}
```

**Vấn đề có thể có:**
1. Margin `0.25rem 0.75rem` có thể làm box không fill đầy đủ
2. Padding có thể không đủ
3. Width có thể bị giới hạn bởi parent container
4. Border-radius có thể làm mất góc
5. Position relative/absolute có thể ảnh hưởng

---

## 🎨 THIẾT KẾ MONG MUỐN

### Visual:
```
┌─────────────────────────────────┐
│ [Sidebar - 280px width]        │
│                                 │
│ ┌─────────────────────────────┐ │
│ │ 🧑‍🤝‍🧑 Người dùng    [ACTIVE]│ │ ← Box xanh fill đầy đủ
│ └─────────────────────────────┘ │
│                                 │
│ ⚠️  Khiếu nại                   │ ← Inactive (xám)
│                                 │
│ 📊 Thống kê                     │ ← Inactive (xám)
│                                 │
└─────────────────────────────────┘
```

### Specifications:
- **Box active:**
  - Background: Gradient blue (#007bff → #0d6efd)
  - Width: ~100% (trừ margin 0.75rem mỗi bên = 250px width)
  - Height: Auto (padding 0.875rem top/bottom)
  - Border-radius: 12px
  - Left indicator: 4px width, orange (#fd7e14)
  - Shadow: Medium shadow để nổi bật

---

## 📝 HƯỚNG DẪN SỬA CHI TIẾT

### Bước 1: Kiểm tra CSS hiện tại
- Mở file `wwwroot/css/admin/admin-sidebar.css`
- Tìm phần `.nav-link.active` (khoảng dòng 248-284)
- Kiểm tra các thuộc tính: `margin`, `padding`, `width`, `display`

### Bước 2: Đảm bảo box fill đầy đủ
```css
#sidebar .nav-link.active {
    /* Đảm bảo width fill đầy đủ */
    width: calc(100% - 1.5rem); /* Trừ margin 0.75rem mỗi bên */
    /* Hoặc */
    margin: 0.25rem 0.75rem;
    display: flex; /* Đã có */
    /* Đảm bảo padding đủ */
    padding: 0.875rem 1.25rem; /* Đã có */
}
```

### Bước 3: Kiểm tra parent container
- Đảm bảo `.nav-item` không có padding/margin làm ảnh hưởng
- Đảm bảo `ul.nav.flex-column` không có padding thừa

### Bước 4: Test và verify
- Mở trang admin (ví dụ: `/Admin/ManageUsers`)
- Kiểm tra menu "Người dùng" có box xanh fill đầy đủ không
- Kiểm tra trên desktop, tablet, mobile
- Kiểm tra khi sidebar collapsed (80px width)

---

## 🐛 CÁC LỖI THƯỜNG GẶP VÀ CÁCH SỬA

### Lỗi 1: Box không fill đầy đủ từ trái
**Nguyên nhân:** Margin trái quá lớn hoặc padding của parent
**Giải pháp:**
```css
#sidebar .nav-link.active {
    margin-left: 0.75rem; /* Giảm nếu cần */
    margin-right: 0.75rem;
}
```

### Lỗi 2: Box bị cắt góc
**Nguyên nhân:** Border-radius quá lớn hoặc overflow hidden
**Giải pháp:**
```css
#sidebar .nav-link.active {
    border-radius: 12px; /* Đảm bảo không quá lớn */
    overflow: visible; /* Không dùng hidden */
}
```

### Lỗi 3: Box không đủ cao
**Nguyên nhân:** Padding vertical không đủ
**Giải pháp:**
```css
#sidebar .nav-link.active {
    padding-top: 0.875rem;
    padding-bottom: 0.875rem;
    min-height: 44px; /* Đảm bảo đủ cao để click */
}
```

### Lỗi 4: Background không hiển thị
**Nguyên nhân:** CSS bị override hoặc specificity không đủ
**Giải pháp:**
```css
#sidebar .nav-link.active {
    background: linear-gradient(135deg, #007bff 0%, #0d6efd 100%) !important;
    /* Thêm !important nếu cần */
}
```

### Lỗi 5: Indicator bar (cam) không hiển thị
**Nguyên nhân:** Z-index hoặc position không đúng
**Giải pháp:**
```css
#sidebar .nav-link.active::before {
    position: absolute;
    left: 0;
    z-index: 2; /* Đảm bảo trên cùng */
}
```

---

## ✅ CHECKLIST HOÀN THÀNH

Sau khi sửa, đảm bảo:
- [ ] Box màu xanh fill đầy đủ từ trái sang phải
- [ ] Box có border-radius 12px mềm mại
- [ ] Text và icon màu trắng, rõ ràng
- [ ] Có indicator bar màu cam bên trái
- [ ] Box có shadow để nổi bật
- [ ] Hover state vẫn hoạt động (không bị override)
- [ ] Responsive tốt trên mọi thiết bị
- [ ] Khi sidebar collapsed (80px), active state vẫn đẹp

---

## 🎯 KẾT QUẢ MONG ĐỢI

Sau khi sửa:
- Menu item active có box màu xanh dương gradient fill đầy đủ
- Box có góc bo tròn 12px mềm mại
- Text và icon màu trắng, dễ đọc
- Có thanh indicator màu cam bên trái
- Box nổi bật so với các item inactive (xám)
- Hoạt động mượt mà, không bị giật

---

## 📞 LƯU Ý

1. **Giữ nguyên thiết kế:** Không thay đổi màu sắc, chỉ sửa layout/spacing
2. **Tương thích:** Đảm bảo hoạt động với Bootstrap 5.3
3. **Responsive:** Test trên desktop, tablet, mobile
4. **Performance:** Không thêm animation phức tạp
5. **Accessibility:** Đảm bảo contrast đủ để đọc

---

**Prompt này đủ chi tiết để AI agent khác hiểu và sửa lỗi sidebar active state fill box màu!** 🎯

