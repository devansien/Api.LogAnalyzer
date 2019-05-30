using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ansien.Api.LogAnalyzer
{
    public class FineWineCategory
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("synonyms")]
        public HashSet<string> Synonyms { get; set; } = new HashSet<string>();
    }
}
