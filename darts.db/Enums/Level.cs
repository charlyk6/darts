using System.ComponentModel;

namespace darts.db.Enums
{
    public enum Level
    {
        [Description("Легкий")]
        Easy = 0,
        [Description("Средний")]
        Medium = 1,
        [Description("Сложный")]
        Hard = 2
    }
}
