using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using ProjectJson;

namespace Artstation
{
    public class Info
    {
        [JsonProperty("permalink")]
        public string Permalink { get; set; }
        
        [JsonProperty("cover_asset_id")]
        public string CoverId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("asset_count")]
        public int AssetCount { get; set; }

        public string linkToProjectJson = "";

        // https://www.artstation.com/projects/9mGgeO.json
        // https://www.artstation.com/artwork/9mGgeO

        public string GetProjectJson(string permalink)
        {
            string currentUrl = permalink.Replace("artwork", "projects");
            currentUrl += ".json";

            return currentUrl;
        }
    
    }

    public class ArtstationSorted
    {
        [JsonProperty("data")]
        public Info[] Data { get; set; }
    }

    class Backend
    {
        private int page = 0;
        private string url = "";
        private string folderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/Artstation";
        WebClient webClientDownload = new WebClient();
        
        public async Task GetJson()
        {
            if (!Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            for (; page <= 200; page++)
            {
                url = $"https://www.artstation.com/projects.json?page={page}&sorting=community";
                ArtstationSorted data = JsonConvert.DeserializeObject<ArtstationSorted>(await Program.client.GetStringAsync(url));

                
                for (int i = 0; i < data.Data.Length; i++)
                {
                    string temporaryUrl = data.Data[i].GetProjectJson(data.Data[i].Permalink);
                    ProjectAssets projectAssets = JsonConvert.DeserializeObject<ProjectAssets>(await Program.client.GetStringAsync(temporaryUrl));

                    for (int j = 0; j < projectAssets.Data.Length; j ++)
                        webClientDownload.DownloadFile(projectAssets.Data[j].ImageUrl, $"{folderPath}/{data.Data[i].Title}{j}.png");
                }
            }
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
