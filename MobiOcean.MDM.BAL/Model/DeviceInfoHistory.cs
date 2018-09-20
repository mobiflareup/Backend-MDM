using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DeviceInfoHistory
/// </summary>
/// 
namespace MobiOcean.MDM.BAL.Model
{
    public class DeviceInfoHistory
    {
        public int DeviceId { get; set; }        
        public string LogDateTime { get; set; }        
        public string Manufacturer { get; set; }
        public string UsrName { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceName { get; set; }
        public string OSVersion { get; set; }
        public string APILevel { get; set; }
        public string BuildID { get; set; }
        public string Processor { get; set; }
        public string ProcessorCores { get; set; }
        public string MaxFrequency { get; set; }
        public string InstructionSets { get; set; }
        public string SIMDInstructions { get; set; }
        public string FrontCamera { get; set; }
        public string RearCamera { get; set; }
        public string Bluetooth { get; set; }
        public string WiFi { get; set; }
        public string Sensors { get; set; }
        public string NFC { get; set; }
        public string USBPort { get; set; }
        public string Technology { get; set; }
        public string InternalMemory { get; set; }
        public string ExternalMemory { get; set; }
        public string IsExternalSDCard { get; set; }
        public string RAMSize { get; set; }
        public string AvailableRAMSize { get; set; }
        public string JVMMaxMemory { get; set; }
        public string TelecomProvider1 { get; set; }
        public string TelecomProvider2 { get; set; }       
        public string IMEINo1 { get; set; }
        public string IMEINo2 { get; set; }
        public string SIMNo1 { get; set; }
        public string SIMNo2 { get; set; }
        public string IsDualSIM { get; set; }
        public string IsSIM1Ready { get; set; }
        public string IsSIM2Ready { get; set; }
        public string AppId { get; set; }

    }
}