using System;
using System.Collections.Generic;
using System.Text;

namespace u_doit.I_DoIt
{
    class ConstantsResponse
    {
        public Dictionary<string, string> objectTypes;
        public ConstantsCategories categories;

        public string GetConstant(string objName)
        {
            foreach (var kv in objectTypes)
            {
                if (kv.Value.Equals(objName))
                {
                    return kv.Key;
                }
            }
            throw new ArgumentException(string.Format("Der Objekt-Typ {0} ist nicht vorhanden!", objName));
        }
    }

    class ConstantsCategories
    {
        public Dictionary<string, string> g;
        public Dictionary<string, string> s;
    }
}
