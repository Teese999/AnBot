using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnBot
{
    public enum DayOfWeekRu
    {

        Воскресенье = 0,

        Понедельник = 1,

        Вторник = 2,

        Среда = 3,

        Четверг = 4,

        Пятница = 5,

        Суббота = 6
    }
    public enum DayOfWeekRuSmall
    {

        ВС = 0,

        ПН = 1,

        ВТ = 2,

        СР = 3,

        ЧТ = 4,

        ПТ = 5,

        СБ = 6
    }
    public static class DateTimeRuExtension
    {
        public static DayOfWeekRu DayOfWeekGetRus(this DayOfWeek today)
        {
            return today switch
            {
                DayOfWeek.Sunday => DayOfWeekRu.Воскресенье,
                DayOfWeek.Monday => DayOfWeekRu.Понедельник,
                DayOfWeek.Tuesday => DayOfWeekRu.Вторник,
                DayOfWeek.Wednesday => DayOfWeekRu.Среда,
                DayOfWeek.Thursday => DayOfWeekRu.Четверг,
                DayOfWeek.Friday => DayOfWeekRu.Пятница,
                DayOfWeek.Saturday => DayOfWeekRu.Суббота,
                _ => DayOfWeekRu.Воскресенье,
            };
        }
        public static DayOfWeekRuSmall DayOfWeekGetRusSmall(this DayOfWeek today)
        {
            return today switch
            {
                DayOfWeek.Sunday => DayOfWeekRuSmall.ВС,
                DayOfWeek.Monday => DayOfWeekRuSmall.ПН,
                DayOfWeek.Tuesday => DayOfWeekRuSmall.ВТ,
                DayOfWeek.Wednesday => DayOfWeekRuSmall.СР,
                DayOfWeek.Thursday => DayOfWeekRuSmall.ЧТ,
                DayOfWeek.Friday => DayOfWeekRuSmall.ПТ,
                DayOfWeek.Saturday => DayOfWeekRuSmall.СБ,
                _ => DayOfWeekRuSmall.ВС,
            };

        }

        public static string DayOfWeekRuSmallToFull(string day)
        {
            return day switch
            {
                "ПН" => "Понедельник",
                "ВТ" => "Вторник",
                "СР" => "Среда",
                "ЧТ" => "Четверг",
                "ПТ" => "Пятница",
                "СБ" => "Суббота",
                "ВС" => "Воскресенье",
                _ => ""
            };
        }
    }
}
