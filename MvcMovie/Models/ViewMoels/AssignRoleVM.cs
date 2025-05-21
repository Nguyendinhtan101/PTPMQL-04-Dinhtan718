
namespace MvcMovie.Models.ViewModels
{
    public class AssignRoleVM
    {
        public string UserId { get; set; }

        public IList<string> SelectedRoles { get; set; } = new List<string>(); // thêm khởi tạo mặc định

        public IList<RoleVM> AllRoles { get; set; } = new List<RoleVM>();
    }

    public class RoleVM
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        // (Tùy chọn) Thêm để hiển thị role đã được chọn hay chưa
        public bool IsSelected { get; set; } = false;
    }
}