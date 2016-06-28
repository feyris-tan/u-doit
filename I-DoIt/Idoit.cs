using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Newtonsoft.Json;
using u_doit.I_DoIt.Objects;
using u_doit.I_DoIt.Objects.Categories;
using u_doit.JsonRpc;
using u_doit.Objects.Categories;

namespace u_doit.I_DoIt
{
    class Idoit : JsonRpcClass, u_doit.I_DoIt.IIdoit
    {
        public VersionResponse Version()
        {
            Console.WriteLine("Checking i-doit Version...");
            return JsonRpcCaller.Call<VersionResponse>("idoit.version", new {apikey = Config.GetInstance().ApiKey});
        }

        public LoginResponse Login(string username,string password)
        {
            Console.WriteLine("Logging in...");
            JsonRpcCaller.XRpcAuthUsername = username;
            JsonRpcCaller.XRpcAuthPassword = password;

            LoginResponse result = JsonRpcCaller.Call<LoginResponse>("idoit.login",
                new {apikey = Config.GetInstance().ApiKey});

            //Daten wieder vergessen, nachdem das einloggen erfolgreich war!
            JsonRpcCaller.XRpcAuthUsername = null;
            JsonRpcCaller.XRpcAuthPassword = null;

            JsonRpcCaller.XRpcAuthSession = result.session_id;
            return result;
        }

        public LogoutResponse Logout()
        {
            Console.WriteLine("Logging out...");
            LogoutResponse result = JsonRpcCaller.Call<LogoutResponse>("idoit.logout", new { apikey = Config.GetInstance().ApiKey });
            if (result.result == 1)
            {
                JsonRpcCaller.XRpcAuthSession = null;
            }
            return result;
        }

        public ConstantsResponse Constants()
        {
            Console.WriteLine("Getting constants...");
            ConstantsResponse response = JsonRpcCaller.Call<ConstantsResponse>("idoit.constants", new { apikey = Config.GetInstance().ApiKey });
            return response;
        }

        public ObjectTypesResponse ObjectTypes()
        {
            Console.WriteLine("Getting object types...");
            ObjectTypesResponse result = JsonRpcCaller.Call<ObjectTypesResponse>("cmdb.object_types", new { apikey = Config.GetInstance().ApiKey });
            return result;
        }

        public ObjectTypeCategoriesResponse ObjectTypeCategories(ObjectType ot)
        {
            Console.WriteLine("Getting object type categories...");
            ObjectTypeCategoriesResponse result = JsonRpcCaller.Call<ObjectTypeCategoriesResponse>("cmdb.object_type_categories", new
            {
                apikey = Config.GetInstance().ApiKey,
                type = ot.constant
            });
            return result;
        }

        public ObjectTypeCategoriesResponse ObjectTypeCategories(int otId)
        {
            Console.WriteLine("Getting object type categories...");
            ObjectTypeCategoriesResponse result = JsonRpcCaller.Call<ObjectTypeCategoriesResponse>("cmdb.object_type_categories", new
            {
                apikey = Config.GetInstance().ApiKey,
                type = otId
            });
            return result;
        }

        public Dictionary<string, DialogFieldInfo> CategoryInfo(CategoryInfo ci)
        {
            Console.WriteLine("Getting category info {0}...", ci.constant);
            Dictionary<string, DialogFieldInfo> result = JsonRpcCaller.Call < Dictionary<string, DialogFieldInfo>>("cmdb.category_info.read", new
            {
                apikey = Config.GetInstance().ApiKey,
                category = ci.constant
            });
            return result;
        }

        public Dictionary<string, DialogFieldInfo> CategoryInfo(string ci)
        {
            switch (ci) //Workaround für einen Bug in der i-doit API.
            {
                case "C__CMDB__SUBCAT__STORAGE__DEVICE":
                    return CategoryInfo(43);
                default:
                    break;
            }
            Console.WriteLine("Getting category info {0}...", ci);
            Dictionary<string, DialogFieldInfo> result = JsonRpcCaller.Call<Dictionary<string, DialogFieldInfo>>("cmdb.category_info.read", new
            {
                apikey = Config.GetInstance().ApiKey,
                category = ci
            });
            return result;
        }

        public Dictionary<string, DialogFieldInfo> CategoryInfo(int ci)
        {
            Console.WriteLine("Getting category info {0}...", ci);
            Dictionary<string, DialogFieldInfo> result = JsonRpcCaller.Call<Dictionary<string, DialogFieldInfo>>("cmdb.category_info.read", new
            {
                apikey = Config.GetInstance().ApiKey,
                catgID = ci
            });
            return result;
        }

        public ObjectTypeCategoriesResponse ObjectTypeCategories(string constant)
        {
            Console.WriteLine("Getting Object Type Categories of {0}...", constant);
            ObjectTypeCategoriesResponse result = JsonRpcCaller.Call<ObjectTypeCategoriesResponse>("cmdb.object_type_categories", new
            {
                apikey = Config.GetInstance().ApiKey,
                type = constant
            });
            return result;
        }

        public IdoitEnumerator Dialog(string categoryConstant, string property)
        {
            Console.WriteLine("Getting Dialog {0} of {1}...", property, categoryConstant);
            IdoitEnumerator result = JsonRpcCaller.Call<IdoitEnumerator>("cmdb.dialog.read", new
            {
                apikey = Config.GetInstance().ApiKey,
                category = categoryConstant,
                property = property
            });

            result.categoryConstant = categoryConstant;
            result.property = property;

            return result;
        }
 
        public static Idoit Login(string apiKey, string url, string username, string password)
        {
            Config.GetInstance().ApiKey = apiKey;
            JsonRpcClass.JsonRpcCaller = new JsonRpcCaller(url);
            Idoit idoit = new Idoit();
            idoit.Login(username, password);
            return idoit;
        }

        public List<CmdbObject> FindObjects(string title, ObjectType objectType)
        {
            Console.WriteLine("Looking up {1}s named {0}...", title, objectType.title);
            List<CmdbObject> o = JsonRpcCaller.Call<List<CmdbObject>>("cmdb.objects", new
            {
                apikey = Config.GetInstance().ApiKey,
                filter = new
                {
                    title = title,
                    type = objectType.id
                }
            });
            return o;
        }

        public List<CmdbObject> FindObjects(ObjectType objectType)
        {
            Console.WriteLine("Looking up {1}s...", null, objectType.title);
            List<CmdbObject> o = JsonRpcCaller.Call<List<CmdbObject>>("cmdb.objects", new
            {
                apikey = Config.GetInstance().ApiKey,
                filter = new
                {
                    type = objectType.id
                }
            });
            return o;
        }

        public CreateObjectResponse CreateCmdbObject(ObjectType objectType, string name)
        {
            Console.WriteLine("Creating a {0} named {1}...", objectType.title, name);
            CreateObjectResponse response = JsonRpcCaller.Call<CreateObjectResponse>("cmdb.object.create", new
            {
                apikey = Config.GetInstance().ApiKey,
                type = objectType.constant,
                title = name
            });
            Console.WriteLine("-> {0}", response.message);

            return response;
        }

        public CmdbObject GetCmdbObject(int cmdb_id)
        {
            Console.WriteLine("Fetching CMDB Object {0}...", cmdb_id);
            CmdbObject response = JsonRpcCaller.Call<CmdbObject>("cmdb.object.read", new
            {
                apikey = Config.GetInstance().ApiKey,
                id = cmdb_id
            });
            return response;
        }

        public bool DialogEdit(Category c, string propertyName, string valueToAdd)
        {
            Console.WriteLine("Adding {0} to {1} {2}s...", valueToAdd, c.DisplayName, propertyName);

            DialogEditResponse response = JsonRpcCaller.Call<DialogEditResponse>("cmdb.dialog.create", new
            {
                apikey = Config.GetInstance().ApiKey,
                catgID = c.Constant,        //nicht dokumentierter Aufruf... Quelle: http://forum.i-doit.org/index.php/topic,4570.msg13880.html#msg13880
                property = propertyName,
                value = valueToAdd
            });

            return response.success;
        }

        public bool DialogEditSpecific(Category c, string propertyName, string valueToAdd)
        {
            Console.WriteLine("Adding {0} to {1} {2}s...", valueToAdd, c.DisplayName, propertyName);

            DialogEditResponse response = JsonRpcCaller.Call<DialogEditResponse>("cmdb.dialog.create", new
            {
                apikey = Config.GetInstance().ApiKey,
                catsID = c.Constant,
                property = propertyName,
                value = valueToAdd
            });

            return response.success;
        }

        public List<T> CategoryRead<T>(long id, Category c)
        {
            if (c.Constant.Equals("C__CMDB__SUBCAT__STORAGE__DEVICE"))
            {
                return CategoryRead<T>(id, 43); //Workaround für einen Bug in der i-doit API.
            }

            List<T> temp = JsonRpcCaller.Call<List<T>>("cmdb.category.read", new
            {
                apikey = Config.GetInstance().ApiKey,
                objID = id,
                category = c.Constant
            });
            return temp;
        }

        public List<T> CategoryRead<T>(long id, int catg)
        {
            List<T> temp = JsonRpcCaller.Call<List<T>>("cmdb.category.read", new
            {
                apikey = Config.GetInstance().ApiKey,
                objID = id,
                catgID = 43
            });
            return temp;
        }

        public void CategoryDelete<T>(CmdbObject obj, string catConstant, int categoryNumber)
            where T : Category
        {
            Console.WriteLine("Deleting {1} #{2} from {0}", obj.title, catConstant, categoryNumber);
            CategoryDeleteResponse cdr = JsonRpcCaller.Call<CategoryDeleteResponse>("cmdb.category.delete", new
            {
                catgID = catConstant,
                cateID = categoryNumber,
                objID = obj.id,
                apikey = Config.GetInstance().ApiKey
            });

            if (cdr.success != 1)
            {
                throw new Exception(cdr.message);
            }
        }

        public void CategoryCreate<T>(CmdbObject obj, T theObject)
            where T : Category
        {
            Console.WriteLine("Adding a {0} to {1}", theObject.DisplayName, obj.title);

            CategoryCreateResponse response;
            if (theObject.Constant.Contains("CATS__"))
            {
                response = JsonRpcCaller.Call<CategoryCreateResponse>("cmdb.category.create", new
                {
                    apikey = Config.GetInstance().ApiKey,
                    catsID = theObject.Constant,
                    objID = obj.id,
                    data = theObject,
                });
            }
            else
            {
                response = JsonRpcCaller.Call<CategoryCreateResponse>("cmdb.category.create", new
                {
                    apikey = Config.GetInstance().ApiKey,
                    catgID = theObject.Constant,
                    objID = obj.id,
                    data = theObject,
                });
            }

            if (response.success != 1)
            {
                throw new Exception(response.message);
            }
        }

        public void CategoryUpdate<T>(CmdbObject obj, T theObject,int categoryID)
            where T : Category
        {
            Dictionary<string, object> updateData = new Dictionary<string, object>();
            updateData.Add("category_id", theObject.id);
            foreach (var fi in theObject.GetType().GetFields())
            {
                //Falls ein Feld mit JsonIgnore gekennzeichnet ist, es ausschließen.
                object[] attribs = fi.GetCustomAttributes(true);
                bool ignoreMe = false;
                foreach (object o in attribs)
                {
                    if (o is JsonIgnoreAttribute)
                    {
                        ignoreMe = true;
                        break;
                    }
                }
                if (ignoreMe) continue;

                //Feld in die Update Warteschlage einreihen:
                updateData.Add(fi.Name, fi.GetValue(theObject));
            }

            Console.WriteLine("Updating {0} #{1} in {2}", theObject.DisplayName, theObject.id, obj.title);
            CategoryDeleteResponse r = JsonRpcCaller.Call<CategoryDeleteResponse>("cmdb.category.update", new
            {
                apikey = Config.GetInstance().ApiKey,
                catgID = categoryID,
                objID = obj.id,
                data = updateData
            });

            if (r.success != 1)
            {
                throw new Exception(r.message);
            }
        }

        public void CategoryUpdate<T>(CmdbObject obj, T theObject)
            where T : Category
        {
            if (theObject.Constant.Equals("C__CMDB__SUBCAT__STORAGE__DEVICE"))
            {
                CategoryUpdate(obj, theObject, 43);     //Workaround für einen Bug in der i-doit API
                return;
            }
            Dictionary<string, object> updateData = new Dictionary<string, object>();
            updateData.Add("category_id", theObject.id);
            foreach (var fi in theObject.GetType().GetFields())
            {
                //Falls ein Feld mit JsonIgnore gekennzeichnet ist, es ausschließen.
                object[] attribs = fi.GetCustomAttributes(true);
                bool ignoreMe = false;
                foreach (object o in attribs)
                {
                    if (o is JsonIgnoreAttribute)
                    {
                        ignoreMe = true;
                        break;
                    }
                }
                if (ignoreMe) continue;

                //Feld in die Update Warteschlage einreihen:
                updateData.Add(fi.Name, fi.GetValue(theObject));
            }

            Console.WriteLine("Updating {0} #{1} in {2}", theObject.DisplayName, theObject.id, obj.title);
            CategoryDeleteResponse r = JsonRpcCaller.Call<CategoryDeleteResponse>("cmdb.category.update", new
            {
                apikey = Config.GetInstance().ApiKey,
                category = theObject.Constant,
                objID = obj.id,
                data = updateData
            });

            if (r.success != 1)
            {
                throw new Exception(r.message);
            }
        }

        public void SyncList<TCat>(CmdbObject obj, List<TCat> inDb, List<TCat> inMachine)
            where TCat : Category
        {
            //Alle Elemente löschen, die zwar in der Datenbank stehen, aber nicht in der Maschine verbaut sind
            foreach (TCat e in inDb)
            {
                if (!inMachine.Contains(e))
                {
                    CategoryDelete<TCat>(obj, e.Constant, e.id);
                    inDb.Remove(e);
                    SyncList<TCat>(obj, inDb , inMachine);  //Da sich der Enumerator verändert, muss die foreach neu gestartet werden.
                    return;
                }
            }

            //Alle Elemente hinzufügen, die zwar in der Maschine verwendet werden, aber nicht in der Datenbank verzeichnet sind.
            foreach (TCat e in inMachine)
            {
                if (!inDb.Contains(e))
                {
                    CategoryCreate<TCat>(obj, e);
                    inDb.Add(e);
                }
                else
                {
                    //Wenn's schon in der Datenbank steht, dann aktualisieren (sinnvoll bei z.b. Software-Versionen):
                    TCat other = inDb.Find((x) => x.Equals(e));     //Das selbe Element aus der Datenbank rauspicken
                    if (e.IsUpdateNeeded(other))    //unnötigen Netzwerkverkehr vermeiden.
                    {
                        e.id = other.id;   
                        CategoryUpdate<TCat>(obj, e);   
                    }
                }
            }

            if ((inDb.Count != inMachine.Count) && !typeof(TCat).Equals(typeof(SoftwareAssignment)))
            {
                //TODO: manuellen löscher implementieren
                Console.WriteLine("Problem mit den {0} von {1}, bitte alle {0}s  von {1} in i-doit manuell löschen!",
                    inDb[0].DisplayName, obj.title);
                return;
            }
        }

    }
}
