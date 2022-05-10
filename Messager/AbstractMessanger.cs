using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AnBot.Messager
{
    public abstract class AbstractMessanger
    {
        public Message Message { get; set; }
        public User User { get; set; }
        public CallbackQuery CallBack { get; set; }
        public BotDataContext Context { get; set; } = new();

        protected AbstractMessanger(Message message)
        {
            Message = message;
            User = Context.Set<User>().FirstOrDefault(x => x.UserId == Message.Chat.Id);
        }
        protected AbstractMessanger(CallbackQuery callback)
        {
            Message = callback.Message;
            CallBack = callback;
            User = Context.Set<User>().FirstOrDefault(x => x.UserId == Message.Chat.Id);
        }
        public async Task OnClick()
        {
            User.Clicks++;
            await Context.SaveChangesAsync();
        }
    }
}
