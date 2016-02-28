using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt.Objects.Categories
{
    [CategoryDisplayName("Share")]
    [CategoryId("C__CATG__SHARES")]
    class Share : Category
    {
        public string title;
        public string unc_path;

        [JsonConverter(typeof(EnumDeserializer))]
        public int volume;

        public string path;
        public string text_area;

        public override bool Equals(object obj)
        {
            Share other = (obj as Share);
            if (other == null) return false;

            return other.title == this.title;
        }
    }

}
