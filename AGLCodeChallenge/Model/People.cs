using System.Collections.Generic;
//using System.Reflection.Metadata;
using System.Text;
using Newtonsoft.Json;

namespace AGLCodeChallenge.Model
{

    public class People
    {
       
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("pets")]
        public List<Pet> Pets { get; set; }
    }
}
