
using System.ComponentModel.DataAnnotations;
using MvcMovie.Models;
public class HeThongPhanPhoi
{
    [Key] // ✅ Thêm khóa chính
    public string MaHTPP { get; set; }

    public string TenHTPP { get; set; }

    // Liên kết với nhiều DaiLy
    public List<DaiLy>? DaiLys { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public HeThongPhanPhoi()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
        MaHTPP = GenerateMaHTPP();
    }

    private string GenerateMaHTPP()
    {
        return "HTPP-" + Guid.NewGuid().ToString("N").Substring(6).ToUpper();
    }
}
