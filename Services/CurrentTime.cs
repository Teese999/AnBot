using AnBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnBot.Services
{
    static public class CurrentTime
    {
        public static DateTime GetCurrentTime()
        {
            return DateTime.Now.AddHours(Settings.UntcTimePlus);
        }
    }
}
