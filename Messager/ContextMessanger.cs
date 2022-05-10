using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AnBot.Messager
{
    class ContextMessanger : AbstractMessanger
    {
        public ContextMessanger(Message message) : base(message) { }
        public ContextMessanger(CallbackQuery callback) : base(callback.Message) { }
        public async Task Answer()
        {
            await OnClick();
            User = Context.Set<User>().FirstOrDefault(x => x.UserId == Message.Chat.Id);
            switch (User.MenuState)
            {
                case "Clean_enterDate":
                    await Clean_enterDate();
                    break;
                default:
                    break;
            }
        }
        private async Task Clean_enterDate()
        {
            var cleanDate = new DateTime();
            if (DateTime.TryParse(Message.Text, out cleanDate))
            {
                User.CleanFrom = cleanDate;
                await SendMessage($"Спасибо, теперь мы знаем что Вы чистый с {cleanDate.ToShortDateString()} и сможем поздравить Вас с юбилеем!");
                User.MenuState = null;
                await Context.SaveChangesAsync();
            }
            else
            {
                await SendMessage($"Кажется Вы не корректно ввели данные, попробуйте снова", InlineKeyboards.MainMenuKeyboard);
                User.MenuState = null;
                await Context.SaveChangesAsync();
            }

        }
        private async Task SendMessage(string text, InlineKeyboardMarkup keyboard = null)
        {

            User currentUser = Context.Set<User>().First(x => x.UserId == Message.Chat.Id);
            currentUser.LastActivity = DateTime.Now;
            await TelegramBot.Bot.SendTextMessageAsync(
                chatId: User.UserId,
                replyMarkup: keyboard,
                text: text
            );
        }
    }
    
}
