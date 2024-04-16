using System.ComponentModel;

namespace darts.db.Enums
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Получить описание значения enum (значение аттрибута <see cref="DescriptionAttribute"/>)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
                return attributes.First().Description;

            return value.ToString();
        }
    }
}
