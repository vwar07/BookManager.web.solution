using BookManager.web.Constants;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace BookManager.web
{
    public static class Utility
    {
        public static string ToUniverselPath(this string path)
        {
            if (IsWindows())
            {
                return path.Replace("/", @"\");
            }
            else if (IsMacOS())
            {
                return path.Replace(@"\", "/");
            }
            else if (!IsLinux())
            {
                return path.Replace(@"\", "/");
            }
            else
            {
                return path;
            }
        }

        public static bool IsWindows() =>
       RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static bool IsMacOS() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        public static bool IsLinux() =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        public static bool IsValidTimeFormat(string timeString)
        {
            string pattern = @"^\d{1,2}h \d{1,2}m$";
            return Regex.IsMatch(timeString, pattern);
        }


        public static List<KeyValuePair<int, string>> GetDefaultRoleList()
        {
            List<KeyValuePair<int, string>> roleList = Enum.GetValues(typeof(DefaultRoles))
            .Cast<DefaultRoles>()
            .Select(role => new KeyValuePair<int, string>((int)role, role.ToString()))
            .ToList();

            return roleList;
        }
    }
}
