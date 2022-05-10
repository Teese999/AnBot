using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AnBot
{
    public static class Program
    {
        public static async Task Main()
        {

            await TelegramBot.BotStart();

        }
        
    }
}
