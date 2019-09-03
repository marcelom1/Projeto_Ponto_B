using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PontoB.Models
{
    public class CaptchaReponse
    {
        [JsonProperty]
        public bool Success { get; set; }
        [JsonProperty]
        public List<string> ErroMessege { get; set; }

    }
}