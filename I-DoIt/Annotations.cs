using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace u_doit.I_DoIt
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class CategoryIdAttribute : Attribute
    {
        public string Value { get; private set; }

        public CategoryIdAttribute(string id)
        {
            this.Value = id;
        }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class CategoryDisplayNameAttribute : Attribute
    {
        public string Value { get; private set; }

        public CategoryDisplayNameAttribute(string displayName)
        {
            this.Value = displayName;
        }
    }

    sealed class DialogNameAttribute : Attribute
    {
        public string Value { get; private set; }

        public DialogNameAttribute(string displayName)
        {
            this.Value = displayName;
        }
    }
    //Um Felder im JSON umzubennen, ist das Attribut JsonProperty(PropertyName = "whatever")] zu benutzen.
}
