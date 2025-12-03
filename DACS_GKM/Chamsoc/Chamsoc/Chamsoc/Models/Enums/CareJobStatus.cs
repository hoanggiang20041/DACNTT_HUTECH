namespace Chamsoc.Models.Enums
{
    /// <summary>
    /// Centralized definition of care job statuses to avoid hard-coded strings.
    /// </summary>
    public static class CareJobStatus
    {
        public const string Cho = "Đang chờ";
        public const string ChoXacNhanCaregiver = "Đang chờ xác nhận từ Caregiver";
        public const string ChoXacNhanSenior = "Đang chờ xác nhận từ Senior";
        public const string ChoCaregiverThanhToanCoc = "Đang chờ Người chăm sóc thanh toán cọc";
        public const string DangThucHien = "Đang thực hiện";
        public const string ChoXacNhanHoanThanh = "Đang chờ xác nhận hoàn thành";
        public const string HoanThanh = "Hoàn thành";
        public const string Huy = "Hủy";
    }
}


