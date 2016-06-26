using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt.Objects.Categories
{
    [CategoryDisplayName("Global")]
    [CategoryId("C__CATG__GLOBAL")]
    class Global : Category
    {
        public string title;
        public string created;
        public string changed;
        [JsonConverter(typeof(EnumDeserializer))] public int purpose;
        [JsonConverter(typeof(EnumDeserializer))] public int category;
        public string sysid;
        [JsonConverter(typeof (EnumDeserializer))] public int cmdb_status;
        [JsonConverter(typeof (EnumDeserializer))] public int type;
        public string description;
    }
}
