using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using AnBot.Messager;
using System.Threading;
using AnBot.Services;
using Microsoft.Extensions.Configuration;
using AnBot.Models;

namespace AnBot
{

    static public class TelegramBot
    {
        public static TelegramBotClient Bot;
        public static async Task BotStart()
        {
            await Settings.Load();
            MeetingsRepository.UpdateAll();
            InlineKeyboards.KeyboardsActivate();
            _ = new BackgroundTimer();
            

            Bot = new TelegramBotClient(Settings.Token);

            var me = await Bot.GetMeAsync();

            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.StartReceiving();
            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();
            Bot.StopReceiving();
        }
        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (message == null || message.Type != MessageType.Text)
                return;

            await new RegisterBarrier(message).StartMessage();

            Console.WriteLine($"{CurrentTime.GetCurrentTime().ToShortTimeString()} {message.Chat.FirstName}, {message.Chat.Username}, {message.Text}");


        }
        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            await new RegisterBarrier(callbackQueryEventArgs.CallbackQuery).StartCallback();
            Console.WriteLine($"{CurrentTime.GetCurrentTime().ToShortTimeString()} {callbackQueryEventArgs.CallbackQuery.From.FirstName}, {callbackQueryEventArgs.CallbackQuery.From.Username}, {callbackQueryEventArgs.CallbackQuery.Data}");
        }


    }
}

