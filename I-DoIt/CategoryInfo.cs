using Newtonsoft.Json;

namespace u_doit.I_DoIt
{
    class CategoryInfo
    {
        public int id;
        public string title;
        [JsonProperty(PropertyName = "const")] public string constant;
        public int multi_value;
        public string source_table;
    }
}