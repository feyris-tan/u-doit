using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using u_doit.I_DoIt;
using u_doit.I_DoIt.Objects.Categories;
using u_doit.Objects.Categories;

namespace u_doit
{
    internal class Win32NTHandler : IOperatingSystemHandler
    {
        static class Win32KernelCalls
        {

            [DllImport("shlwapi.dll", SetLastError = true, EntryPoint = "#437")]
            public static extern bool IsOS(int os);

            [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public extern static bool GetVolumeInformation(
                string RootPathName,
                StringBuilder VolumeNameBuffer,
                int VolumeNameSize,
                out uint VolumeSerialNumber,
                out uint MaximumComponentLength,
                out FileSystemFeature FileSystemFlags,
                StringBuilder FileSystemNameBuffer,
                int nFileSystemNameSize);

            public const int OS_ANYSERVER = 29;

            public static bool IsWindows10()
            {
                return WindowsProductName().StartsWith("Windows 10");
            }

            public static bool IsWindowsXP()
            {
                string pname = WindowsProductName();
                return pname.Contains("Windows XP");
            }

            public static string WindowsProductName()
            {
                var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

                return (string) reg.GetValue("ProductName");
            }

            [Flags]
            public enum FileSystemFeature : uint
            {
                /// <summary>
                /// The file system supports case-sensitive file names.
                /// </summary>
                CaseSensitiveSearch = 1,

                /// <summary>
                /// The file system preserves the case of file names when it places a name on disk.
                /// </summary>
                CasePreservedNames = 2,

                /// <summary>
                /// The file system supports Unicode in file names as they appear on disk.
                /// </summary>
                UnicodeOnDisk = 4,

                /// <summary>
                /// The file system preserves and enforces access control lists (ACL).
                /// </summary>
                PersistentACLS = 8,

                /// <summary>
                /// The file system supports file-based compression.
                /// </summary>
                FileCompression = 0x10,

                /// <summary>
                /// The file system supports disk quotas.
                /// </summary>
                VolumeQuotas = 0x20,

                /// <summary>
                /// The file system supports sparse files.
                /// </summary>
                SupportsSparseFiles = 0x40,

                /// <summary>
                /// The file system supports re-parse points.
                /// </summary>
                SupportsReparsePoints = 0x80,

                /// <summary>
                /// The specified volume is a compressed volume, for example, a DoubleSpace volume.
                /// </summary>
                VolumeIsCompressed = 0x8000,

                /// <summary>
                /// The file system supports object identifiers.
                /// </summary>
                SupportsObjectIDs = 0x10000,

                /// <summary>
                /// The file system supports the Encrypted File System (EFS).
                /// </summary>
                SupportsEncryption = 0x20000,

                /// <summary>
                /// The file system supports named streams.
                /// </summary>
                NamedStreams = 0x40000,

                /// <summary>
                /// The specified volume is read-only.
                /// </summary>
                ReadOnlyVolume = 0x80000,

                /// <summary>
                /// The volume supports a single sequential write.
                /// </summary>
                SequentialWriteOnce = 0x100000,

                /// <summary>
                /// The volume supports transactions.
                /// </summary>
                SupportsTransactions = 0x200000,
            }

            //64-bit Erkennung von: http://stackoverflow.com/a/1840313
            [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
            public extern static IntPtr LoadLibrary(string libraryName);

            [DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
            public extern static IntPtr GetProcAddress(IntPtr hwnd, string procedureName);

            private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, [Out] out bool isWow64Process);

            public static bool IsOS64Bit()
            {
                if (IntPtr.Size == 8 || (IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private static IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
            {
                IntPtr handle = LoadLibrary("kernel32");

                if (handle != IntPtr.Zero)
                {
                    IntPtr fnPtr = GetProcAddress(handle, "IsWow64Process");

                    if (fnPtr != IntPtr.Zero)
                    {
                        return
                            (IsWow64ProcessDelegate)
                                Marshal.GetDelegateForFunctionPointer((IntPtr) fnPtr, typeof (IsWow64ProcessDelegate));
                    }
                }

                return null;
            }

            private static bool Is32BitProcessOn64BitProcessor()
            {
                IsWow64ProcessDelegate fnDelegate = GetIsWow64ProcessDelegate();

                if (fnDelegate == null)
                {
                    return false;
                }

                bool isWow64;
                bool retVal = fnDelegate.Invoke(Process.GetCurrentProcess().Handle, out isWow64);

                if (retVal == false)
                {
                    return false;
                }

                return isWow64;
            }

            //To Whoever at Microsoft thought it was a good idea that WOW64 get a redirected registry: If you're reading this: I don't like you. No, I don't. Really.
            //Info um vom 32-bit Kontext auf die 64-bit Registry zuzugreifen von: https://social.msdn.microsoft.com/Forums/en-US/da055767-0f69-4f07-b8e7-f3dce19f7ecb/windows-7-64bit-registry-access-using-x86-assembly?forum=netfx64bitv
            [DllImport("advapi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode,
                EntryPoint = "RegQueryValueExW", SetLastError = true)]
            static extern int RegQueryValueEx(UIntPtr hKey, string lpValueName, int lpReserved, out uint lpType,
                byte[] lpData, ref int lpcbData);

            [DllImport("advapi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode,
                EntryPoint = "RegOpenKeyExW", SetLastError = true)]
            static extern int RegOpenKeyEx(UIntPtr hKey, string subKey, uint options, int sam,
                out UIntPtr phkResult);

            static UIntPtr HKEY_CURRENT_USER = (UIntPtr) 0x80000001;
            static UIntPtr HKEY_LOCAL_MACHINE = (UIntPtr) 0x80000002;
            static int KEY_QUERY_VALUE = 0x0001;
            static int KEY_SET_VALUE = 0x0002;
            static int KEY_CREATE_SUB_KEY = 0x0004;
            static int KEY_ENUMERATE_SUB_KEYS = 0x0008;
            static int KEY_WOW64_64KEY = 0x0100;
            static int KEY_WOW64_32KEY = 0x0200;

            public static string GetWindowsCdKey()
            {
                UIntPtr regKeyHandle;
                if (
                    RegOpenKeyEx(HKEY_LOCAL_MACHINE, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", 0,
                        KEY_QUERY_VALUE | KEY_WOW64_64KEY, out regKeyHandle) == 0)
                {
                    uint type;
                    byte[] digitalProductId = new byte[2048];
                    int cbData = 2048;
                    if (RegQueryValueEx(regKeyHandle, "DigitalProductId", 0, out type, digitalProductId, ref cbData) ==
                        0)
                    {
                        var isWin8OrUp = (Environment.OSVersion.Version.Major == 6 && System.Environment.OSVersion.Version.Minor >= 2) || (Environment.OSVersion.Version.Major > 6);
                        if (isWin8OrUp)
                        {
                            string tmp = DecodeProductKeyWin8AndUp(digitalProductId);
                            if (tmp.EndsWith("3V66T") && IsWindows10())                 //If we're dealing with an updated Windows 10
                            {
                                return DecodeWindows10UpdateKey();
                            }
                            else
                            {
                                return tmp;
                            }
                        }
                        else
                        {
                            return DecodeXpStyleProductKey(digitalProductId);
                        }
                    }
                }
                throw new ApplicationException(
                    "Konnte CD-Key nicht bestimmen, da ein Registry-Query fehlgeschlagen ist...");
            }

            private static string DecodeWindows10UpdateKey()
            {
                UIntPtr regKeyHandle;
                if (
                    RegOpenKeyEx(HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Internet Explorer\Registration", 0,
                        KEY_QUERY_VALUE | KEY_WOW64_64KEY, out regKeyHandle) == 0)
                {
                    uint type;
                    byte[] digitalProductId = new byte[2048];
                    int cbData = 2048;
                    if (RegQueryValueEx(regKeyHandle, "DigitalProductId", 0, out type, digitalProductId, ref cbData) ==
                        0)
                    {
                        byte[] digitalProductId4 = new byte[2048];
                        cbData = 2048;
                        if (RegQueryValueEx(regKeyHandle, "DigitalProductId4", 0,out type,digitalProductId4,ref cbData) == 0)
                        {
                            string ascii =Encoding.ASCII.GetString(digitalProductId4, 0x378, 0x20).Replace("\0", string.Empty).Trim();
                            if (ascii.Contains("X15"))
                            {
                                return DecodeXpStyleProductKey(digitalProductId);
                            }
                            else
                            {
                                return DecodeProductKeyWin8AndUp(digitalProductId);
                            }
                        }
                    }
                }
                throw new ApplicationException(
                    "Konnte CD-Key nicht bestimmen, da ein Registry-Query fehlgeschlagen ist...");
            }

            /// <summary>
            /// Funktionieert laut CodeProject mit XP, 7, Office10 und Office11
            /// </summary>
            /// <param name="b"></param>
            /// <param name="digitalProductId"></param>
            /// <returns></returns>
            /// Geklaut von:
            /// <see cref="http://www.codeproject.com/Articles/23334/Microsoft-Product-Key-Finder"/>
            public static string DecodeXpStyleProductKey(byte[] digitalProductId)
            {
                // Offset of first byte of encoded product key in 
                //  'DigitalProductIdxxx" REG_BINARY value. Offset = 34H.
                const int keyStartIndex = 52;
                // Offset of last byte of encoded product key in 
                //  'DigitalProductIdxxx" REG_BINARY value. Offset = 43H.
                const int keyEndIndex = keyStartIndex + 15;
                // Possible alpha-numeric characters in product key.
                char[] digits = new char[]
      {
        'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P', 'Q', 'R', 
        'T', 'V', 'W', 'X', 'Y', '2', '3', '4', '6', '7', '8', '9',
      };
                // Length of decoded product key
                const int decodeLength = 29;
                // Length of decoded product key in byte-form.
                // Each byte represents 2 chars.
                const int decodeStringLength = 15;
                // Array of containing the decoded product key.
                char[] decodedChars = new char[decodeLength];
                // Extract byte 52 to 67 inclusive.
                ArrayList hexPid = new ArrayList();
                for (int i = keyStartIndex; i <= keyEndIndex; i++)
                {
                    hexPid.Add(digitalProductId[i]);
                }
                for (int i = decodeLength - 1; i >= 0; i--)
                {
                    // Every sixth char is a separator.
                    if ((i + 1) % 6 == 0)
                    {
                        decodedChars[i] = '-';
                    }
                    else
                    {
                        // Do the actual decoding.
                        int digitMapIndex = 0;
                        for (int j = decodeStringLength - 1; j >= 0; j--)
                        {
                            int byteValue = (digitMapIndex << 8) | (byte)hexPid[j];
                            hexPid[j] = (byte)(byteValue / 24);
                            digitMapIndex = byteValue % 24;
                            decodedChars[i] = digits[digitMapIndex];
                        }
                    }
                }
                return new string(decodedChars);
            }

            //geklaut von: http://stackoverflow.com/a/32139629
            public static string DecodeProductKeyWin8AndUp(byte[] digitalProductId)
            {
                var key = String.Empty;
                const int keyOffset = 52;
                var isWin8 = (byte)((digitalProductId[66] / 6) & 1);
                digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);

                // Possible alpha-numeric characters in product key.
                const string digits = "BCDFGHJKMPQRTVWXY2346789";
                int last = 0;
                for (var i = 24; i >= 0; i--)
                {
                    var current = 0;
                    for (var j = 14; j >= 0; j--)
                    {
                        current = current * 256;
                        current = digitalProductId[j + keyOffset] + current;
                        digitalProductId[j + keyOffset] = (byte)(current / 24);
                        current = current % 24;
                        last = current;
                    }
                    key = digits[current] + key;
                }
                var keypart1 = key.Substring(1, last);
                const string insert = "N";
                key = key.Substring(1).Replace(keypart1, keypart1 + insert);
                if (last == 0)
                    key = insert + key;
                for (var i = 5; i < key.Length; i += 6)
                {
                    key = key.Insert(i, "-");
                }
                return key;
            }
        }


        public bool IsLaptop()
        {
            ManagementClass systemEnclosures = new ManagementClass("Win32_SystemEnclosure");
            foreach (ManagementObject obj in systemEnclosures.GetInstances())
            {
                foreach (int i in (UInt16[]) (obj["ChassisTypes"]))
                {
					ChassisTypes actualType = (ChassisTypes)i;
					Console.WriteLine("Detected Chassis type: {0}", actualType);
					switch (actualType)
                    {
					case ChassisTypes.Desktop:
                            return false;
					case ChassisTypes.Notebook:
						return true;
                        default:
                            Console.WriteLine("Don't know what a {0} is - assuming it's a desktop computer.",
                                (ChassisTypes) i);
                            break;
                    }
                }
            }
            return false;
        }

        public enum ChassisTypes
        {
            Other = 1,
            Unknown,
            Desktop,
            LowProfileDesktop,
            PizzaBox,
            MiniTower,
            Tower,
            Portable,
            Laptop,
            Notebook,
            Handheld,
            DockingStation,
            AllInOne,
            SubNotebook,
            SpaceSaving,
            LunchBox,
            MainSystemChassis,
            ExpansionChassis,
            SubChassis,
            BusExpansionChassis,
            PeripheralChassis,
            StorageChassis,
            RackMountChassis,
            SealedCasePc
        }

        public bool IsVirtualMachine()
        {
            //TODO: Detect QEMU
            //TODO: Detect VirtualBox
            Log.Error("Virtual machine detection not yet supported. Assuming this is not a virtual machine.");
            return false;
        }

        public bool IsVirtualMachineHost()
        {
            //TODO: Detect QEMU
            //TODO: Detect VirtualBox
            Log.Error("Virtual machine host detection not yet supported. Assuming this is not a virtual machine host.");
            return false;
        }

        public bool IsServer()
        {
            return Win32KernelCalls.IsOS(Win32KernelCalls.OS_ANYSERVER);
        }

        public Cpu[] GetCpu()
        {
            List<Cpu> result = new List<Cpu>();
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_Processor");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Cpu child = new Cpu();
                    child.cores = (int) (uint) queryObj["NumberOfCores"];
                    child.description = string.Format("{0} --- {1}", (string) queryObj["Description"],
                        (string) queryObj["SocketDesignation"]);
                    child.frequency = (int) (uint) queryObj["CurrentClockSpeed"];
                    child.frequency_unit = Cpu.C__CATG__CPU_FREQUENCY_UNIT.C__FREQUENCY_UNIT__MHZ;
                    child.title = (string) queryObj["Name"];
                    child.cpuid = (string) queryObj["Manufacturer"];
                    //Laut Microsoft ist CPUID = Manufacturer (Warum auch immer...)
                    result.Add(child);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("-> An error occurred while querying for WMI data: " + e.Message);
            }
            return result.ToArray();
        }

        public List<StorageDevice> GetStorageDevices()
        {
            List<StorageDevice> result = new List<StorageDevice>();
            //C/DVD Laufwerke finden.
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_CDROMDrive");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    StorageDevice child = new StorageDevice();
                    string deviceId = (string) queryObj["DeviceID"];
                    if (!string.IsNullOrEmpty(deviceId))
                    {
                        child.connected = (deviceId).StartsWith("SCSI")
                            ? StorageDevice.ConnectionType.Extern
                            : StorageDevice.ConnectionType.Intern;
                    }
                    else
                    {
                        child.connected = StorageDevice.ConnectionType.Other;
                    }
                    child.description = (string) queryObj["Description"];
                    if (!Win32KernelCalls.IsWindowsXP()) child.serial = string.Format("{0}", queryObj["SerialNumber"]); //Seriennummern für DVD Laufwerke waren bei XP offenbar noch nicht erfunden...
                    child.title = (string) queryObj["Name"];
                    child.type = StorageDevice.StorageDeviceType.CDVD;
                    child.unit = StorageDevice.C__CATG__MEMORY_UNIT.C__MEMORY_UNIT__MB;
                    if (child.title.Contains("CLONEDRIVE")) continue; //Virtual CloneDrive juckt uns nicht...
                    result.Add(child);
                }
            }
            catch (Exception)
            {

                throw;
            }

            //Festplatten finden
            try
            {
                hardDisks = new List<StorageDevice>();
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    StorageDevice child = new StorageDevice();
                    string deviceId = (string)queryObj["DeviceID"];
                    if (!string.IsNullOrEmpty(deviceId))
                    {
                        child.connected = (deviceId).StartsWith("USB")
                            ? StorageDevice.ConnectionType.Extern
                            : StorageDevice.ConnectionType.Intern;
                    }
                    else
                    {
                        child.connected = StorageDevice.ConnectionType.Other;
                    }
                    child.description = (string) queryObj["Description"];
                    if (!Win32KernelCalls.IsWindowsXP()) child.serial = string.Format("{0}", queryObj["SerialNumber"]); //siehe oben
                    child.title = (string)queryObj["Caption"];
                    child.unit = StorageDevice.C__CATG__MEMORY_UNIT.C__MEMORY_UNIT__GB;
                    child.capacity = (double) ((long) (Convert.ToInt64(queryObj["Size"])/1000/1000/1000));
                    //Ja, 1000, nicht 1024, 
                    //zwischen Mebibyte und Megabyte ist ein Unterschied! ;)
                    //Oder habt ihr schon mal SSDs mit 111GB beworben gesehen?

                    string mediaType = (string) queryObj["MediaType"];
                    if (!string.IsNullOrEmpty(mediaType))
                    {
                        if (queryObj["MediaType"].ToString().Contains("Removable"))
                            //Gut, dass WMI Sprach-unabhängig immer Werte auf englisch angibt.
                        {
                            child.type = StorageDevice.StorageDeviceType.UsbStick;
                        }
                        else
                        {
                            child.type = StorageDevice.StorageDeviceType.HardDisk;
                        }
                    }
                    else
                    {
                        child.type = StorageDevice.StorageDeviceType.HardDisk;  //Unter Windows 10 lässt sich dies nicht mehr zuverlässig ermitteln, in dubio pro Festplatte.
                    }
                    result.Add(child);
                    hardDisks.Add(child);
                }
            }
            catch (Exception)
            {
                throw;
            }

            //Diskettenlaufwerke finden
			if (!Win32KernelCalls.IsWindows10 ()) {	//Windows 10 kann man nicht mehr nach Diskettenlaufwerken fragen...
				try {
					ManagementObjectSearcher searcher =
						new ManagementObjectSearcher ("root\\CIMV2",
							"SELECT * FROM Win32_FloppyDrive");

					foreach (ManagementObject queryObj in searcher.Get()) {
						StorageDevice child = new StorageDevice ();
						string deviceId = (string)queryObj ["DeviceID"];
						if (!string.IsNullOrEmpty (deviceId)) {
							child.connected = (deviceId).StartsWith ("USB")
                            ? StorageDevice.ConnectionType.Extern
                            : StorageDevice.ConnectionType.Intern;
						} else {
							child.connected = StorageDevice.ConnectionType.Other;
						}
						child.description = (string)queryObj ["Description"];
						child.title = (string)queryObj ["Name"];
						child.unit = StorageDevice.C__CATG__MEMORY_UNIT.C__MEMORY_UNIT__KB;
						child.type = StorageDevice.StorageDeviceType.Floppy;
						result.Add (child);
					}
				} catch (Exception) {

					throw;
				}
			}

            //Bandlaufwerke finden
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_TapeDrive");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    StorageDevice child = new StorageDevice();
                    child.connected = StorageDevice.ConnectionType.Other;
                    //Bei Tapes lässt sich nicht zuverlässig bestimmen, ob sie intern oder extern sind - da es wohl so oder so SCSI ist...
                    child.description = (string) queryObj["Description"];
                    //Zeigt zumindest an, ob es sich um LTO, DDS, oder Whatever-Tapes handelt.
                    child.title = (string) queryObj["Name"];
                    child.type = StorageDevice.StorageDeviceType.Tape;
                    child.unit = StorageDevice.C__CATG__MEMORY_UNIT.C__MEMORY_UNIT__GB;
                    //Hat heutzutage noch irgendwer was kleineres an Tapes? :o
                    result.Add(child);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public string GetVolumeSerialNumber(string volume)
        {
            StringBuilder volumeNameBuffStringBuilder = new StringBuilder();
            uint volumeSerialNumber, MaximumComponentLength;
            Win32KernelCalls.FileSystemFeature flags;
            StringBuilder filesystemNameBuffStringBuilder = new StringBuilder();

            try
            {
                Win32KernelCalls.GetVolumeInformation(volume, volumeNameBuffStringBuilder, Byte.MaxValue,
                    out volumeSerialNumber, out MaximumComponentLength, out flags, filesystemNameBuffStringBuilder,
                    Byte.MaxValue);

                return String.Format("{0:X}", volumeSerialNumber);
            }
            catch (Exception)
            {
                return String.Empty;
            }

        }

        public List<StorageDevice> hardDisks;

        public List<Drive> GetDrives(Idoit idoit)
        {
            if (hardDisks == null) GetStorageDevices();
            IdoitEnumerator fsEnumerator = idoit.Dialog(new Drive().Constant, "filesystem");
            //IdoitEnumerator onDeviceEnumerator = idoit.Dialog(new Drive().Constant, "device");    //Das funktioniert wegen einem Bug in der i-doit API leider nicht...

            List<Drive> result = new List<Drive>();
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                if (!di.IsReady) continue; //Wechseldatenträger und DVDs überspringen.

                Drive child = new Drive();
                child.mount_point = di.Name;
                child.title = di.VolumeLabel;
                child.system_drive =
                    Environment.GetEnvironmentVariable("windir")
                        .ToLowerInvariant()
                        .StartsWith(di.RootDirectory.FullName.ToLowerInvariant())
                        ? 1
                        : 0;
                if (fsEnumerator.TestTitle(di.DriveFormat))
                {
                    child.filesystem = fsEnumerator.FindTitleLike(di.DriveFormat);
                }
                else
                {
                    idoit.DialogEdit(child, "filesystem", di.DriveFormat);
                    fsEnumerator = idoit.Dialog(new Drive().Constant, "filesystem");
                    child.filesystem = fsEnumerator.FindTitleLike(di.DriveFormat);
                }
                child.capacity = (float) ((long) (di.TotalSize/1000/1000/1000));
                child.serial = GetVolumeSerialNumber(di.Name);
                child.unit = Drive.Unit.GB;
                //child.device lässt sich wegen o.g. Bug nicht korrekt setzen...
                result.Add(child);
            }
            return result;
        }

        public List<Memory> GetMemoryBanks(Idoit idoit)
        {
            List<Memory> result = new List<Memory>();
            IdoitEnumerator titles = idoit.Dialog(new Memory().Constant, "title");
            IdoitEnumerator manufactors = idoit.Dialog(new Memory().Constant, "manufacturer");
            IdoitEnumerator type = idoit.Dialog(new Memory().Constant, "type");

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_PhysicalMemory");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    string actualPartNumber = (string) queryObj["PartNumber"];                                      //ältere RAM Bänke geben evtl. null zurück  
                    if (!string.IsNullOrEmpty(actualPartNumber)) actualPartNumber = actualPartNumber.Trim();

                    string actualManufacturer = (string)queryObj["Manufacturer"];                                   //ältere RAM Bänke geben evtl. null zurück
                    if (!string.IsNullOrEmpty(actualManufacturer)) actualManufacturer.Trim();

                    if (IsRamManufacturerSpecified(actualManufacturer))
                    {
                        //Wenn kein Hersteller definiert, dann anhand der PartNumber den Hersteller herausfinden.
                        if (!string.IsNullOrEmpty(actualPartNumber))
                        {
                            foreach (var kv in Config.ramManufactors)
                            {
                                if (actualPartNumber.StartsWith(kv.Key))
                                {
                                    actualManufacturer = kv.Value;
                                    break;
                                }
                            }
                        }
                    }

                    Memory child = new Memory();
                    child.quantity = 1;
                    if (!titles.TestTitle(queryObj["BankLabel"].ToString()))
                    {
                        idoit.DialogEdit(new Memory(), "title", queryObj["BankLabel"].ToString());
                        titles = idoit.Dialog(new Memory().Constant, "title");
                    }
                    child.title = titles.FindTitleLike(queryObj["BankLabel"].ToString());

                    if (IsRamManufacturerSpecified(actualManufacturer))
                    {
                        if (!manufactors.TestTitle(actualManufacturer))
                        {
                            //Sollte der Hersteller des RAMs bekannt sein, und er ist weder in u-doit noch in i-doit bekannt, den RAM-Hersteller eintragen:
                            idoit.DialogEdit(new Memory(), "manufacturer", actualManufacturer);
                            manufactors = idoit.Dialog(new Memory().Constant, "manufacturer");
                        }

                        child.manufacturer = manufactors.FindTitleLike(actualManufacturer);
                    }

                    if (!string.IsNullOrEmpty(actualPartNumber))
                    {
                        if (!type.TestTitle(actualPartNumber))
                        {
                            idoit.DialogEdit(new Memory(), "type", actualPartNumber);
                            type = idoit.Dialog(new Memory().Constant, "type");
                        }
                        child.type = type.FindTitleLike(actualPartNumber);
                    }

                    child.capacity = (float) (Convert.ToInt64(queryObj["Capacity"].ToString())/1024/1024);
                    child.unit = Memory.C__CATG__MEMORY_UNIT.C__MEMORY_UNIT__MB;
                    child.description = queryObj["Description"].ToString();
                    result.Add(child);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        private static bool IsRamManufacturerSpecified(string actualManufacturer)
        {
            if (string.IsNullOrEmpty(actualManufacturer)) return false;
            if (actualManufacturer.Equals("Undefined")) return false;
            return true;
        }

        public List<SoftwareInfo> GetSoftware()
        {
            if (Win32KernelCalls.IsOS64Bit())
            {
                //64-bit OS
                List<SoftwareInfo> result = new List<SoftwareInfo>();
                try
                {
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher("root\\CIMV2",
                            "SELECT * FROM Win32_Product");

                    foreach (ManagementObject queryObj in searcher.Get())
                    {
                        SoftwareInfo child = new SoftwareInfo();
                        child.productName = (string) queryObj["Caption"];
                        child.installPath = (string) queryObj["InstallLocation"];
                        child.manufacterer = (string) queryObj["Vendor"];
                        child.registrationKey = "";
                        child.version = (string) queryObj["Version"];
                        result.Add(child);
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                //Der folgende Code listet alle 32-bit Applikation. (Das ist schon einfacher... :D)
                RegistryKey targetKey =
                    Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
                ScanSoftwareRegistryKey(targetKey, result);
                result.Sort((x, y) => (x.CompareTo(y)));
                return result;
            }
            else
            {
                //32-bit OS (trivial...)
                RegistryKey targetKey =
                    Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
                return ScanSoftwareRegistryKey(targetKey);
            }
        }

        private void ScanSoftwareRegistryKey(RegistryKey rk, List<SoftwareInfo> outputList)
        {
            foreach (string subKeyName in rk.GetSubKeyNames())
            {
                RegistryKey subkey = rk.OpenSubKey(subKeyName, false);
                string dn = (string) subkey.GetValue("DisplayName");
                if (!string.IsNullOrEmpty(dn))
                {
                    /*if (!outputList.Contains(dn))
                    {
                        outputList.Add(dn);
                    }*/
                    if (outputList.Find(x => x.productName.Equals(dn)) == null)
                    {
                        SoftwareInfo child = new SoftwareInfo();
                        child.productName = dn;
                        child.manufacterer = (string) subkey.GetValue("Publisher");
                        child.installPath = (string) subkey.GetValue("InstallLocation");
                        child.version = (string) subkey.GetValue("DisplayVersion");
                        outputList.Add(child);
                    }
                }
            }
        }

        private List<SoftwareInfo> ScanSoftwareRegistryKey(RegistryKey rk)
        {
            List<SoftwareInfo> result = new List<SoftwareInfo>();
            ScanSoftwareRegistryKey(rk, result);
            return result;
        }

        public List<Share> GetShares()
        {
            List<Share> result = new List<Share>();
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_Share");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (Convert.ToInt64(queryObj["Type"]) != 0) continue;  //Die administrativen Freigaben von Windows interessieren uns nicht.
                    Share child = new Share();
                    child.title = (string) queryObj["Caption"];
                    child.path = (string) queryObj["Path"];
                    child.unc_path = string.Format("\\\\{0}\\{1}", Environment.MachineName, (string)queryObj["Name"]);
                    if (child.unc_path.EndsWith("$")) continue;             //Die administrativen Freigaben von Windows XP interessieren uns auch nicht... (die rutschen bei der Abfrage oben dennoch durch.)
                    child.text_area = (string) queryObj["Description"];
                    result.Add(child);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return result;
        }

        public List<SoundCard> GetSoundCards(Idoit idoit)
        {
            IdoitEnumerator manufacEnum = idoit.Dialog(new SoundCard().Constant, "manufacturer");
            List<SoundCard> result = new List<SoundCard>();
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_SoundDevice");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    SoundCard child = new SoundCard();

                    //Wert für Hersteller herausfinden.
                    string manufacQuery = (string) queryObj["Manufacturer"];
                    if (!string.IsNullOrEmpty(manufacQuery))
                    {
                        if (!manufacEnum.TestTitle(manufacQuery))
                        {
                            //Hersteller hinzufügen, falls noch nicht bekannt...
                            idoit.DialogEdit(new SoundCard(), "manufacturer", manufacQuery);
                            manufacEnum = idoit.Dialog(new SoundCard().Constant, "manufacturer");
                        }
                        child.manufacturer = manufacEnum.FindTitleLike(manufacQuery);
                    }
                    child.title = (string) queryObj["Caption"];
                    child.description = (string) queryObj["Description"];
                    result.Add(child);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return result;
        }

        public string uname()
        {
            return Win32KernelCalls.WindowsProductName();
        }

        public bool OperatingSystemNeedsLicense()
        {
            return true;
        }

        public string OperatingSystemLicensingInfo()
        {
            return Win32KernelCalls.GetWindowsCdKey();
        }
    }
}