using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AnBot.Models
{
    public static class Settings
    {
        public static string Token { get; set; }
        public static string ConnectionString { get; set; }
        public static string DefaultCity { get; set; }
        public static bool EnableOpenMeetings { get; set; }
        public static string OpenMeetingsPicUrl { get; set; }
        public static int UntcTimePlus { get; set; }
        public static string MeetingsUrl { get; set; }
        internal static async Task Load()
        {
            await Task.Run(() => 
            {
                IConfiguration Config = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddEnvironmentVariables()
                           .AddJsonFile("appsettings.json", false, true)
                           .Build();

                Token = Config["Token"];
                ConnectionString = Config["ConnectionString"];
                DefaultCity = Config["DefaultCity"];
                EnableOpenMeetings = Config.GetValue<bool>("EnableOpenMeetings");
                OpenMeetingsPicUrl = Config["OpenMeetingsPicUrl"];
                UntcTimePlus = Config.GetValue<int>("UntcTimePlus");
                MeetingsUrl = Config["MeetingsUrl"];
                Console.WriteLine($"Token = {Token} \n" +
                    $"ConnectionString = {ConnectionString} \n" +
                    $"DefaultCity = {DefaultCity} \n" +
                    $"MeetingsUrl = {MeetingsUrl} \n" +
                    $"EnableOpenMeetings = {EnableOpenMeetings} \n" +
                    $"OpenMeetingsPicUrl = {OpenMeetingsPicUrl} \n" +
                    $"UntcTimePlus = {UntcTimePlus}");
            });          
        }
    }
}
