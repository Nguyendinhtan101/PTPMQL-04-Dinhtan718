namespace MvcMovie.Models.ViewModels
{
    public class UserWithRoleVM
    {
        public ApplicationUser User { get; set; }

        // Khởi tạo để luôn có danh sách, không bị null
        public IList<string> Roles { get; set; } = new List<string>();
    }
}