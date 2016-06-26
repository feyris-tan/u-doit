using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.JsonRpc
{
    internal class Request
    {
        public Request()
        {
            version = "2.0";
            id = Config.GetInstance().RNG.Next();
        }

        public string version;
        public string method;
        public object @params;
        public int id;

        [JsonIgnore]
        public bool IsNotification
        {
            get { return id != 0; }
            set { 
                id = value ? Config.GetInstance().RNG.Next() : 0;
            }
        }
    }
}
