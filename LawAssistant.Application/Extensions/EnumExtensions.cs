using System.ComponentModel;
using System.Reflection;

namespace LawAssistant.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum genericEnum)
        {
            Type type = genericEnum.GetType();
            string text = genericEnum.ToString();
            MemberInfo[] member = type.GetMember(text);
            if (member != null && member.Length <= 0)
            {
                return text;
            }

            object[] customAttributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
            if (customAttributes == null || customAttributes.Length > 0)
            {
                return (customAttributes[0] as DescriptionAttribute)?.Description ?? text;
            }

            return text;
        }
    }
}