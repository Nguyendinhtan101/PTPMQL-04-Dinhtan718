
using System;
using System.ComponentModel.DataAnnotations;  // ✅ Bổ sung thư viện

namespace MvcMovie.Models
{
    public class DaiLy
    {
        [Key]  // ✅ Xác định khóa chính
        public string MaDaiLy { get; set; }

        public string? TenDaiLy { get; set; }
        public string? DiaChi { get; set; }
        public string? NguoiDaiDien { get; set; }
        public string? DienThoai { get; set; }

        // Khóa ngoại liên kết với HeThongPhanPhoi
        public string? MaHTPP { get; set; }
        public HeThongPhanPhoi? HeThongPhanPhoi { get; set; }

        public DaiLy()
        {
            MaDaiLy = GenerateMaDaiLy();
        }

        private string GenerateMaDaiLy()
        {
            return "DL-" + Guid.NewGuid().ToString("N").Substring(6).ToUpper();
        }
    }
}
