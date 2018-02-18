using System.Collections.Generic;
//using System.Reflection.Metadata;
using System.Text;
using Newtonsoft.Json;
namespace AGLCodeChallenge.Model
{
    public class Pet
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
