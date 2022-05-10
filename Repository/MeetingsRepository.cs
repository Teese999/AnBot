using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using AngleSharp;
using AnBot.Models;

namespace AnBot
{
    public static class MeetingsRepository
    {

        static public List<Meeting> MeetingsListAll { get; private set; } = new();
        static public string Diary { get; private set; }
        static public string AboutBot { get; private set; }
        static public List<string> DaysShort { get; private set; } = new List<string>() { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС", "Today", "Tomorrow", "AfterTomorrow" };
        private static void RepositiryUpdate()
        {

            WebClient wc = new(); //New webclient

            string meetingsstring = wc.DownloadString(Settings.MeetingsUrl); //Download meetings from API

            MeetingsListAll = JsonConvert.DeserializeObject<List<Meeting>>(meetingsstring); //Convert meetings from JSON to list

            Console.WriteLine("Repository updated");
        }


        static public void UpdateAll()
        {
            DiaryUpdateAsync();
            RepositiryUpdate();
            AboutBotUpdate();
        }
        private static async void DiaryUpdateAsync()
        {
            Diary = null;
            using var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

            using var doc = await context.OpenAsync("https://na-russia.org/eg");
            Diary += doc.QuerySelector("span.dt_date").TextContent + "\t";
            Diary += doc.QuerySelector("span.dt_title").TextContent;
            Diary += "\n" + "\n";
            Diary += doc.QuerySelector("div.dt_quote").TextContent + "\n";
            Diary += doc.QuerySelector("div.dt_quote_from").TextContent;
            Diary += "\n" + "\n";
            Diary += doc.QuerySelector("div.dt_body").TextContent;
            Diary += "\n" + "\n";
            Diary += doc.QuerySelector("div.dt_footer").TextContent;
            Diary += "\n" + "\n";
            Diary += "Взято с сайта https://na-russia.org";

            Console.WriteLine("Diary updated");
        }
        private static void AboutBotUpdate()
        {
            AboutBot =
                "Бот сделан для сообщества Анонимные Наркоманы. \n" +
                "Исходный код бота полностью открыт и может быть использован любым желающим \n" +
                "Исходники лежат тут https://github.com/Teese999/AnBot \n" +
                "Все вопросы по боту, предложения, помощь в настройке, идеи нового функционала просьба писать сюда \n" +
                "https://t.me/Teese";
            Console.WriteLine("AboutBot updated");
        }

    }


}
