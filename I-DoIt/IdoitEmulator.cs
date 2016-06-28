using System;
using System.Collections.Generic;
using System.Text;

namespace u_doit.I_DoIt
{
    class IdoitEmulator : IIdoit
    {
        public IdoitEmulator()
        {
            enums = new Dictionary<string, IdoitEnumerator>();
        }

        Dictionary<string, IdoitEnumerator> enums;

        public void CategoryCreate<T>(CmdbObject obj, T theObject) where T : Objects.Category
        {
            throw new NotImplementedException();
        }

        public void CategoryDelete<T>(CmdbObject obj, string catConstant, int categoryNumber) where T : Objects.Category
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, DialogFieldInfo> CategoryInfo(int ci)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, DialogFieldInfo> CategoryInfo(string ci)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, DialogFieldInfo> CategoryInfo(CategoryInfo ci)
        {
            throw new NotImplementedException();
        }

        public List<T> CategoryRead<T>(long id, int catg)
        {
            throw new NotImplementedException();
        }

        public List<T> CategoryRead<T>(long id, Objects.Category c)
        {
            throw new NotImplementedException();
        }

        public void CategoryUpdate<T>(CmdbObject obj, T theObject) where T : Objects.Category
        {
            throw new NotImplementedException();
        }

        public void CategoryUpdate<T>(CmdbObject obj, T theObject, int categoryID) where T : Objects.Category
        {
            throw new NotImplementedException();
        }

        public ConstantsResponse Constants()
        {
            throw new NotImplementedException();
        }

        public CreateObjectResponse CreateCmdbObject(ObjectType objectType, string name)
        {
            throw new NotImplementedException();
        }

        public IdoitEnumerator Dialog(string categoryConstant, string property)
        {
            if (!enums.ContainsKey(property))
            {
                enums.Add(property, new IdoitEnumerator());
            }
            return enums[property];
        }

        public bool DialogEdit(Objects.Category c, string propertyName, string valueToAdd)
        {
            if (!enums.ContainsKey(propertyName))
            {
                enums.Add(propertyName, new IdoitEnumerator());
            }
            EnumInfo child = new EnumInfo();
            child.title = valueToAdd;
            child.id = enums[propertyName].Count;

            enums[propertyName].Add(child);
            return true;
        }

        public bool DialogEditSpecific(Objects.Category c, string propertyName, string valueToAdd)
        {
            throw new NotImplementedException();
        }

        public List<CmdbObject> FindObjects(string title, ObjectType objectType)
        {
            throw new NotImplementedException();
        }

        public List<CmdbObject> FindObjects(ObjectType objectType)
        {
            throw new NotImplementedException();
        }

        public CmdbObject GetCmdbObject(int cmdb_id)
        {
            throw new NotImplementedException();
        }

        public LoginResponse Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public LogoutResponse Logout()
        {
            throw new NotImplementedException();
        }

        public ObjectTypeCategoriesResponse ObjectTypeCategories(int otId)
        {
            throw new NotImplementedException();
        }

        public ObjectTypeCategoriesResponse ObjectTypeCategories(string constant)
        {
            throw new NotImplementedException();
        }

        public ObjectTypeCategoriesResponse ObjectTypeCategories(ObjectType ot)
        {
            throw new NotImplementedException();
        }

        public ObjectTypesResponse ObjectTypes()
        {
            throw new NotImplementedException();
        }

        public void SyncList<TCat>(CmdbObject obj, List<TCat> inDb, List<TCat> inMachine) where TCat : Objects.Category
        {
            throw new NotImplementedException();
        }

        public VersionResponse Version()
        {
            throw new NotImplementedException();
        }
    }
}
