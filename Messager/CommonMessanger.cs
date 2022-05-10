using AnBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AnBot.Messager
{
    public class CommonMessanger : AbstractMessanger
    {
        public CommonMessanger(Message message) : base(message) { }
        public async Task Answer()
        {
            await OnClick();

            switch (Message.Text.ToLower())
            {
                case "/menu":
                    await SendMessage("Главное меню", InlineKeyboards.MainMenuKeyboard);
                    break;
                case "/city":
                    await SendMessage("Выберете нужный город", InlineKeyboards.CitiesKeyboard);
                    break;
                case "/clean":
                    User.MenuState = "Clean_enterDate";
                    await Context.SaveChangesAsync();
                    await SendMessage(
                        "Введите дату с которой Вы остаетесь чистыми \n" +
                        "Дата должна быть в формате 01.01.1990");
                    break;
                case "/settings":

                    break;
                case "/post":

                    break;
                case "/about":
                    await SendMessage(MeetingsRepository.AboutBot, InlineKeyboards.MainMenuKeyboard);
                    break;
                case "/admin":

                    break;
                default:
                    await SendMessage("Главное меню", InlineKeyboards.CitiesKeyboard);
                    break;
            }


        }
        private async Task SendMessage(string text, InlineKeyboardMarkup keyboard = null)
        {

            User currentUser = Context.Set<User>().First(x => x.UserId == Message.Chat.Id);
            currentUser.LastActivity = CurrentTime.GetCurrentTime();
            await TelegramBot.Bot.SendTextMessageAsync(
                chatId: User.UserId,
                replyMarkup: keyboard,
                text: text,
                disableWebPagePreview: true
            );
        }
    }
}
