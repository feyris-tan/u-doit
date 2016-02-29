using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt
{
    class ObjectType
    {
        public long id;
        public string title;
        public long container;
        [JsonProperty(PropertyName = "const")] public string constant;
        public string color;
        public string image;
        public string icon;
        public long cats;
        public long treeGroup;
        public long status;
        public long type_group;
        public string type_group_title;
    }

    class ObjectTypesResponse : List<ObjectType>
    {
        public string GetConstantFromTitle(string title)
        {
            foreach (ObjectType ot in this)
            {
                if (ot.title.Equals(title))
                    return ot.constant;
            }
            throw new ArgumentException(string.Format("Der Objekt-Typ {0} wurde nicht gefunden.", title));
        }

        public ObjectType GetObjectTypeFromTitle(string name)
        {
            foreach (ObjectType ot in this)
            {
                if (ot.title.Equals(name))
                    return ot;
            }
            throw new ArgumentException(string.Format("Der Objekt-Typ {0} wurde nicht gefunden.", name));
        }
    }
}
