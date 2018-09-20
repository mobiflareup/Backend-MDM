using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MobiOcean.MDM.DAL.DAL.ProfileDALTableAdapters;

/// <summary>
/// Summary description for ProfileBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class ProfileBAL
    {
        tblProfileTableAdapter tblprofile;
        tblProfileFeatureMappingTableAdapter pfm;
        DataTable1TableAdapter profile;
        tblProfileTableAdapter prfile;
        DataTable2TableAdapter profileusrmpnghstry;
        tblGroupTableAdapter grp;
        ProgileUserGroupMappingTableAdapter prfileusrgrpmpng;
        tblProfileFeatureTimingTempTableAdapter tmpfeaturetiming;
        GetPhAndPrmsStngChangeForSMSAndEmailTableAdapter getphandprms;
        GetPhAndPrmsStngChangeForSMSAndEmailForAppTableAdapter getphandprmsApp;
        tblProfileBranchDeptMappingTableAdapter profilebranchdeptmpng;
        tblSensorTableAdapter Sensor;
        CheckFeaturesTableAdapter chkFeature;
        DataTable dt;

        private int _GroupId, _IsChanged, _ProfileFeatureTimingId, _NotificationOn, _LogOn, _AutoSyncOn, _DeviceId, _BranchId, _DeptId, _ProfileBranchDeptId;

        public int ProfileBranchDeptId
        {
            get { return _ProfileBranchDeptId; }
            set { _ProfileBranchDeptId = value; }
        }
        public int BranchId
        {
            get { return _BranchId; }
            set { _BranchId = value; }
        }
        public int DeptId
        {
            get { return _DeptId; }
            set { _DeptId = value; }
        }
        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        private int _ProfileGroupId;
        private int _ProfileId;
        private int _ClientId;
        private string _ProfileCode;
        private string _ProfileName;
        private string _ProfilePurpose;
        private int _Status;
        private string _LoggedBy;
        private string _RowVer;

        private int _ProfileFeatureMappingId;
        private int _FeatureId;
        private int _IsEnable;
        private int _FromDate;
        private int _ToDate;
        private string _FromTime;
        private string _ToTime;
        private string _AppliedDateTime, _CancelDateTime, _ProfileNo, _OldProfileNo, _Message;
        private int _IsDayControlled, _Duration;
        private int _IsTimeControlled;
        private int _IsDurationControlled;
        private int _ProfileUserHstryId, _UserId, _ProfileUserId, _FeatureStatus, _ProfileStatus, _WifiSensorId, _SensorId;
        int _PhStngDtlId;
        int _AlowFromDay;
        string _AlowFromTime;
        int _AlowToDay;
        string _AlowToTime;
        int _TotalDuration, _IsForUpdate;

        public int IsForUpdate
        {
            get { return _IsForUpdate; }
            set { _IsForUpdate = value; }
        }

        public int ProfileFeatureTimingId
        {
            get { return _ProfileFeatureTimingId; }
            set { _ProfileFeatureTimingId = value; }
        }
        public int PhStngDtlId
        {
            get { return _PhStngDtlId; }
            set { _PhStngDtlId = value; }
        }
        public int AlowFromDay
        {
            get { return _AlowFromDay; }
            set { _AlowFromDay = value; }
        }
        public string AlowFromTime
        {
            get { return _AlowFromTime; }
            set { _AlowFromTime = value; }
        }
        public int AlowToDay
        {
            get { return _AlowToDay; }
            set { _AlowToDay = value; }
        }
        public string AlowToTime
        {
            get { return _AlowToTime; }
            set { _AlowToTime = value; }
        }
        public int TotalDuration
        {
            get { return _TotalDuration; }
            set { _TotalDuration = value; }
        }
        public int IsChanged
        {
            get { return _IsChanged; }
            set { _IsChanged = value; }
        }
        public int GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value; }
        }
        public int ProfileGroupId
        {
            get { return _ProfileGroupId; }
            set { _ProfileGroupId = value; }
        }
        public string ProfileNo
        {
            get { return _ProfileNo; }
            set { _ProfileNo = value; }
        }
        public string OldProfileNo
        {
            get { return _OldProfileNo; }
            set { _OldProfileNo = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public int FeatureStatus
        {
            get { return _FeatureStatus; }
            set { _FeatureStatus = value; }
        }
        public int ProfileStatus
        {
            get { return _ProfileStatus; }
            set { _ProfileStatus = value; }
        }
        public int ProfileUserHstryId
        {
            get { return _ProfileUserHstryId; }
            set { _ProfileUserHstryId = value; }
        }
        public int ProfileUserId
        {
            get { return _ProfileUserId; }
            set { _ProfileUserId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public string ProfileCode
        {
            get { return _ProfileCode; }
            set { _ProfileCode = value; }
        }
        public string ProfileName
        {
            get { return _ProfileName; }
            set { _ProfileName = value; }
        }
        public string ProfilePurpose
        {
            get { return _ProfilePurpose; }
            set { _ProfilePurpose = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string LoggedBy
        {
            get { return _LoggedBy; }
            set { _LoggedBy = value; }
        }
        public string RowVer
        {
            get { return _RowVer; }
            set { _RowVer = value; }
        }

        public int ProfileFeatureMappingId
        {
            get { return _ProfileFeatureMappingId; }
            set { _ProfileFeatureMappingId = value; }
        }
        public int FeatureId
        {
            get { return _FeatureId; }
            set { _FeatureId = value; }
        }
        public int IsEnable
        {
            get { return _IsEnable; }
            set { _IsEnable = value; }
        }
        public int NotificationOn
        {
            get { return _NotificationOn; }
            set { _NotificationOn = value; }
        }
        public int LogOn
        {
            get { return _LogOn; }
            set { _LogOn = value; }
        }
        public int AutoSyncOn
        {
            get { return _AutoSyncOn; }
            set { _AutoSyncOn = value; }
        }
        public int FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public int ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public string FromTime
        {
            get { return _FromTime; }
            set { _FromTime = value; }
        }
        public string ToTime
        {
            get { return _ToTime; }
            set { _ToTime = value; }
        }
        public int Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }
        public int IsDayControlled
        {
            get { return _IsDayControlled; }
            set { _IsDayControlled = value; }
        }
        public int IsTimeControlled
        {
            get { return _IsTimeControlled; }
            set { _IsTimeControlled = value; }
        }
        public int IsDurationControlled
        {
            get { return _IsDurationControlled; }
            set { _IsDurationControlled = value; }
        }
        public string AppliedDateTime
        {
            get { return _AppliedDateTime; }
            set { _AppliedDateTime = value; }
        }
        public string CancelDateTime
        {
            get { return _CancelDateTime; }
            set { _CancelDateTime = value; }
        }

        public int WifiSensorId
        {
            get { return _WifiSensorId; }
            set { _WifiSensorId = value; }
        }
        public int SensorId
        {
            get { return _SensorId; }
            set { _SensorId = value; }
        }

        public int UserCount { get; set; }
        public string ProfileBranchDeptIds { get; set; }

        public ProfileBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public bool CheckEnableFeatures()
        {
            chkFeature = new CheckFeaturesTableAdapter();
            try
            {
                return Convert.ToBoolean(chkFeature.CheckEnableFeatures(FeatureId, ClientId, ProfileId));
            }
            catch
            {
                return false;
            }
        }

        public bool CheckEnableFeaturesFromUSER()
        {
            chkFeature = new CheckFeaturesTableAdapter();
            try
            {
                return Convert.ToBoolean(chkFeature.CheckEnableFeaturesFromUSER(ClientId, ProfileId, UserCount));
            }
            catch
            {
                return false;
            }
        }

        public bool CheckEnableFeaturesFromBrnandDpt()
        {
            chkFeature = new CheckFeaturesTableAdapter();
            try
            {
                return Convert.ToBoolean(chkFeature.CheckEnableFeaturesFromBrnandDpt(ClientId, ProfileId, ProfileBranchDeptIds));
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetProfileByClient()
        {
            try
            {
                tblprofile = new tblProfileTableAdapter();
                return tblprofile.GetProfileFeatureMapping(_ClientId);
            }
            finally
            {
                tblprofile = null;
            }

        }
        public DataTable GetProfileData()
        {
            try
            {
                tblprofile = new tblProfileTableAdapter();
                return tblprofile.GetProfileData(_ClientId);
            }
            finally
            {
                tblprofile = null;
            }

        }
        public DataTable GetProfileByClientUpdate()
        {
            try
            {
                tblprofile = new tblProfileTableAdapter();
                return tblprofile.GetProfileByClientUpdate(_ClientId, _DeviceId);
            }
            finally
            {
                tblprofile = null;
            }
        }
        public DataTable GetProfileForApp()
        {
            try
            {
                tblprofile = new tblProfileTableAdapter();
                return tblprofile.GetProfileForApp(_ClientId, _DeviceId, _IsForUpdate);
            }
            finally
            {
                tblprofile = null;
            }
        }
        public DataTable GetProfileFeatureMapping()
        {
            pfm = new tblProfileFeatureMappingTableAdapter();
            return pfm.GetProfileFeatureMappingData();
        }
        public DataTable getdata()
        {
            profile = new DataTable1TableAdapter();
            dt = new DataTable();
            try
            {
                dt = profile.GetData();
                return dt;
            }
            finally
            {
                dt = null;
                profile = null;
            }
        }
        public string InsertProfileData()
        {
            prfile = new tblProfileTableAdapter();
            return prfile.InsertProfileDtls(_ClientId, _ProfileId, _ProfileCode, _ProfileName, _ProfilePurpose, _UserId).ToString();
        }
        public int DeleteProfile()
        {
            prfile = new tblProfileTableAdapter();
            return Convert.ToInt32(prfile.DeleteProfileDtls(_ProfileId, _UserId).ToString());
        }
        public DataTable GetProfileUsrMappingHstry()
        {
            profileusrmpnghstry = new DataTable2TableAdapter();
            return profileusrmpnghstry.GetData();
        }
        public DataTable GetGroupName()
        {
            grp = new tblGroupTableAdapter();
            return grp.GetGroupName(_ClientId);
        }
        public DataTable GetProfileUserGrpMappingData()
        {
            prfileusrgrpmpng = new ProgileUserGroupMappingTableAdapter();
            return prfileusrgrpmpng.GetProfileUserGrpMappingData(_ClientId);
        }
        public int DeleteProfileUsrGrpDtls()
        {
            prfileusrgrpmpng = new ProgileUserGroupMappingTableAdapter();
            return prfileusrgrpmpng.DeleteProfileUsrGrpDtls(_ProfileGroupId);
        }
        public string spProfileUsrGrpMpng()
        {
            prfileusrgrpmpng = new ProgileUserGroupMappingTableAdapter();
            return prfileusrgrpmpng.spProfileUsrGrpMpng(_ProfileGroupId, _ClientId, _GroupId, _ProfileId, _IsEnable, _AppliedDateTime, _LoggedBy, _Status).ToString();
        }
        public string MoveDatainTempTable()
        {
            try
            {
                pfm = new tblProfileFeatureMappingTableAdapter();
                pfm.MoveDatainTempTable(_ClientId, _ProfileId);
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public string MoveDatainOriginalTables()
        {
            try
            {
                pfm = new tblProfileFeatureMappingTableAdapter();
                pfm.MoveDatainOriginalTables(_ProfileId, Convert.ToInt32(_LoggedBy));
                return "1";
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public DataTable GetProfileftrtimingfromTemptable()
        {
            tmpfeaturetiming = new tblProfileFeatureTimingTempTableAdapter();
            return tmpfeaturetiming.GetProfileftrtimingfromTemptable(_ProfileFeatureMappingId);
        }
        public int IU_ProfileFeatureMapping()
        {
            try
            {
                tmpfeaturetiming = new tblProfileFeatureTimingTempTableAdapter();
                return Convert.ToInt32(tmpfeaturetiming.IU_ProfileFeatureMapping(_ProfileFeatureMappingId, _ClientId, _ProfileId, _FeatureId, _IsEnable, int.Parse(_LoggedBy)).ToString());

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int IU_ProfileFetaureON()
        {
            try
            {
                tmpfeaturetiming = new tblProfileFeatureTimingTempTableAdapter();
                return Convert.ToInt32(tmpfeaturetiming.IU_ProfileFetaureON(_ClientId, _ProfileId, _FeatureId, _IsEnable, _NotificationOn, _LogOn, _AutoSyncOn).ToString());

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int IU_ProfileFetaureONwithChange()
        {
            try
            {
                tmpfeaturetiming = new tblProfileFeatureTimingTempTableAdapter();
                return Convert.ToInt32(tmpfeaturetiming.IU_ProfileFetaureONwithChange(_ClientId, _ProfileId, _FeatureId, _IsEnable, _NotificationOn, _LogOn, _AutoSyncOn, _IsChanged).ToString());

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int IU_FeatureTimingDtl()
        {
            try
            {
                tmpfeaturetiming = new tblProfileFeatureTimingTempTableAdapter();
                return Convert.ToInt32(tmpfeaturetiming.IU_FeatureTimingDtl(_ProfileFeatureTimingId, _ProfileFeatureMappingId, _AlowFromDay, _AlowFromTime, _AlowToDay, _AlowToTime, _TotalDuration, _IsDayControlled, _IsTimeControlled, _IsDurationControlled, int.Parse(_LoggedBy), _GroupId).ToString());

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int ChangeTempTimingStatus()
        {
            try
            {
                tmpfeaturetiming = new tblProfileFeatureTimingTempTableAdapter();
                tmpfeaturetiming.ChangeTempTimingStatus(_ProfileFeatureTimingId);
                return 1;

            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DataTable GetPhAndPrmsStngChangeForSMSAndEmail()
        {
            getphandprms = new GetPhAndPrmsStngChangeForSMSAndEmailTableAdapter();
            return getphandprms.GetPhAndPrmsStngChangeForSMSAndEmail(_ProfileFeatureMappingId);


        }
        public DataTable GetPhAndPrmsStngChangeForSMSAndEmailForApp()
        {
            getphandprmsApp = new GetPhAndPrmsStngChangeForSMSAndEmailForAppTableAdapter();
            return getphandprmsApp.GetPhAndPrmsStngChangeForSMSAndEmailForApp(_ProfileFeatureMappingId);


        }
        public int UpdateMsg()
        {
            tmpfeaturetiming = new tblProfileFeatureTimingTempTableAdapter();
            return tmpfeaturetiming.UpdateMsg(_Message, _ProfileFeatureMappingId);


        }
        public DataTable GetProfileDtls()
        {
            tblprofile = new tblProfileTableAdapter();
            return tblprofile.sp_CountUser(_ClientId);
        }
        public DataTable GetCreationDateByProfileId()
        {
            tblprofile = new tblProfileTableAdapter();
            return tblprofile.GetCreationDateByProfileId(_ProfileId);
        }
        public DataTable GetProfileBranchDeptMapping()
        {
            profilebranchdeptmpng = new tblProfileBranchDeptMappingTableAdapter();
            return profilebranchdeptmpng.GetProfileBranchDeptMapping();
        }
        public string spProfileBranchDeptMapping()
        {
            profilebranchdeptmpng = new tblProfileBranchDeptMappingTableAdapter();
            return profilebranchdeptmpng.spProfileBranchDeptMapping(_ProfileBranchDeptId, _ClientId, _ProfileId, _IsEnable, _AppliedDateTime, _LoggedBy, _BranchId, _DeptId, _Status).ToString();
        }
        public DataTable GetUserNameAndDeviceName()
        {
            profilebranchdeptmpng = new tblProfileBranchDeptMappingTableAdapter();
            return profilebranchdeptmpng.GetUserNameAndDeviceName();
        }
        public DataTable GetSensorNameByClientId()
        {
            Sensor = new tblSensorTableAdapter();
            return Sensor.GetSensorNameByClientId(_ClientId);
        }
        public DataTable GetWifiSensorDetails()
        {
            Sensor = new tblSensorTableAdapter();
            return Sensor.GetWifiSensorDetails(_ClientId);
        }
        public int InsertWifiSensor()
        {
            Sensor = new tblSensorTableAdapter();
            return Convert.ToInt32(Sensor.InsertWifiSensor(_WifiSensorId, _ProfileId, _ClientId, _SensorId, _UserId).ToString());
        }
        public int DeleteWifiSensorDetails()
        {
            Sensor = new tblSensorTableAdapter();
            return Convert.ToInt32(Sensor.DeleteWifiSensorDetails(_WifiSensorId));
        }
        public int SpGetProfileId()
        {
            tblprofile = new tblProfileTableAdapter();
            return Convert.ToInt32(tblprofile.SpGetProfileId(_DeviceId, _UserId).ToString());
        }
    }
}
