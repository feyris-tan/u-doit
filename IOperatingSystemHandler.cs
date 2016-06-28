using System.Collections.Generic;
using u_doit.I_DoIt;
using u_doit.I_DoIt.Objects.Categories;
using u_doit.Objects.Categories;

namespace u_doit
{
    interface IOperatingSystemHandler
    {
        bool IsLaptop();
        bool IsVirtualMachine();
        bool IsVirtualMachineHost();
        bool IsServer();
        Cpu[] GetCpu();
        List<StorageDevice> GetStorageDevices();
        List<Drive> GetDrives(IIdoit idoit);
        List<Memory> GetMemoryBanks(IIdoit idoit);
        List<SoftwareInfo> GetSoftware();
        List<Share> GetShares();
        List<SoundCard> GetSoundCards(IIdoit idoit);
        string uname();
        bool OperatingSystemNeedsLicense();
        string OperatingSystemLicensingInfo();
    }
}