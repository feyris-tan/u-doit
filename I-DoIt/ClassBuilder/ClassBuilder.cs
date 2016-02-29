using System;
using System.Collections.Generic;
using System.Text;

namespace u_doit.I_DoIt.ClassBuilder
{
    abstract class ClassBuilder : IDisposable
    {
        protected ClassBuilder(string id,string displayName)
        {
            this.id = id;
            this.displayName = displayName;
        }

        protected string id, displayName;

        protected bool isCategory
        {
            get { return id.StartsWith("C__C"); }
        }

        protected string recommendedClassName
        {
            get
            {
                return
                    displayName.Replace(" ", "")
                        .Replace("/", "")
                        .Replace(".", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("-", "");
            }
        }

        protected bool isFinalized;
        public abstract void Finalize();
        public abstract void AddField(string name, DialogFieldInfo fi);
        public abstract void Dispose();
        public abstract void AddEnum(string name, List<EnumInfo> di);

        protected static string GetDataType(DialogFieldInfo fid)
        {
            if (fid.ui.type == "dialog")
            {
                return fid.ui.id;
            }
            switch (fid.data.type)
            {
                case "int":
                    return "int";
                case "text":
                case "text_area":
                    return "string";
                case "float":
                    return "float";
                case "date_time":
                case "date":
                    return "DateTime";
                case "double":
                    return "double";
                default:
                    Console.WriteLine("Don't know what a {0} is.",fid.data.type);
                    return "object";
            }
        }
    }
}
