using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatToSanta.Responses
{
    public class Response
    {
        [JsonProperty(PropertyName ="query")]
        public string Query { get; set; }

        [JsonProperty(PropertyName = "topScoringIntent")]
        public Intent TopScoringIntent { get; set; }
    }
}