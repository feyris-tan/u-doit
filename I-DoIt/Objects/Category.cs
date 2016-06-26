using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt.Objects
{
    abstract class Category
    {
        [JsonIgnore]
        public string Constant
        {
            get
            {
                Type t = this.GetType();
                foreach (var customAttribute in t.GetCustomAttributes(true))
                {
                    CategoryIdAttribute cia = customAttribute as CategoryIdAttribute;
                    if (cia != null)
                    {
                        return cia.Value;
                    }
                }
                throw new Exception(string.Format("No {1} defined for {0}", t.Name, "CategoryId"));
            }
        }

        [JsonIgnore]
        public string DisplayName
        {
            get
            {
                Type t = this.GetType();
                foreach (var customAttribute in t.GetCustomAttributes(true))
                {
                    CategoryDisplayNameAttribute cia = customAttribute as CategoryDisplayNameAttribute;
                    if (cia != null)
                    {
                        return cia.Value;
                    }
                }
                throw new Exception(string.Format("No {1} defined for {0}", t.Name, "DisplayName"));
            }
        }

        /*
         * Mit dem cleveren Zweckentfremden von Attributen lässt sich JSON.NET dazu bringen, Felder zwar zu deserialisieren, aber nicht wieder zu serialisieren.
         * Dies wird in i-doit benötigt, um Objekte zu speichern.
         * 
         * Lösung von: http://stackoverflow.com/questions/11564091/making-a-property-deserialize-but-not-serialize-with-json-net
         */

        [JsonIgnore]
        public int id;
        [JsonIgnore]
        public int objID;

        [JsonProperty("id")]
        public int IdSetter
        {
            set { id = value; }
        }

        [JsonProperty("objID")]
        public int ObjIdSetter
        {
            set { objID = value;  }
        }

        /*
         * Um zu vermeiden, für jede Klasse GetHashCode(), Equals() und ToString() zu implementieren zu müssen, können wir es hier mit Metaprogrammierung lösen:
         */

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(obj, null)) return false;
            Type myType = this.GetType();
            Type otherType = obj.GetType();
            if (myType != otherType) return false;

            FieldInfo[] fi = myType.GetFields();
            foreach (FieldInfo field in fi)
            {
                if (field.Name.ToLowerInvariant().Equals("title"))
                {
                    object myName = field.GetValue(this);
                    object otherName = field.GetValue(obj);
                    return myName.Equals(otherName);
                }
            }

            throw new NotSupportedException(string.Format("Object Type {0} does not have a title to compare!",
                myType.Name));
        }

        public override int GetHashCode()
        {
            int result = 0;

            foreach (FieldInfo fi in this.GetType().GetFields())
            {
                result = (result*397) ^ fi.GetValue(this).GetHashCode();
            }

            return result;
        }

        public override string ToString()
        {
            foreach (FieldInfo field in this.GetType().GetFields())
            {
                if (field.Name.ToLowerInvariant().Equals("title"))
                {
                    return field.GetValue(this).ToString();
                }
            }

            return string.Format("--> {0} <--", this.GetType().Name);
        }

        public virtual bool IsUpdateNeeded(Category other)
        {
            Type myType = this.GetType();
            Type otherType = other.GetType();
            if (myType != otherType)
                throw new ArgumentException(string.Format("Can't compare a {0} with a {1}", myType.Name, otherType.Name));

            FieldInfo[] fields = myType.GetFields();
            foreach (FieldInfo fi in fields)
            {
                object[] attribs = fi.GetCustomAttributes(true);
                bool ignoreMe = false;
                foreach (object o in attribs)
                {
                    if (o is JsonIgnoreAttribute)
                    {
                        ignoreMe = true;
                        break;
                    }
                }
                if (ignoreMe) continue;

                object valA = fi.GetValue(this);
                object valB = fi.GetValue(other);
                if ((valA == null) && (valB.Equals(""))) return false;
                if ((valB == null) && (valA.Equals(""))) return false;
                if (!valA.Equals(valB)) return true;
            }
            return false;
        }
    }

}
