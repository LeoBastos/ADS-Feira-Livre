using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ads.feira.application.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var memberInfos = enumType.GetMember(enumValue.ToString());
            if (memberInfos.Length > 0)
            {
                var attributes = memberInfos[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attributes.Length > 0)
                {
                    return ((DisplayAttribute)attributes[0]).Name;
                }
            }
            return enumValue.ToString();
        }
    }
}
