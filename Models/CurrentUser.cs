using System;

namespace YouthUnionManagement.Models
{
    public static class CurrentUser
    {
        public static User User { get; set; }

        public static bool IsAdmin => User?.Role == "Admin";
        public static bool IsManager => User?.Role == "Manager" || IsAdmin;
        public static bool IsMember => User != null;
    }
}