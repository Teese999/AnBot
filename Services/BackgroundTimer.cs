using AnBot.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AnBot
{
    public class BackgroundTimer
    {
        private readonly Timer _timer = new(300000);
        //private readonly Timer _timer = new(1000);
        private DateTime NextDay = CurrentTime.GetCurrentTime().AddDays(1).Date;
        public event EventHandler NewDay;

        public BackgroundTimer()
        {
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
            NewDay += OnNewDay;
            Console.WriteLine("Timer started");

            Console.WriteLine(CurrentTime.GetCurrentTime() + "\t DATETIME NOW");
            Console.WriteLine(NextDay + "\t DATETIME NEXTDAY");

        }
        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            if (CurrentTime.GetCurrentTime() > NextDay)
            {
                NewDay?.Invoke(this, EventArgs.Empty);
                NextDay = NextDay.AddDays(1);
                Console.WriteLine(CurrentTime.GetCurrentTime() + "\t DATETIME NOW");
                Console.WriteLine(NextDay + "\t DATETIME NEXTDAY");
            }

        }
        public void OnNewDay(object sender, EventArgs e)
        {
            TelegramBot.Bot.StopReceiving();
            Console.WriteLine(CurrentTime.GetCurrentTime() + "-------------------------Updating");
            MeetingsRepository.UpdateAll();
            InlineKeyboards.KeyboardsActivate();
            TelegramBot.Bot.StartReceiving();
        }

    }
}
