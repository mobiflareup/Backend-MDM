using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.SensorDALTableAdapters;

/// <summary>
/// Summary description for SensorBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class SensorBAL
    {
        WifiSensorTableAdapter Sensor;
        tblSensorEnableTableAdapter sensorenable;
        private int _ClientId;
        public int SensorId { get; set; }
        public int BranchId { get; set; }
        public int DepartmentId { get; set; }
        public int ProfileId { get; set; }
        public string SensorName { get; set; }
        public string BSSID { get; set; }
        public string SSID { get; set; }
        public string Password { get; set; }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public string ProfileCode { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePurpose { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public string AppId { get; set; }
        public string LogDateTime { get; set; }
        public int IsSensor { get; set; }
        public int WifiSensorId { get; set; }
        public int profileId { get; set; }
        public string sensorIdlist { get; set; }
        public int Isenable { get; set; }
        public int ProfileBranchDeptId { get; set; }
        public int Checked { get; set; }
        public string Descripition { get; set; }
        public string createdby { get; set; }

        public SensorBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetWifiSensorDetails()
        {
            Sensor = new WifiSensorTableAdapter();
            return Sensor.GetWifiSensorDtls(_ClientId);
        }
        public DataTable GetSensorDetails()
        {
            Sensor = new WifiSensorTableAdapter();
            return Sensor.GetSensorDetails(_ClientId);
        }
        public int UpdateSensor()
        {
            Sensor = new WifiSensorTableAdapter();
            return Convert.ToInt32(Sensor.UpdateSensor(SensorId, ClientId, BranchId, DepartmentId, SensorName, Description, BSSID, SSID, Password, CreatedBy).ToString());
        }
        public int DeleteSensor()
        {
            Sensor = new WifiSensorTableAdapter();
            return Sensor.spDeleteSensorDetails(SensorId);
        }
        public int InsertIntoSensorEnable()
        {
            sensorenable = new tblSensorEnableTableAdapter();
            return sensorenable.InsertintoSensorEnable(UserId, _ClientId, DeviceId, SensorId, AppId, LogDateTime, IsSensor);
        }
        public DataTable GetmobileNos()
        {
            Sensor = new WifiSensorTableAdapter();
            return Sensor.Mobilenolst(_ClientId);
        }
        public int AssignSensorToProfile()
        {
            Sensor = new WifiSensorTableAdapter();
            return Convert.ToInt32(Sensor.AssignSensorToProfile(WifiSensorId, _ClientId, UserId, Isenable).ToString());
        }
        public DataTable GetBranchDeptData()
        {
            Sensor = new WifiSensorTableAdapter();
            return Sensor.GetBranchDeptDetails(_ClientId);
        }
        public int AssignBranchDeptToProfile()
        {
            Sensor = new WifiSensorTableAdapter();
            return Convert.ToInt32(Sensor.AssignBranchDeptToProfile(ProfileBranchDeptId, ClientId, Checked, Convert.ToInt32(UserId)));
        }
        public DataTable GetSensoreDetails()
        {
            Sensor = new WifiSensorTableAdapter();
            try
            {
                return Sensor.GetSensorDetailForApp(_ClientId);
            }
            finally
            {
                Sensor = null;
            }
        }
        public int insertSensor()
        {
            sensorenable = new tblSensorEnableTableAdapter();
            return Convert.ToInt32(sensorenable.InsertSensor(SensorId, ClientId, BranchId, DepartmentId, SensorName, Descripition, BSSID, SSID, Password, createdby).ToString());
        }
    }
    public class sensor
    {
        public string sensorname { get; set; }
        public string sensorvalue { get; set; }
    }
    public class SensorDetails
    {
        public int ProfileId { get; set; }
        public string BSSID { get; set; }
        public string SSID { get; set; }
        public string Password { get; set; }
    }
}
