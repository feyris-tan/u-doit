using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt.Objects.Categories
{
    [CategoryDisplayName("Directly Attached Storage Device")]
    [CategoryId("C__CMDB__SUBCAT__STORAGE__DEVICE")]
    class StorageDevice : Category
    {
        [JsonConverter(typeof(EnumDeserializer))]
        public StorageDeviceType type;

        public string title;

        [JsonConverter(typeof(FloatDeserializer))]
        public double capacity;

        [JsonConverter(typeof(EnumDeserializer))]
        public C__CATG__MEMORY_UNIT unit;

        [JsonConverter(typeof(EnumDeserializer))]
        public ConnectionType connected;

        public string serial;
        public string description;

        public enum StorageDeviceType
        {
            CDVD = 3,
            Floppy = 2,
            HardDisk = 1,
            Tape = 5,
            UsbStick = 4
        }

        public enum C__CATG__MEMORY_UNIT : int
        {
            C__MEMORY_UNIT__KB = 1,
            C__MEMORY_UNIT__MB = 2,
            C__MEMORY_UNIT__GB = 3,
            C__MEMORY_UNIT__TB = 4,
            C__MEMORY_UNIT__B = 1000,
        }

        public enum ConnectionType : int
        {
            Extern = 1,
            Intern = 2,
            Other = 3,
        }
    }
}
