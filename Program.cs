using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using u_doit.I_DoIt;
using u_doit.I_DoIt.ClassBuilder;
using u_doit.I_DoIt.Objects;
using u_doit.I_DoIt.Objects.Categories;
using u_doit.JsonRpc;
using u_doit.Objects.Categories;
using Version = System.Version;

namespace u_doit
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Usage();
                return;
            }
            switch (args[0])
            {
                case "scan-local":
                    switch (args[1])
                    {
                        case "online":
                            ScanLocal(args[2], args[3], args[4], args[5]);
                            return;
                        default:
                            Usage();
                            return;
                    }
                    break;
                case "generate-classes":
                    GenerateClasses(args[1], args[2], args[3], args[4]);
                    return;
                case "interactive":
                    Interactive(args[2], args[3], args[4], args[5]);
                    return;
                case "getKey":
                    if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                    {
                        Console.WriteLine("This is {0}, so you probably won't need a key for it.",
                            Environment.OSVersion.Platform.ToString());
                            return;
                    }
                    Console.WriteLine(new Win32NTHandler().OperatingSystemLicensingInfo());
                    return;
                case "hwSummary":
                    FileInfo outFile;
                    if (args.Length == 1)
                    {
                        outFile = new FileInfo(Environment.MachineName + ".txt");
                    }
                    else
                    {
                        outFile = new FileInfo(args[1]);
                    }
                    if (outFile.Exists)
                    {
                        outFile.Delete();
                    }
                    FileStream outStream = outFile.OpenWrite();
                    StreamWriter outWriter = new StreamWriter(outStream, System.Text.Encoding.UTF8);
                    HardwareSummary(outWriter);
                    outWriter.Flush();
                    outWriter.Close();
                    return;
				default:
					Usage();
					return;
            }

            /*
             * string apiKey = "1cak755";
            string url = "http://127.0.0.1:2500/src/jsonrpc.php";
            string username = "admin";
            string password = "admin";

            ScanLocal(apiKey, url, username, password);
            //GenerateClasses(apiKey, url, username, password);
            //Interactive(apiKey, url, username, password);
             */
        }

        
        static void Usage()
        {
            /*
             * Scan while on-line:
             * Syntax: scan-local online [apikey] [url] [username] [password]
             *    e.g: scan-local online 1cak755 http://127.0.0.1:2500/src/jsonrpc.php admin admin
             *    
             * Generate C# Classes for i-doit Constants (only useful for u-doit development):
             * Syntax: generate-classes [apikey] [url] [username] [password]
             *    e.g: generate-classes 1cak755 http://127.0.0.1:2500/src/jsonrpc.php admin admin
             *    
             * Open up a kind-of interactive i-doit Shell (only useful for u-doit development):
             * Syntax: interactive [apikey] [url] [username] [password]
             *    e.g: interactive 1cak755 http://127.0.0.1:2500/src/jsonrpc.php admin admin
             */
        }

        private static void AnalyzeCategories(Dictionary<string, string> gCats, Idoit idoit, List<string> ignoredList)
        {
            /*Dictionary<string, KeyValuePair<string, DialogFieldInfo>> enumKeyValuePairs =
                new Dictionary<string, KeyValuePair<string, DialogFieldInfo>>();*/

            foreach (var catDictionaryEntry in gCats)
            {
                if (catDictionaryEntry.Key.Equals("C__CMDB__SUBCAT__STORAGE__DEVICE"))
                {
                }
                try
                {
                    var fields = idoit.CategoryInfo(catDictionaryEntry.Key);
                    if (fields != null)
                    {
                        CSharpSourceWriter writer = new CSharpSourceWriter(catDictionaryEntry.Key,
                            catDictionaryEntry.Value);
                        foreach (var fieldEntry in fields)
                        {
                            if ((fieldEntry.Value.ui.type == "dialog" || (fieldEntry.Value.ui.type == "popup")) && (fieldEntry.Value.data.type == "int" || (fieldEntry.Value.data.type == "text")))
                            {
                                try
                                {
                                    var enumData = idoit.Dialog(catDictionaryEntry.Key, fieldEntry.Key);
                                    writer.AddEnum(fieldEntry.Value.ui.id, enumData);
                                }
                                catch (JsonReaderException)
                                {
                                }
                            }
                            writer.AddField(fieldEntry.Key, fieldEntry.Value);
                        }
                        writer.Finalize();
                        writer.SaveTo(writer.RecommendedFileName);
                        writer.Dispose();
                    }
                }
                catch (JsonRpcException rpcException)
                {
                    Console.WriteLine("-> Ignored class cause it could not be parsed.");
                    ignoredList.Add(catDictionaryEntry.Key);
                }
                catch (WebException webException)
                {
                    HttpWebResponse hwr = (HttpWebResponse) webException.Response;
                    if (hwr.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        Console.WriteLine("-> Ignored class because it does not contain any data.");
                        ignoredList.Add(catDictionaryEntry.Key);
                    }
                }
            }
        }

        //Now we know all about categories - now figure stuff out about objects!

        private static void GenerateClasses(string apiKey, string url, string username, string password)
        {
            var idoit = Idoit.Login(apiKey, url, username, password);

            List<ObjectType> ot = idoit.ObjectTypes();
            ConstantsResponse constants = idoit.Constants();
            List<string> ignoredList = new List<string>();

            AnalyzeCategories(constants.categories.g, idoit, ignoredList);
            AnalyzeCategories(constants.categories.s, idoit, ignoredList);
            File.WriteAllLines("ignoredClasses.txt", ignoredList.ToArray());
            idoit.Logout();
        }

        private static void ScanLocal(string apiKey, string url, string username, string password)
        {
            //In i-doit einloggen, und Konstanten abfragen.
            Idoit idoit = null;
            try
            {
                idoit = Idoit.Login(apiKey, url, username, password);
            }
            catch (JsonRpcException rpcException)
            {
                Console.WriteLine("-> Unable to connect to i-doit:");
                Console.WriteLine(rpcException.Error.message);
                return;
            }
            catch (WebException we)
            {
                Console.WriteLine("-> Unable to connect to i-doit:");
                Console.WriteLine(string.Format("-> {1}",null,we.Message));
                return;
            }

            VersionResponse vr = idoit.Version();
            Version parsedVersion = new Version(vr.version);
            if (parsedVersion < new Version(1,4,8))
            {
                Console.WriteLine("-> This Program requires i-doit 1.4.8");
                idoit.Logout();
                return;
            }
            
            ConstantsResponse constants = idoit.Constants();
            ObjectTypesResponse ot = idoit.ObjectTypes();
            
            //Die i-doit API erlaubt es uns nicht Objekttypen programmatisch anzulegen oder zu modifizieren.
            //Von daher testen die drei nächsten Absätze, ob es möglich ist Notebooks einzutragen, falls nicht, wird der Anwender aufgefordert, 
            //den Objekttyp anzulegen.
            if (!constants.objectTypes.ContainsValue("Notebook"))
            {
                Console.WriteLine("-> Could not find object type \"Notebook\" in your i-doit setup!");
                Console.WriteLine(
                    "-> Please create it by creating it in the Object type configuration in the i-doit web interface.");
                idoit.Logout();
                return;
            }
            ObjectTypeCategoriesResponse notebookCats = null;
            foreach (var m in constants.objectTypes)
            {
                if (m.Value.Equals("Notebook"))
                {
                    notebookCats = idoit.ObjectTypeCategories(m.Key);
                    break;
                }
            }
            foreach(Category c in new Category[] { new Drive(), new Model(), new SoundCard(), new Cpu(), new Memory(), new SoftwareAssignment(), })
            {
                if (!notebookCats.TestForConstant(c.Constant))
                {
                    Console.WriteLine("-> Could not find category \"{0}\" in object type \"Notebook\" in your i-doit setup!",c.DisplayName);
                    Console.WriteLine(
                        "-> Please add it by editing the Object type configuration in the i-doit web interface.");
                    idoit.Logout();
                    return;    
                }
            }
            
            //Rauskriegen, auf was für einem OS wir laufen
            IOperatingSystemHandler osHandler;
            osHandler = DetectOperatingSystem();
            if (osHandler == null)
            {
                idoit.Logout();
                return;
            }

            //Herausfinden was wir für eine Art von PC sind...
            ObjectType targetObjectType = null;
            if (osHandler.IsLaptop())
            {
                //Ich gehe mal davon aus, das Notebooks nicht als Server-artige Geräte verwendet werden.
                targetObjectType = ot.GetObjectTypeFromTitle("Notebook");
            }
            else if (osHandler.IsVirtualMachine())
            {
                if (osHandler.IsServer())
                {
                    targetObjectType = ot.GetObjectTypeFromTitle("Virtual server");
                }
                else
                {
                    targetObjectType = ot.GetObjectTypeFromTitle("Virtual client");
                }
            }
            else
            {
                if (osHandler.IsVirtualMachineHost())
                {
                    targetObjectType = ot.GetObjectTypeFromTitle("Virtual host");
                }
                else if (osHandler.IsServer())
                {
                    targetObjectType = ot.GetObjectTypeFromTitle("Server");
                }
                else
                {
                    targetObjectType = ot.GetObjectTypeFromTitle("Desktop");
                }
            }

            ObjectTypeCategoriesResponse targetObjectTypeCategories = idoit.ObjectTypeCategories(targetObjectType);

            //Unseren Rechner aus der Datenbank rausfischen, oder ihn eintragen...
            CmdbObject objectId;
            List<CmdbObject> o = idoit.FindObjects(Environment.MachineName, targetObjectType);
            if (o.Count > 1)
            {
                Console.WriteLine(
                    "Es gibt mehr als einen {0} der {1} heißt - bitte physikalisch beheben, dann in i-doit manuell löschen!");
                idoit.Logout();
                return;
            }
            else if (o.Count == 0)
            {
                CreateObjectResponse createObjectResponse = idoit.CreateCmdbObject(targetObjectType,
                    Environment.MachineName);
                if (!createObjectResponse.success)
                {
                    Console.WriteLine("-> {0}", createObjectResponse.message);
                    Console.WriteLine("-> Konnte das Objekt nicht anlegen, bitte in den Logs von i-doit nachsehen...");
                    idoit.Logout();
                    return;
                }
                objectId = new CmdbObject();
                objectId.id = createObjectResponse.id;
            }
            else
            {
                objectId = o[0];
            }


            //CPU Hersteller
            IdoitEnumerator cpuManufactorEnumerator = idoit.Dialog(new Cpu().Constant, "manufacturer");
            bool cpuManufactorEnumeratorChanged = false;
            foreach (string manufac in Config.cpuManufactors)    //Liste der CPU Hersteller von: https://de.wikipedia.org/wiki/CPUID
            {
                if (!cpuManufactorEnumerator.TestTitle(manufac))
                {
                    idoit.DialogEdit(new Cpu(), "manufacturer", manufac);
                    cpuManufactorEnumeratorChanged = true;
                }
            }
            if (cpuManufactorEnumeratorChanged) cpuManufactorEnumerator = idoit.Dialog(new Cpu().Constant, "manufacturer");

            IdoitEnumerator cpuModelEnumator = idoit.Dialog(new Cpu().Constant, "type");
            bool cpuModelEnumeratorChanged = false;
            foreach (string model in Config.cpuModels)   //TODO: mehr Modell IDs rauskriegen...
            {
                if (!cpuModelEnumator.TestTitle(model))
                {
                    idoit.DialogEdit(new Cpu(), "type", model);
                    cpuModelEnumeratorChanged = true;
                }
            }
            if (cpuModelEnumeratorChanged) cpuModelEnumator = idoit.Dialog(new Cpu().Constant, "type");

            

            //CPU
            if (targetObjectTypeCategories.TestForConstant(new Cpu()))
            {
                List<Cpu> cpus = new List<Cpu>();
                cpus.AddRange(osHandler.GetCpu());
                foreach (Cpu c in cpus)
                {
                    string modelCmp = c.title.Replace("(TM)", "");
                    foreach (string manufac in Config.cpuManufactors)
                    {
                        if (c.cpuid.Contains(manufac))
                        {
                            c.manufacturer = cpuManufactorEnumerator.FindTitleLike(manufac);
                            break;
                        }
                    }
                    foreach (EnumInfo ei in cpuModelEnumator)
                    {
                        if (modelCmp.Contains(ei.title))
                        {
                            c.type = ei.id;
                            break;
                        }
                    }
                }
                List<Cpu> cpusInDb = idoit.CategoryRead<Cpu>(objectId.id, cpus[0]);
                idoit.SyncList(objectId, cpusInDb, cpus);
            }
            else
            {
                Console.WriteLine("-> According to i-doit {0}s do not have {1}s", targetObjectType.title, "CPU");
            }

            //Physikalische Laufwerke
            if (targetObjectTypeCategories.TestForConstant(new StorageDevice()))
            {
                List<StorageDevice> storageDevices = osHandler.GetStorageDevices();
                List<StorageDevice> storageDevicesInDb = idoit.CategoryRead<StorageDevice>(objectId.id,
                    new StorageDevice());
                idoit.SyncList(objectId, storageDevicesInDb, storageDevices);
            }
            else
            {
                Console.WriteLine("-> According to i-doit {0}s do not have {1}s", targetObjectType.title, "Storage Device");
            }

            //Logische Laufwerke
            if (targetObjectTypeCategories.TestForConstant(new Drive()))
            {
                List<Drive> drives = osHandler.GetDrives(idoit);
                List<Drive> drivesInDb = idoit.CategoryRead<Drive>(objectId.id, new Drive());
                idoit.SyncList(objectId, drivesInDb, drives);
            }
            else
            {
                Console.WriteLine("-> According to i-doit {0}s do not have {1}s", targetObjectType.title, "Drive");    
            }

            //RAM Hersteller
            IdoitEnumerator ramManufactorEnumerator = idoit.Dialog(new Memory().Constant, "manufacturer");
            foreach (KeyValuePair<string, string> kv in Config.ramManufactors)
            {
                if (!ramManufactorEnumerator.TestTitle(kv.Value))
                {
                    idoit.DialogEdit(new Memory(), "manufacturer", kv.Value);
                    ramManufactorEnumerator = idoit.Dialog(new Memory().Constant, "manufacturer");
                }
            }

            //RAMs
            if (targetObjectTypeCategories.TestForConstant(new Memory()))
            {
                List<Memory> rams = osHandler.GetMemoryBanks(idoit);
                List<Memory> ramsInDb = idoit.CategoryRead<Memory>(objectId.id, new Memory());
                idoit.SyncList(objectId, ramsInDb, rams);
            }
            else
            {
                Console.WriteLine("-> According to i-doit {0}s do not have {1}s", targetObjectType.title, "Memory");
            }

            //Als nächstes wollen wir Software Assignments eintragen, dazu stellen wir zunächst sicher, dass jede installierte Software auch in i-doit bekannt ist.
            if (targetObjectTypeCategories.TestForConstant(new SoftwareAssignment()))
            {
                ObjectType softwareObjectType = ot.GetObjectTypeFromTitle("Application");
                List<SoftwareInfo> softwares = osHandler.GetSoftware();
                List<CmdbObject> softwareKnown = idoit.FindObjects(softwareObjectType);
                IdoitEnumerator softwareManufacturers = idoit.Dialog(new Applications().Constant, "manufacturer");
                foreach (SoftwareInfo si in softwares)
                {
                    if (softwareKnown.Find(x => x.title.Equals(si.productName)) == null)
                    {
                        //Software in i-doit bekannt machen.
                        int newSoftwareId = idoit.CreateCmdbObject(softwareObjectType, si.productName).id;
                        CmdbObject newSoftware = idoit.GetCmdbObject(newSoftwareId);
                        Applications newSoftwareApplications = new Applications();
                        newSoftwareApplications.install_path = si.installPath;
                        newSoftwareApplications.release = si.version;

                        //Software-Entwickler in i-doit bekannt machen.
                        if (!string.IsNullOrEmpty(si.manufacterer))
                        {
                            bool manufacturerKnown = softwareManufacturers.TestTitle(si.manufacterer);
                            if (!manufacturerKnown)
                            {
                                idoit.DialogEditSpecific(newSoftwareApplications, "manufacturer", si.manufacterer);
                                softwareManufacturers = idoit.Dialog(new Applications().Constant, "manufacturer");
                            }
                            newSoftwareApplications.manufacturer = softwareManufacturers.FindTitleLike(si.manufacterer);
                        }

                        //Software-Info schreiben.
                        idoit.CategoryCreate(newSoftware, newSoftwareApplications);

                        softwareKnown = idoit.FindObjects(softwareObjectType);
                        //direkt nochmal abfragen, um Doubletten auszuschließen.
                    }
                }

                //Liste von Software Assignments erzeugen:
                List<SoftwareAssignment> assignments =
                    softwares.ConvertAll(
                        x => new SoftwareAssignment(softwareKnown.Find(y => y.title.Equals(x.productName)).id));

                //Das Betriebssystem ist nach der Logik von i-doit auch ein Software Assignment. Von daher müssen wir uns nun um Betriebssystem-Infos kümmern:
                string uname = osHandler.uname();
                ObjectType operatingSystemObjectType = ot.GetObjectTypeFromTitle("Operating System");
                List<CmdbObject> knownOperatingSystems = idoit.FindObjects(operatingSystemObjectType);
                CmdbObject operatingSystemCmdbObject = null;
                operatingSystemCmdbObject = knownOperatingSystems.Find((x) => x.title.Equals(uname));
                if (operatingSystemCmdbObject == null)
                {
                    operatingSystemCmdbObject = new CmdbObject(idoit.CreateCmdbObject(operatingSystemObjectType, uname));
                }
                SoftwareAssignment osSoftwareAssignment = new SoftwareAssignment();
                osSoftwareAssignment.application = operatingSystemCmdbObject.id;

                //Dem Software Assignment für das Betriebssystem ggf. eine Lizenz nahe bringen.
                if (osHandler.OperatingSystemNeedsLicense())    //sowas braucht bekanntlich nur Windows... :D
                {
                    string cdKey = osHandler.OperatingSystemLicensingInfo();
                    string licenseName = string.Format("{0}-Lizenz für {1}", uname, Environment.MachineName);

                    ObjectType licenseObjectType = ot.GetObjectTypeFromTitle("Licenses");
                    List<CmdbObject> knownLicenses = idoit.FindObjects(licenseObjectType);
                    CmdbObject licenseCmdbObject = null;
                    operatingSystemCmdbObject = knownLicenses.Find((x) => x.title.Equals(licenseName));
                    if (operatingSystemCmdbObject == null)
                    {
                        //Neues Objekt für die Lizenz anlegen:
                        licenseCmdbObject = new CmdbObject(idoit.CreateCmdbObject(licenseObjectType, licenseName));
                        //...und die Lizenz im Feld "Description" hinterlegen.
                        Global licenseGlobal = idoit.CategoryRead<Global>(licenseCmdbObject.id, new Global())[0];
                        licenseGlobal.description = cdKey;

                        idoit.CategoryCreate<Global>(licenseCmdbObject, licenseGlobal); //Gut zu wissen: Wenn eine Kategorie nur einmal in einem Objekt vorhanden ist, wird
                        //sie durch Create überschrieben...
                    }
                    //osSoftwareAssignment.assigned_license = licenseCmdbObject.id;     //kann nicht direkt zugewiesen werden, gibt eine Exception dass irgendwelche SQL-Constraints nicht erfüllt sind...

                    //TODO: Das selbe hier für Office und Konsorten machen...
                }
                assignments.Add(osSoftwareAssignment);

                List<SoftwareAssignment> assignmentsInDb = idoit.CategoryRead<SoftwareAssignment>(objectId.id,
                    new SoftwareAssignment());
                idoit.SyncList(objectId, assignmentsInDb, assignments);
            }
            else
            {
                Console.WriteLine("-> According to i-doit {0}s do not have {1}s", targetObjectType.title, "Software assignment");    
            }

            //Samba Shares abgleichen
            if (targetObjectTypeCategories.TestForConstant(new Share()))
            {
                List<Share> shares = osHandler.GetShares();
                List<Share> sharesInDb = idoit.CategoryRead<Share>(objectId.id, new Share());
                idoit.SyncList(objectId, sharesInDb, shares);
            }
            else
            {
                Console.WriteLine("-> According to i-doit {0}s do not have {1}s", targetObjectType.title, "Share");    
            }

            //Soundkarten abgleichen
            if (targetObjectTypeCategories.TestForConstant(new SoundCard()))
            {
                List<SoundCard> soundCards = osHandler.GetSoundCards(idoit);
                List<SoundCard> soundCardsInDb = osHandler.GetSoundCards(idoit);
                idoit.SyncList(objectId, soundCardsInDb, soundCards);
            }
            else
            {
                Console.WriteLine("-> According to i-doit {0}s do not have {1}s", targetObjectType.title, "Sound card");
            }

            idoit.Logout();

        }

        private static IOperatingSystemHandler DetectOperatingSystem()
        {
            IOperatingSystemHandler osHandler;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                    osHandler = new Win32NTHandler();
                    break;
                default:
                    Console.WriteLine("-> Sorry, your platform ({0}) is not yet supported...", Environment.OSVersion.Platform);
                    osHandler = null;
                    break;
            }
            return osHandler;
        }

        private static void Interactive(string apiKey, string url, string username, string password)
        {
            Idoit idoit = Idoit.Login(apiKey, url, username, password);
            string cat = null;
            bool active = true;
            Console.WriteLine("OK");
            while (active)
            {
                string[] args = Console.ReadLine().Split(' ');
                switch (args[0])
                {
                    case "setcat":
                        cat = args[1];
                        break;
                    case "enum":
                        if (string.IsNullOrEmpty(cat))
                        {
                            Console.WriteLine("no category specified. Use setcat first.");
                        }
                        CSharpSourceWriter csw = new CSharpSourceWriter("", "");
                        var dialog = idoit.Dialog(cat, args[1]);
                        csw.GenEnum(args[1], dialog, Console.Out);
                        csw.Dispose();
                        break;
                    case "exit":
                        active = false;
                        break;
                    default:
                        Console.WriteLine("What?");
                        break;
                }
            }
            idoit.Logout();
        }

        private static void HardwareSummary(StreamWriter output)
        {
            IOperatingSystemHandler os = DetectOperatingSystem();
            IdoitEmulator emu = new IdoitEmulator();

            if (os == null)
            {
                Console.WriteLine("Unsupported Platform.");
                return;
            }

            output.WriteLine("Machine Name: {0}", Environment.MachineName);
            output.WriteLine("Logical CPUs: {0}", Environment.ProcessorCount);
            output.WriteLine("Domain Name: {0}", Environment.UserDomainName);
            output.WriteLine("");
            output.WriteLine("Physical CPUs: ");
            foreach(Cpu cpu in os.GetCpu())
            {
                output.WriteLine("{0} ({1} MHZ) ({2} cores)", cpu.title, cpu.frequency, cpu.cores);
            }

            output.WriteLine("");
            output.WriteLine("Logical Volumes: {0}");
            foreach(Drive d in os.GetDrives(emu))
            {
                output.WriteLine("{0} \t {1} GB, Volume serial number: {2}", d.mount_point, d.capacity, d.serial);
            }

            output.WriteLine("");
            output.WriteLine("Memory banks:");
            foreach(var mb in os.GetMemoryBanks(emu))
            {
                output.WriteLine("1x {1} - {0} MB", mb.capacity,mb.description);
            }

            output.WriteLine("");
            output.WriteLine("Storage devices:");
            foreach(var stor in os.GetStorageDevices())
            {
                output.WriteLine("{0}: {1} ({2} GB)", stor.type, stor.title, stor.capacity);
            }

            output.WriteLine("");
            output.WriteLine("Installed Programs and features:");
            foreach (var s in os.GetSoftware())
            {
                output.WriteLine("{1} {2} (by {0})", s.manufacterer, s.productName, s.version);
            }
        }
    }
}
