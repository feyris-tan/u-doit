using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt
{
    class LoginResponse
    {
        public int result;
        public int userid;
        public string name;
        public string mail;
        public string username;
        [JsonProperty(PropertyName = "session-id")]
        public string session_id;
        [JsonProperty(PropertyName = "client-id")]
        public long client_id;
        [JsonProperty(PropertyName = "client-name")]
        public string client_name;
    }
}
