using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ads.feira.api.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var memberInfos = enumType.GetMember(enumValue.ToString());
            var attributes = memberInfos[0].GetCustomAttributes(typeof(DisplayAttribute), false);

            return attributes.Length > 0 ? ((DisplayAttribute)attributes[0]).Name : enumValue.ToString();
        }
    }
}
