using AnBot.Models;
using AnBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace AnBot.Messager
{
    public class CallbackQueryMessanger : AbstractMessanger
    {

        public CallbackQueryMessanger(CallbackQuery callBack) : base(callBack) { }
        public async Task Answer()
        {
            await OnClick();
            if (InlineKeyboards.Regions.Any(x => x == CallBack.Data))
            {
                await CityChange(CallBack.Data);
            }
            else if (MeetingsRepository.DaysShort.Any(x => x == CallBack.Data))
            {
                if (CallBack.Data == "Today" || CallBack.Data == "Tomorrow" || CallBack.Data == "AfterTomorrow")
                {
                    switch (CallBack.Data)
                    {
                        case "Today":
                            var dateToday = CurrentTime.GetCurrentTime().DayOfWeek.DayOfWeekGetRusSmall();
                            await SendMeetingsList(dateToday.ToString());
                            break;
                        case "Tomorrow":
                            var dateTomorrow = CurrentTime.GetCurrentTime().AddDays(1).DayOfWeek.DayOfWeekGetRusSmall();
                            await SendMeetingsList(dateTomorrow.ToString());
                            break;
                        case "AfterTomorrow":
                            var dateAfterTomorrow = CurrentTime.GetCurrentTime().AddDays(2).DayOfWeek.DayOfWeekGetRusSmall();
                            await SendMeetingsList(dateAfterTomorrow.ToString());
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    await SendMeetingsList(CallBack.Data);
                }

            }
            else
            {
                switch (CallBack.Data)
                {
                    case "Diary":
                        await SendMessage(MeetingsRepository.Diary, InlineKeyboards.MainMenuKeyboard);
                        break;
                    case "MainMenu":
                        await SendMessage("Главное меню", InlineKeyboards.MainMenuKeyboard);
                        break;
                    case "MyCity":
                        User.CityChoise = true;
                        await SendMessage(User.City, InlineKeyboards.MainCityKeyboard);
                        await Context.SaveChangesAsync();
                        break;
                    case "AnotherCity":
                        User.CityChoise = false;
                        await SendMessage("Расписание в других городах", InlineKeyboards.AnotherCityKeyboard);
                        await Context.SaveChangesAsync();
                        break;
                    case "OpenMeetings":
                        if (Settings.EnableOpenMeetings == false) { break; }
                        User.CityChoise = false;
                        await SendMessagePhoto("Открытые собрания", InlineKeyboards.MainMenuKeyboard);
                        await Context.SaveChangesAsync();
                        break;
                    case "InfolineCall":
                        await SendMessage("+79507205050", InlineKeyboards.MainMenuKeyboard);
                        break;
                    default:
                        break;
                }

            }
        }

        private async Task SendMeetingsList(string day)
        {
            List<Meeting> meetinglist;
            string answer = DateTimeRuExtension.DayOfWeekRuSmallToFull(day);
            answer += "\n";
            switch (User.CityChoise)
            {

                case true:
                    meetinglist = MeetingsRepository.MeetingsListAll.Where(x => (x.Day.ToString() == day) && (x.SubRegion == User.City)).OrderBy(x => x.Time).ToList();
                    if (meetinglist == null || meetinglist.Count == 0)
                    {
                        await SendMessage("На этот день собраний нет", InlineKeyboards.MainCityKeyboard);
                        break;
                    }
                    answer += MeetingsToString(meetinglist);
                    await SendMessage(answer, InlineKeyboards.MainCityKeyboard, 2);
                    break;
                case false:
                    meetinglist = MeetingsRepository.MeetingsListAll.Where(x => (x.Day.ToString() == day) && (x.SubRegion != User.City)).OrderBy(x => x.Time).ToList();
                    if (meetinglist == null || meetinglist.Count == 0)
                    {
                        await SendMessage("На этот день собраний нет", InlineKeyboards.AnotherCityKeyboard);
                        break;
                    }
                    answer += MeetingsToString(meetinglist);
                    await SendMessage(answer, InlineKeyboards.AnotherCityKeyboard, 2);
                    break;
                default:
                    break;
            }
        }

        private string MeetingsToString(List<Meeting> meetinglist)
        {
            string answer = null;

            foreach (var item in meetinglist)
            {
                answer += $"<a href='{item.Url}'>{item.Name}</a>" + "\t";
                answer += $"с {item.Time} до {item.EndTime}" + "\n";
                answer += $"<a href='https://yandex.ru/maps/39/rostov-na-donu/?ll={item.Longitude}%2C{item.Latitude}&mode=search&sll={item.Longitude}%2C{item.Latitude}&text={item.Latitude}%2C{item.Longitude}&z=16'>{item.FormattedAddress}</a>";
                answer += "\n" + "\n";
            }

            return answer;
        }

        private async Task CityChange(string city)
        {
            User.City = CallBack.Data;
            User.LastActivity = CurrentTime.GetCurrentTime();

            await TelegramBot.Bot.SendTextMessageAsync(
                chatId: CallBack.Message.Chat.Id,
                replyMarkup: InlineKeyboards.MainMenuKeyboard,
                text: $"Ваш город {CallBack.Data}"
                + Environment.NewLine
                + "Для выбора другого города введите команду /city"
            );

            await Context.SaveChangesAsync();
        }
        private async Task SendMessage(string text, InlineKeyboardMarkup keyboard = null, int parseMode = 0)
        {
            var parseModeSelect = Telegram.Bot.Types.Enums.ParseMode.Default;
            if (parseMode == 2)
            {
                parseModeSelect = Telegram.Bot.Types.Enums.ParseMode.Html;
            }

            User currentUser = Context.Set<User>().First(x => x.UserId == Message.Chat.Id);
            currentUser.LastActivity = CurrentTime.GetCurrentTime();
            await TelegramBot.Bot.SendTextMessageAsync(
                parseMode: parseModeSelect,
                chatId: CallBack.Message.Chat.Id,
                replyMarkup: keyboard,
                text: text,
                disableWebPagePreview: true
            );
        }
        private async Task SendMessagePhoto(string text, InlineKeyboardMarkup keyboard = null, int parseMode = 0)
        {
            var parseModeSelect = Telegram.Bot.Types.Enums.ParseMode.Default;
            if (parseMode == 2)
            {
                parseModeSelect = Telegram.Bot.Types.Enums.ParseMode.Html;
            }

            User currentUser = Context.Set<User>().First(x => x.UserId == Message.Chat.Id);
            currentUser.LastActivity = CurrentTime.GetCurrentTime();
            await TelegramBot.Bot.SendPhotoAsync(
                parseMode: parseModeSelect,
                chatId: CallBack.Message.Chat.Id,
                replyMarkup: keyboard,
                photo: new InputOnlineFile(new Uri(Settings.OpenMeetingsPicUrl))
            );
        }
    }
}
