using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Artstation
{
    public class Info
    {
        
    }

    public class ArtstationSorted
    {
        [JsonProperty("data")]
        public List<Info> Array { get; set; }
    }

    class Backend
    {
        private readonly string url = "https://www.artstation.com/projects.json?sort_by=community";
        
        public async Task GetJson()
        {
            JObject jsonObject = new JObject(JObject.Parse(await Program.client.GetStringAsync(url)));

            Console.WriteLine(jsonObject.ToString());
        }
    }

    class Program
    {
        public static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Backend obj = new Backend();
            await obj.GetJson();
        }
    }
}
