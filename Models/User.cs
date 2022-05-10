using AnBot.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace AnBot
{
    public class User
    {
        [Key]
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsAdmin { get; set; }
        /// <summary>
        /// true - MyCity, false - AnotherCity
        /// </summary>
        public bool? CityChoise { get; set; }
        public DateTime FollowDate { get; set; }
        public string MenuState { get; set; }
        public uint Clicks { get; set; }
        /// To next versions
        public DateTime CleanFrom { get; set; }
        public bool DiarySubscription { get; set; }
        public bool NewsSubscription { get; set; }
        public bool MeetingsSubscription { get; set; }

        public static User CreateUser(Message message)
        {
            return new User()
            {
                UserId = message.Chat.Id,
                Nickname = message.Chat.Username,
                FirstName = message.Chat.FirstName,
                LastName = message.Chat.LastName,
                City = InlineKeyboards.Regions[0],
                FollowDate = CurrentTime.GetCurrentTime(),
                LastActivity = CurrentTime.GetCurrentTime(),
                IsAdmin = false,
                CleanFrom = default,
                DiarySubscription = default,
                NewsSubscription = default,
                MeetingsSubscription = default,
                CityChoise = null,
                Clicks = 1
            };
        }
    }
}
