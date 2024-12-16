using System.ComponentModel;

namespace Zimovka.View
{
    public enum MenuEnum
    {
        [Description("Поиск по регионам")]
        userSearch,

        [Description("Добавить в избранное")]
        addFav,

        [Description("Открыть избранное")]
        userFav,

        [Description("Выход из программы")]
        userExit,

        [Description("Неверный ввод. Повторите попытку")]
        Unknown
    }
}