using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnBot
{
    public class Meeting
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("location_id")]
        public int LocationId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("day")]
        public DayOfWeekRuSmall Day { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("end_time")]
        public string EndTime { get; set; }

        [JsonProperty("time_formatted")]
        public string TimeFormatted { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("location_url")]
        public string LocationUrl { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("region_id")]
        public int RegionId { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("sub_region")]
        public string SubRegion { get; set; }

        [JsonProperty("website")]
        public Uri Website { get; set; }

        [JsonProperty("conference_url")]
        public string ConferenceUrl { get; set; }

        [JsonProperty("conference_url_notes")]
        public string ConferenceUrlNotes { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }
   
}
