using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.DeviceInfoDALTableAdapters;
using MobiOcean.MDM.BAL.Model;
/// <summary>
/// Summary description for DeviceInfoBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class DeviceInfoBAL : ConstantBAL
    {

        tblDeviceInfoHistoryTableAdapter insrtAll;
        InsertBatteryInfoTableAdapter batterinfo;

        public NetworkInfo networkinfo { get; set; }
        public MdMVersionInfo mdmversioninfo { get; set; }
        public InternetConnectivity internetconnectivity { get; set; }
        public BatteryInfo batteryinfo { get; set; }
        public DeviceInfoHistory deviceinfo { get; set; }
        public string AppId { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public int ClientId { get; set; }
        public string LogDateTime { get; set; }

        public bool IsDeviceRouted { get; set; }

        public DeviceInfoBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string InsertDeviceInfo()
        {
            insrtAll = new tblDeviceInfoHistoryTableAdapter();
            return insrtAll.InsertDeviceInfo(deviceinfo.DeviceId, deviceinfo.UsrName, deviceinfo.DeviceModel, deviceinfo.DeviceName, deviceinfo.OSVersion, deviceinfo.APILevel, deviceinfo.BuildID, deviceinfo.Processor, deviceinfo.ProcessorCores, deviceinfo.MaxFrequency, deviceinfo.InstructionSets, deviceinfo.SIMDInstructions, deviceinfo.FrontCamera, deviceinfo.RearCamera, deviceinfo.Bluetooth, deviceinfo.WiFi, deviceinfo.Sensors, deviceinfo.NFC, deviceinfo.USBPort, deviceinfo.Technology, deviceinfo.InternalMemory, deviceinfo.ExternalMemory, deviceinfo.IsExternalSDCard, deviceinfo.RAMSize, deviceinfo.AvailableRAMSize, deviceinfo.JVMMaxMemory, deviceinfo.TelecomProvider1, deviceinfo.TelecomProvider2, deviceinfo.IMEINo1, deviceinfo.IMEINo2, deviceinfo.SIMNo1, deviceinfo.SIMNo2, deviceinfo.IsDualSIM, deviceinfo.IsSIM1Ready, deviceinfo.IsSIM2Ready, deviceinfo.LogDateTime, deviceinfo.Manufacturer).ToString();
        }

        public DataTable VodafoneNos(int vodaFoneService)
        {
            insrtAll = new tblDeviceInfoHistoryTableAdapter();
            return insrtAll.GetVodaFoneNos(vodaFoneService);
        }
        //public void UpdateDeviceLocation(int clientId, int userId, string mobileno, string latitude, string longitude, string Provider, string AppId, string Location)
        //{
        //    insrtAll = new tblDeviceInfoHistoryTableAdapter();
        //    insrtAll.UpdateDeviceLocation(clientId, userId, latitude, longitude, Provider, AppId, Location, GetCurrentDateTimeByUserId(userId).ToString("dd-MMM-yyyy HH:mm"));
        //}
        //public DataTable GetUserDeviceDetailsByMobileNo(string MobileNo)
        //{
        //    insrtAll = new tblDeviceInfoHistoryTableAdapter();
        //    return insrtAll.GetUserDeviceDetailsByMobileNo(MobileNo);
        //}
        public string InsertBatteryInfo()
        {
            insrtAll = new tblDeviceInfoHistoryTableAdapter();
            return insrtAll.InsertBatteryInfo(batteryinfo.DeviceId, batteryinfo.Battery_Info, batteryinfo.Voltage, batteryinfo.Temperature, batteryinfo.BatteryPercent, batteryinfo.BatteryStatus, batteryinfo.BatteryHealth, batteryinfo.LogDateTime).ToString();
        }


        public string InsertInternetConnectivity()
        {
            insrtAll = new tblDeviceInfoHistoryTableAdapter();
            return insrtAll.InsertInternetConnectivityInfo(internetconnectivity.DeviceId, internetconnectivity.ConnectedToWiFi, internetconnectivity.ConnectedToMobile, internetconnectivity.ConnectivityName, internetconnectivity.ConnectivityType, internetconnectivity.LogDateTime).ToString();
        }


        public string InsertMdMVersion()
        {
            insrtAll = new tblDeviceInfoHistoryTableAdapter();
            return insrtAll.InsertMdMVersionInfo(mdmversioninfo.DeviceId, mdmversioninfo.ClientVersionMDM, mdmversioninfo.dateTime).ToString();
        }
        public string InsertMdMVersion1()
        {
            insrtAll = new tblDeviceInfoHistoryTableAdapter();
            return insrtAll.InsertMdMVersionInfo1(mdmversioninfo.DeviceId, mdmversioninfo.ClientVersionMDM, mdmversioninfo.AppTypeId, mdmversioninfo.dateTime, mdmversioninfo.InstalledStatus).ToString();
        }

        public string InsertNetworkInfo()
        {
            insrtAll = new tblDeviceInfoHistoryTableAdapter();
            return insrtAll.InsertNetworkInfo(networkinfo.DeviceId, networkinfo.NetworkStrength, networkinfo.NetworkTypeInfo, networkinfo.NetworkStatus, networkinfo.Roaming, networkinfo.IsConnectedToProvisioningNetwork, networkinfo.LogDateTime).ToString();
        }
        public int IU_IsDeviceRouted()
        {
            insrtAll = new tblDeviceInfoHistoryTableAdapter();
            return insrtAll.IU_IsDeviceRouted(UserId, ClientId, DeviceId, IsDeviceRouted, LogDateTime);
        }

        public DataTable InsertBatteryInfo1()
        {
            batterinfo = new InsertBatteryInfoTableAdapter();
            return batterinfo.InsertBatteryInfo1(batteryinfo.DeviceId, batteryinfo.Battery_Info, batteryinfo.Voltage, batteryinfo.Temperature, batteryinfo.BatteryPercent, batteryinfo.BatteryStatus, batteryinfo.BatteryHealth, batteryinfo.LogDateTime);
        }

        public DateTime? LastSMSSentTime()
        {
            batterinfo = new InsertBatteryInfoTableAdapter();
            DateTime? date = null;
            try
            {
                DataTable dt = new DataTable();
                dt = batterinfo.GetUpdatedSMSDateTime(DeviceId);
                date = Convert.ToDateTime(dt.Rows[0]["LogDateTime"]);
            }
            catch (Exception){ }
            return date;
        }

        public void InsertOrUpdateBatterInfoSMS()
        {
            batterinfo = new InsertBatteryInfoTableAdapter();
            batterinfo.InsertOrUpdateBatterInfoSMS(DeviceId,ClientId);
        }
        public void InsertOrUpdateBatterInfoSMS1()
        {
            batterinfo = new InsertBatteryInfoTableAdapter();
            batterinfo.InsertOrUpdateBatterInfoSMS1(DeviceId, ClientId);
        }
        public DataTable CheckActiveStatusofDeviceBatterInfo()
        {
            batterinfo = new InsertBatteryInfoTableAdapter();
            return batterinfo.CheckActiveStatusofDeviceBatterInfo();
        }       
    }
}
