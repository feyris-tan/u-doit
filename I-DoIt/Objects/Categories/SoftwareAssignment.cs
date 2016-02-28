using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt.Objects.Categories
{
    [CategoryId("C__CATG__APPLICATION")]
    [CategoryDisplayName("Software Assignment")]
    class SoftwareAssignment : Category
    {
        public SoftwareAssignment()
        {
        }

        public SoftwareAssignment(int i)
        {
            application = i;
        }
        [DialogName("isys_connection__id")]                
        
        [JsonConverter(typeof(EnumDeserializer))]
        public int application;

        [DialogName("isys_cats_lic_list__id")]
        [JsonConverter(typeof(EnumDeserializer))]
        public int assigned_license;
        
        [DialogName("isys_cats_database_access_list__id")]
        [JsonIgnore]
        public int assigned_database_schema;                //Interessiert uns nicht, nutzen wir nicht...
        
        [DialogName("isys_catg_its_components_list__id")]
        [JsonConverter(typeof(EnumDeserializer))]
        public int assigned_it_service;
        
        [DialogName("isys_cats_app_variant_list__id")]
        [JsonConverter(typeof(EnumDeserializer))]
        public int assigned_variant;
        
        string description;

        public override bool Equals(object obj)
        {
            SoftwareAssignment other = (obj as SoftwareAssignment);
            if (other == null) return false;

            return this.application == other.application;
        }

        public override bool IsUpdateNeeded(Category other)
        {
            //Software Assignments nicht per Update-Funktion nachbearbeiten, da andernfalls Lizenz und Vertragsinfos verloren gehen können.
            return false;   
        }

        public override string ToString()
        {
            return string.Format("Assignment of {0}", application);
        }
    }
}
