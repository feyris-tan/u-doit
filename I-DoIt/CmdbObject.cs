using System;
using System.Collections.Generic;
using System.Text;

namespace u_doit.I_DoIt
{
    class CmdbObject
    {
        public CmdbObject()
        {
            
        }

        public CmdbObject(CreateObjectResponse cor)
        {
            this.id = cor.id;
        }

        public int id;
        public string title;
        public string sysid;
        public int type;
        public string type_title;
        public string type_icon;
        public int type_group;
        public string type_group_title;
        public int status;
        public int cmdb_status;
        public string cmdb_status_title;
        public string image;

        protected bool Equals(CmdbObject other)
        {
            return id == other.id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CmdbObject) obj);
        }

        public override int GetHashCode()
        {
            return id;
        }
    }
}
