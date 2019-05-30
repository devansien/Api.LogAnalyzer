using Newtonsoft.Json;

namespace Ansien.Api.LogAnalyzer
{
    public class ResponseWrapper
    {
        [JsonProperty("top_user")]
        public string TopUser { get; set; }

        [JsonProperty("top_user_counter")]
        public string TopUserCounter { get; set; }

        [JsonProperty("top_category")]
        public string TopCategory { get; set; }

        [JsonProperty("top_category_counter")]
        public string TopCategoryCounter { get; set; }
    }
}
