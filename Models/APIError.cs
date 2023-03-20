using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpShowCase.Models
{
    public class APIError
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        public APIError(string error)
        {
            Error = error;
        }

        public APIError()
        {
        }
    }
}
