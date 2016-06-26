using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt
{
    class DialogFieldInfo
    {
        public string title;
        public FieldInfoDetails info;
        public FieldInfoData data;
        public FieldInfoUi ui;
        public FieldInfoFormat format;
        public FieldInfoCheck check;

        public bool IsEnum
        {
            get
            {
                return ((ui.type == "dialog" || (ui.type == "popup")) &&
                        (data.type == "int" || (data.type == "text")));
            }
        }

        public bool IsFloat
        {
            get { return ((data.type == "float")); }
        }
    }

    class FieldInfoDetails
    {
        public string primary_field;
        public string type;
        public bool backward;
        public string title;
        public string description;
    }

    class FieldInfoData
    {
        public string type;
        public string field;
    }

    class FieldInfoUi
    {
        public string type;
        [JsonProperty(PropertyName = "params")] public object parameters;
        [JsonProperty(PropertyName = "default")] public object defaultValue;
        public string id;
    }

    class FieldInfoFormat
    {
        public object callback;
    }

    class FieldInfoCheck
    {
        public object mandatory;
    }
}
