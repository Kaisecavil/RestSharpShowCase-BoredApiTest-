using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpShowCase.Models
{
    public class Activity
    {
        [JsonProperty("activity")]
        public string ActivityDescription { get; set; }
        [JsonProperty("accessibility")]
        public double Accessibility { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("participants")]
        public int Participants { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("key")]
        public long Key { get; set; }

        public Activity(string activity, double accessibility, string type, int participants, double price, string link, long key)
        {
            ActivityDescription = activity;
            Accessibility = accessibility;
            Type = type;
            Participants = participants;
            Price = price;
            Link = link;
            Key = key;
            
        }

        public Activity()
        {
        }
    }
}
