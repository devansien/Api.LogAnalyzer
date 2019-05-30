using Newtonsoft.Json;
using System;

namespace Ansien.Api.LogAnalyzer
{
    public class FineWineLog
    {
        [JsonProperty("query_id")]
        public string QueryId { get; set; }

        [JsonProperty("customer_id")]
        public int CustomerId { get; set; }

        [JsonProperty("natural_query")]
        public string[] NaturalLangQuery { get; set; }

        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("result_count")]
        public int ResultCount { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
