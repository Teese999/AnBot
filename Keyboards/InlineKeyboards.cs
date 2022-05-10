using AnBot.Models;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace AnBot
{
    public static class InlineKeyboards
    {
        public static List<string> Regions = new();
        static public InlineKeyboardMarkup MeetingsKeyboard { get; private set; }
        static public InlineKeyboardMarkup MainMenuKeyboard { get; private set; }
        static public InlineKeyboardMarkup CitiesKeyboard { get; private set; }
        static public InlineKeyboardMarkup MainCityKeyboard { get; private set; }
        static public InlineKeyboardMarkup AnotherCityKeyboard { get; private set; }

        static public void KeyboardsActivate()
        {
            CreateCitiesInlineKeyboard(); 
            CreateMainMenuKeyboard();
            CreateMainCityMeetingsKeyboard(); 
            CreateAnotherSityMeetingsKeyboard();
        }
        private static void CreateCitiesInlineKeyboard()
        {
            Regions.Clear();
            Regions.AddRange(MeetingsRepository.MeetingsListAll.Where(x => x.SubRegion != null).Select(x => x.SubRegion).Distinct().ToList());
            var CityArray = new List<IEnumerable<InlineKeyboardButton>>();


            for (int i = 0; i < Regions.Count; i++)
            {
                CityArray.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData(Regions[i], Regions[i]) });

            }
            CitiesKeyboard = new(CityArray);
            Console.WriteLine("Cities keyboard activaded");
        }
        private static void CreateMainMenuKeyboard()
        {

            var mainMenu = new List<IEnumerable<InlineKeyboardButton>>();
            mainMenu.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Собрания в Вашем городе", "MyCity") });
            mainMenu.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("В других городах", "AnotherCity") });
            if (Settings.EnableOpenMeetings == true)
            {
                mainMenu.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Открытые собрания", "OpenMeetings") });
            }
            mainMenu.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Связаться с Инфолинией", "InfolineCall") });
            mainMenu.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Ежедневник", "Diary") });
            MainMenuKeyboard = new(mainMenu);
            Console.WriteLine("MainMenu keyboard activaded");
        }
        private static void CreateMainCityMeetingsKeyboard()
        {

            var meetings = new List<IEnumerable<InlineKeyboardButton>>();
            meetings.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Сегодня", "Today"), InlineKeyboardButton.WithCallbackData("Завтра", "Tomorrow"), InlineKeyboardButton.WithCallbackData("Послезавтра", "AfterTomorrow") });
            meetings.Add(new List<InlineKeyboardButton>() {
                InlineKeyboardButton.WithCallbackData("ПН", "ПН"),
                InlineKeyboardButton.WithCallbackData("ВТ", "ВТ"),
                InlineKeyboardButton.WithCallbackData("СР", "СР"),
                InlineKeyboardButton.WithCallbackData("ЧТ", "ЧТ"),
                InlineKeyboardButton.WithCallbackData("ПТ", "ПТ"),
                InlineKeyboardButton.WithCallbackData("СБ", "СБ"),
                InlineKeyboardButton.WithCallbackData("ВС", "ВС")
            });

            meetings.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("В других городах", "AnotherCity") });

            MainCityKeyboard = new(meetings);
            Console.WriteLine("MainCity keyboard activaded");
        }
        private static void CreateAnotherSityMeetingsKeyboard()
        {

            var meetings = new List<IEnumerable<InlineKeyboardButton>>();
            meetings.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Сегодня", "Today"), InlineKeyboardButton.WithCallbackData("Завтра", "Tomorrow"), InlineKeyboardButton.WithCallbackData("Послезавтра", "AfterTomorrow") });
            meetings.Add(new List<InlineKeyboardButton>() {
                InlineKeyboardButton.WithCallbackData("ПН", "ПН"),
                InlineKeyboardButton.WithCallbackData("ВТ", "ВТ"),
                InlineKeyboardButton.WithCallbackData("СР", "СР"),
                InlineKeyboardButton.WithCallbackData("ЧТ", "ЧТ"),
                InlineKeyboardButton.WithCallbackData("ПТ", "ПТ"),
                InlineKeyboardButton.WithCallbackData("СБ", "СБ"),
                InlineKeyboardButton.WithCallbackData("ВС", "ВС")
            });
            meetings.Add(new List<InlineKeyboardButton>() { InlineKeyboardButton.WithCallbackData("Собрания в Вашем городе", "MyCity") });

            AnotherCityKeyboard = new(meetings);
            Console.WriteLine("AnotherCity keyboard activaded");
        }
    }
}
