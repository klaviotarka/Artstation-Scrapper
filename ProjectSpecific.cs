using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;

namespace ProjectJson
{
    public class ProjectInfo
    {
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("asset_type")]
        public string AssetType { get; set; }
    }
    public class ProjectAssets
    {
        [JsonProperty("assets")]
        public ProjectInfo[] Data { get; set; }
    }
}