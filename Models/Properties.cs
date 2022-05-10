using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AnBot
{
    [JsonObject]
    public class Properties
    {

        [JsonProperty]
        public string BotToken { get; private set; }
        [JsonProperty]
        public Uri MeetingsUrl { get; private set; }
        [JsonProperty]
        public string BdConnectionString { get; private set; }

        [JsonProperty]
        public string SubRegion { get; private set; }
        public static Properties Download()
        {
            using StreamReader file = File.OpenText(@"Properties.json");
            JsonSerializer serializer = new();
            Properties props = (Properties)serializer.Deserialize(file, typeof(Properties));          
            return props;
        }
        

    }
}
