using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt
{
    class EnumInfo
    {
        public int id;
        public string @const;
        public string title;
        public string title_lang;

        public override string ToString()
        {
            return title;
        }
    }

    class IdoitEnumerator : List<EnumInfo>
    {
        
        [JsonIgnore] public string categoryConstant;
        [JsonIgnore] public string property;

        public bool TestTitle(string value)
        {
            foreach (EnumInfo item in this)
            {
                if (item.title.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public int FindTitleLike(string s)
        {
            EnumInfo id = this.Find(x => (x.title.ToLowerInvariant().Contains(s.ToLowerInvariant())));
            
            if (id == null)
            {
                throw new Exception("Dieser Enumerator enthält keinen Titel, der " + s + " ähnelt.");
            }

            return id.id;
        }
    }
}
