using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace AnBot.Messager
{
    public class RegisterBarrier
    {
        public RegisterBarrier(Message message)
        {
            Message = message;
            UserCheck();
            
        }
        public RegisterBarrier(CallbackQuery callback)
        {
            Message = callback.Message;
            CallBack = callback;
            UserCheck();

        }
        public async Task StartMessage()
        {
            await OnClick();
            if (User.MenuState == null)
            {
                await new CommonMessanger(Message).Answer();
            }
            else
            {
                await new ContextMessanger(Message).Answer();
            }
        }
        public async Task StartCallback()
        {
            await OnClick();
            if (User.MenuState == null)
            {
                await new CallbackQueryMessanger(CallBack).Answer();
            }
            else
            {
                await new ContextMessanger(CallBack).Answer();
            }
        }
        public Message Message { get; set; }
        public User User { get; set; }
        public CallbackQuery CallBack { get; set; }
        public BotDataContext Context { get; set; } = new();
        private void UserCheck()
        {
            User = Context.Set<User>().FirstOrDefault(x => x.UserId == Message.Chat.Id);
            if (User == null)
            {
                Context.Set<User>().Add(User.CreateUser(Message));
                Context.SaveChanges();
                User = Context.Set<User>().FirstOrDefault(x => x.UserId == Message.Chat.Id);
            }

        }
        private async static Task OnClick()
        {

            using (BotDataContext Context = new())
            {
                var clicDate = Context.Set<Click>().FirstOrDefault(x => x.Date == DateTime.Today);
                if (clicDate != null)
                {
                    clicDate.Count++;
                }
                else
                {
                    await Context.Set<Click>().AddAsync(new Click(1));
                }
                await Context.SaveChangesAsync();
            }

        }

    }
}
