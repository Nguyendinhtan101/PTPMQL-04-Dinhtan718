namespace MvcMovie.Models.Constants
{
    public static class SystemPermissions
    {
        public static List<string> AllPermissions => new List<string>
        {
            "User.View",
            "User.Create",
            "User.Edit",
            "User.Delete",
            "Role.View",
            "Role.Create",
            "Role.Edit",
            "Role.Delete"
            // Thêm quyền khác nếu cần
        };
    }
}
