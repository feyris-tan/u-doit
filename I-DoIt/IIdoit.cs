using System;
namespace u_doit.I_DoIt
{
    interface IIdoit
    {
        void CategoryCreate<T>(CmdbObject obj, T theObject) where T : u_doit.I_DoIt.Objects.Category;
        void CategoryDelete<T>(CmdbObject obj, string catConstant, int categoryNumber) where T : u_doit.I_DoIt.Objects.Category;
        System.Collections.Generic.Dictionary<string, DialogFieldInfo> CategoryInfo(int ci);
        System.Collections.Generic.Dictionary<string, DialogFieldInfo> CategoryInfo(string ci);
        System.Collections.Generic.Dictionary<string, DialogFieldInfo> CategoryInfo(CategoryInfo ci);
        System.Collections.Generic.List<T> CategoryRead<T>(long id, int catg);
        System.Collections.Generic.List<T> CategoryRead<T>(long id, u_doit.I_DoIt.Objects.Category c);
        void CategoryUpdate<T>(CmdbObject obj, T theObject) where T : u_doit.I_DoIt.Objects.Category;
        void CategoryUpdate<T>(CmdbObject obj, T theObject, int categoryID) where T : u_doit.I_DoIt.Objects.Category;
        ConstantsResponse Constants();
        CreateObjectResponse CreateCmdbObject(ObjectType objectType, string name);
        IdoitEnumerator Dialog(string categoryConstant, string property);
        bool DialogEdit(u_doit.I_DoIt.Objects.Category c, string propertyName, string valueToAdd);
        bool DialogEditSpecific(u_doit.I_DoIt.Objects.Category c, string propertyName, string valueToAdd);
        System.Collections.Generic.List<CmdbObject> FindObjects(string title, ObjectType objectType);
        System.Collections.Generic.List<CmdbObject> FindObjects(ObjectType objectType);
        CmdbObject GetCmdbObject(int cmdb_id);
        LoginResponse Login(string username, string password);
        LogoutResponse Logout();
        ObjectTypeCategoriesResponse ObjectTypeCategories(int otId);
        ObjectTypeCategoriesResponse ObjectTypeCategories(string constant);
        ObjectTypeCategoriesResponse ObjectTypeCategories(ObjectType ot);
        ObjectTypesResponse ObjectTypes();
        void SyncList<TCat>(CmdbObject obj, System.Collections.Generic.List<TCat> inDb, System.Collections.Generic.List<TCat> inMachine) where TCat : u_doit.I_DoIt.Objects.Category;
        VersionResponse Version();
    }
}
