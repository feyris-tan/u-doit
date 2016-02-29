using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt.Objects.Categories
{
    [CategoryId("C__CATG__DRIVE")]
    [CategoryDisplayName("Drive")]
    class Drive : Category
    {
        public string mount_point;
        public string title;

        [JsonConverter(typeof(BrokenInt32Deserializer))]
        public int system_drive;

        [DialogName("isys_filesystem_type__id")]
        [JsonConverter(typeof(EnumDeserializer))]
        public int filesystem;

        [DialogName("isys_memory_unit__id")]
        [JsonConverter(typeof(EnumDeserializer))]
        public Unit unit;

        [JsonConverter(typeof(FloatDeserializer))]
        public float capacity;

        [JsonConverter(typeof(BrokenInt32Deserializer))]
        public int assigned_raid;

        [JsonConverter(typeof(BrokenInt32Deserializer))]
        public int drive_type;

        [JsonConverter(typeof(EnumDeserializer))]
        public int device;

        [JsonConverter(typeof(BrokenInt32Deserializer))]
        public int raid;

        [JsonConverter(typeof(BrokenInt32Deserializer))]
        public int ldev;

        public string description;
        public string serial;

        public enum Filesystem : int
        {
            EXT2 = 6,
            EXT3 = 7,
            FAT = 1,
            FAT32 = 2,
            HPFS = 8,
            LinuxSwap = 4,
            NSS = 9,
            NTFS = 3,
            ReiserFS = 5,   //Das Dateisystem, dass deine Waifu killt :P
        }

        public enum Unit : int
        {
            GB = 3
        }
    }
}
