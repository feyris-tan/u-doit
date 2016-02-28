using System;
using System.Collections.Generic;
using System.Text;
using u_doit.JsonRpc;

namespace u_doit
{
    internal class Config
    {
        private Config()
        {
            RNG = new Random();
        }

        private static Config instance;

        public static Config GetInstance()
        {
            if (instance == null)
            {
                instance = new Config();
            }
            return instance;
        }

        public Random RNG{ get; private set; }

        public string ApiKey { internal get; set; }

        public const bool LogAllJson = false;

        public static readonly string[] cpuManufactors = new string[] { "Intel", "AMD", "Cyrix", "VIA", "Transmeta", "SiS", "UMC" };
        public static readonly string[] cpuModels = new string[] {"Core i5", "Core i3"};
        public static readonly KeyValuePair<string,string>[] ramManufactors = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("99","Mushkin"),
            new KeyValuePair<string, string>("AD","ADATA"), 
            new KeyValuePair<string, string>("AM","ADATA"), 
            new KeyValuePair<string, string>("AX","ADATA"), 
            new KeyValuePair<string, string>("BL","Crucial"),
            new KeyValuePair<string, string>("CM","Corsair"), 
            new KeyValuePair<string, string>("CT","Crucial"),
            new KeyValuePair<string, string>("DIM","PNY"),
            new KeyValuePair<string, string>("F2","G.Skill"),
            new KeyValuePair<string, string>("F3","G.Skill"),
            new KeyValuePair<string, string>("F4","G.Skill"),
            new KeyValuePair<string, string>("FA","G.Skill"),
            new KeyValuePair<string, string>("HX","Kingston"),
            new KeyValuePair<string, string>("JM","Transcend"),
            new KeyValuePair<string, string>("K-DIM","PNY"),
            new KeyValuePair<string, string>("KCP","Kingston"),
            new KeyValuePair<string, string>("KHX","Kingston"),
            new KeyValuePair<string, string>("KVR","Kingston"),
			new KeyValuePair<string, string>("NT", "Nanya"),			//Medion PCs
            new KeyValuePair<string, string>("PSA","Patriot/Apple"),
            new KeyValuePair<string, string>("PSD","Patriot"), 
            new KeyValuePair<string, string>("PV","Patriot"),
            new KeyValuePair<string, string>("PX","Patriot"),
            new KeyValuePair<string, string>("MD","PNY"),
            new KeyValuePair<string, string>("M3","Samsung"),
            new KeyValuePair<string, string>("SU","ADATA"),
            new KeyValuePair<string, string>("TS","Transcend"),
            new KeyValuePair<string, string>("TWIN2","Corsair"),
            new KeyValuePair<string, string>("VS","Corsair"),
            new KeyValuePair<string, string>("","Undefined"),   //muss immer als letzter stehen!
        };
    }
}
