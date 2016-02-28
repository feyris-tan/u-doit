using System;
using System.Collections.Generic;
using System.Text;

namespace u_doit
{
    class SoftwareInfo : IComparable
    {
        public string productName;
        public string manufacterer;
        public string version;
        public string registrationKey;
        public string installPath;

        public int CompareTo(object obj)
        {
            SoftwareInfo other = (obj as SoftwareInfo);
            if (other == null) throw new ArgumentException(obj.GetType().Name);
            return this.productName.CompareTo(other.productName);
        }

        public override bool Equals(object obj)
        {
            SoftwareInfo other = (obj as SoftwareInfo);
            if (other == null) return false;
            return productName.Equals(other.productName);
        }

        public override string ToString()
        {
            return productName;
        }
    }
}
