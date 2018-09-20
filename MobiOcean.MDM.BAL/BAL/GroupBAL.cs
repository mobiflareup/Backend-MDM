using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.GroupDALTableAdapters;
using MobiOcean.MDM.BAL.Model;

/// <summary>
/// Summary description for GroupBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class GroupBAL //: ConstantBAL
    {
        tblChatGroupTableAdapter chatGroup;
        tblAppGroupDeviceMappingTableAdapter appgrpta;
        tblGroupTableAdapter group;
        tblAppMasterTableAdapter appmaster;
        tblAppGroupTableAdapter AppGrp;
        tblChatAppTableAdapter chatapp;
        LocReqfreqTableAdapter locreqfreq;
        LocReqFreqHstryTableAdapter locreqfreqhstry;
        DataTable dt, dt1;
        GeofenceFreqTableAdapter geofence;
        GeoFenceTableAdapter geo;

        private int _ClientId, _DeviceId, _UserId, _GroupDtlId, _ChatGroupId, _AppGroupDeviceId, _IsEnable, _GroupId, _ApplicationId, _IsForUpdate, _LocReqFreqId, _LocReqFrequency, _GeoFenceReqFreqId, _ProfileId;
        private string _Description, _GroupCode, _GroupName, _GrouppName, _AppId, _Message, _UserIdList, _LoggedBy, _AppCode, _AppName, _GroupCount, _FrmTime, _ToTime, _Lat, _Lng, _Name;
        private double _Radius;

        public DateTime currentDateTime { get; set; }
        public int ProfileId
        {
            get { return _ProfileId; }
            set { _ProfileId = value; }
        }
        public int GeoFenceReqFreqId
        {
            get { return _GeoFenceReqFreqId; }
            set { _GeoFenceReqFreqId = value; }
        }
        public double Radius
        {
            get { return _Radius; }
            set { _Radius = value; }
        }
        public string Lat
        {
            get { return _Lat; }
            set { _Lat = value; }
        }
        public string Lng
        {
            get { return _Lng; }
            set { _Lng = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string FrmTime
        {
            get { return _FrmTime; }
            set { _FrmTime = value; }
        }
        public string ToTime
        {
            get { return _ToTime; }
            set { _ToTime = value; }
        }
        public string GroupCount
        {
            get { return _GroupCount; }
            set { _GroupCount = value; }
        }
        public int IsForUpdate
        {
            get { return _IsForUpdate; }
            set { _IsForUpdate = value; }
        }
        public string UserIdList
        {
            get { return _UserIdList; }
            set { _UserIdList = value; }
        }
        public string LoggedBy
        {
            get { return _LoggedBy; }
            set { _LoggedBy = value; }
        }
        public int IsEnable
        {
            get { return _IsEnable; }
            set { _IsEnable = value; }
        }
        public int GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value; }
        }
        public int LocReqFreqId
        {
            get { return _LocReqFreqId; }
            set { _LocReqFreqId = value; }
        }
        public int LocReqFrequency
        {
            get { return _LocReqFrequency; }
            set { _LocReqFrequency = value; }
        }
        public int ApplicationId
        {
            get { return _ApplicationId; }
            set { _ApplicationId = value; }
        }
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public int AppGroupDeviceId
        {
            get { return _AppGroupDeviceId; }
            set { _AppGroupDeviceId = value; }
        }
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }
        public int ChatGroupId
        {
            get { return _ChatGroupId; }
            set { _ChatGroupId = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public string AppCode
        {
            get { return _AppCode; }
            set { _AppCode = value; }
        }
        public string AppName
        {
            get { return _AppName; }
            set { _AppName = value; }
        }
        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
        public int GroupDtlId
        {
            get { return _GroupDtlId; }
            set { _GroupDtlId = value; }
        }
        public string GroupCode
        {
            get { return _GroupCode; }
            set { _GroupCode = value; }
        }
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        public string GrouppName
        {
            get { return _GrouppName; }
            set { _GrouppName = value; }
        }
        public string AppId
        {
            get { return _AppId; }
            set { _AppId = value; }
        }

       

        public GroupBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetDeviceByAppId()
        {
            chatGroup = new tblChatGroupTableAdapter();
            return chatGroup.GetDeviceId(_AppId);
        }
        public string InsertIntoGrp()
        {
            try
            {
                chatGroup = new tblChatGroupTableAdapter();
                return chatGroup.InsertGroup(_ChatGroupId, _DeviceId, _GroupCode, _GroupName).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public DataTable GetAppGroupByDeviceId()
        {
            appgrpta = new tblAppGroupDeviceMappingTableAdapter();
            return appgrpta.GetAppGroupByDeviceId(_DeviceId);
        }
        public DataTable GetAppGroupByDeviceIdUpdate()
        {
            appgrpta = new tblAppGroupDeviceMappingTableAdapter();
            return appgrpta.GetAppGroupByDeviceIdUpdate(_DeviceId, _IsForUpdate);
        }
        public DataTable GetGroupDtls()
        {
            group = new tblGroupTableAdapter();
            return group.GetData(_ClientId);
        }
        public int InsertGroupDtls()
        {
            group = new tblGroupTableAdapter();
            return Convert.ToInt32(group.InsertGroupDtls(_GroupId, _ClientId, _GrouppName, _Description));
        }
        public int DeleteGroupDtls()
        {
            group = new tblGroupTableAdapter();
            return Convert.ToInt32(group.DeleteQuery(_GroupId));
        }
        public DataTable GetGrpName()
        {
            group = new tblGroupTableAdapter();
            return group.GetGroupNameFromGrpId(_GroupId);
        }
        public DataTable GetGrpNameForDDL()
        {
            group = new tblGroupTableAdapter();
            return group.GetGroupNameForDDL();

        }
        public string IU_AsgnCmnGpToEmp()
        {
            group = new tblGroupTableAdapter();
            return group.IU_AsgnCmnGpToEmp(_GroupId, _ClientId, _GroupId, _UserIdList, _LoggedBy).ToString();
        }
        public int iu_AppMster()
        {
            appmaster = new tblAppMasterTableAdapter();
            return Convert.ToInt32(appmaster.IU_AppMaster(_ApplicationId, _AppCode, _AppName, (_GroupId.ToString()), _GroupName, _ClientId, Convert.ToInt32(_LoggedBy)));
        }
        public int IU_ProfileAppMaster()
        {
            appmaster = new tblAppMasterTableAdapter();
            return Convert.ToInt32(appmaster.IU_ProfileAppMaster(_ApplicationId, _AppCode, _AppName, _GroupId.ToString(), _GroupName, _ClientId, Convert.ToInt32(_LoggedBy), _ProfileId));
        }
        public int DeleteAppMster()
        {
            appmaster = new tblAppMasterTableAdapter();
            return appmaster.DeleteQuery(_LoggedBy, _ApplicationId);
        }
        public DataTable GetAppGrpNameForDDL()
        {
            AppGrp = new tblAppGroupTableAdapter();
            return AppGrp.GetAppGrpNameForDDL();
        }
        public DataTable GetGroupIdByDeviceId()
        {
            chatapp = new tblChatAppTableAdapter();
            return chatapp.GetGroupIdByDeviceId(_DeviceId);
        }
        public DataTable GetAppIndexByDeviceIdAndGrpId()
        {
            chatapp = new tblChatAppTableAdapter();
            return chatapp.GetAppIndexByDeviceIdAndGrpId(_DeviceId, _GroupId);
        }
        public string IU_AppGrpDeviceMapping(int y = 0)
        {
            chatapp = new tblChatAppTableAdapter();
            return chatapp.IU_AppGrpDeviceMapping(_DeviceId, _GroupId, _Message, _ProfileId, y).ToString();
        }
        public void AppGrouping()
        {
            int AppIndex, GroupId = 0, ProfileId = 0; string Message = "";
            try
            {
                chatapp = new tblChatAppTableAdapter();
                dt = new DataTable();
                //dt = chatapp.GetGroupIdByDeviceId(_DeviceId);
                dt = chatapp.GetGroupandProfileByDeviceId(_DeviceId, _ClientId);
                int y = 0;
                foreach (DataRow row in dt.Rows)
                {
                    GroupId = Convert.ToInt32(row["GroupId"].ToString());
                    ProfileId = Convert.ToInt32(row["ProfileId"].ToString());
                    // chatapp.DeleteAppGroupDeviceMapping(_DeviceId,ProfileId);
                    dt1 = new DataTable();
                    try
                    {
                        chatapp = new tblChatAppTableAdapter();
                        //dt1 = chatapp.GetAppIndexByDeviceIdAndGrpId(_DeviceId, GroupId);
                        dt1 = chatapp.GetAppIndxByDevicegroupandProfileId(_DeviceId, ProfileId, GroupId.ToString());
                    }
                    finally
                    {
                        chatapp = null;
                    }
                    Message = "Gbox set as AA" + GroupId + "";
                    foreach (DataRow rows in dt1.Rows)
                    {
                        AppIndex = Convert.ToInt32(rows["AppIndx"].ToString());
                        Message = Message + " " + AppIndex;
                    }
                    if (_ProfileId != ProfileId)
                    {
                        y = 1;
                    }
                    else
                    {
                        y = 0;
                    }
                    _ProfileId = ProfileId;
                    _GroupId = GroupId;
                    _Message = Message;
                    IU_AppGrpDeviceMapping(y);
                    Message = string.Empty;
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }
        public DataTable GetLocReqFreq()
        {
            locreqfreq = new LocReqfreqTableAdapter();
            return locreqfreq.GetLocReqFreq(_ClientId);
        }
        public DataTable GetGeoFenceFreqByClientIdAndProfileId()
        {
            geofence = new GeofenceFreqTableAdapter();
            return geofence.GetGeoFenceFreqByClientIdAndProfileId(_ClientId, ProfileId);
        }
        public int IU_LocReqFreq()
        {
            locreqfreq = new LocReqfreqTableAdapter();
            return Convert.ToInt32(locreqfreq.IU_LocReqFreq(_LocReqFreqId, _GroupId, _ClientId, _FrmTime, _ToTime, _LocReqFrequency, _UserId));
        }
        public int IU_GeoFenceLocReqFreq()
        {
            locreqfreq = new LocReqfreqTableAdapter();
            return Convert.ToInt32(locreqfreq.IU_GeoFenceLocReqFreq(_GeoFenceReqFreqId, _ProfileId, _ClientId, _FrmTime, _ToTime, _Radius, _Lat, _Lng, _Name, "", _UserId, currentDateTime));
        }
        public int DeleteLocReqFreq()
        {
            locreqfreq = new LocReqfreqTableAdapter();
            return locreqfreq.DeleteLocReqFreq(_LocReqFreqId);
        }
        public int DeleteGeoFence()
        {
            geofence = new GeofenceFreqTableAdapter();
            return geofence.DeleteGeoFence(_LocReqFreqId);
        }
        public int UpdateLocReqFreqMsg()
        {
            try
            {
                group = new tblGroupTableAdapter();
                return Convert.ToInt32(group.UpdateLocFreqMsg(_GroupId, _Message).ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int UpdateGeoFenceFreqMsg()
        {
            try
            {
                group = new tblGroupTableAdapter();
                return Convert.ToInt32(group.UpdateGeoFreqMsg(_GroupId, _Message).ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public string GetFreqMsgForApp()
        {
            string Msg = "NA";
            dt = new DataTable();
            group = new tblGroupTableAdapter();
            dt = group.GetFreqMsg(_UserId);
            if (dt.Rows.Count > 0)
            {
                Msg = dt.Rows[0]["LocReqFreqMsg"].ToString();
            }
            return Msg;
        }
        public string GetGeoFenceFreqMsgForApp()
        {
            string Msg = "NA";
            dt = new DataTable();
            group = new tblGroupTableAdapter();
            dt = group.GetGeoFenceMessage(_UserId);
            if (dt.Rows.Count > 0)
            {
                Msg = dt.Rows[0]["GeoFenceMsg"].ToString();
            }
            return Msg;
        }
        public DataTable GetGeofenceByGeoFenceId()
        {
            geo = new GeoFenceTableAdapter();
            return geo.GetGeofenceByGeoFenceId(_GeoFenceReqFreqId, _ClientId);
        }
        public int IU_AsgnGrpToUsr()
        {
            group = new tblGroupTableAdapter();
            return Convert.ToInt32(group.IU_AsgnGrpToUsr(_ClientId, _GroupId, _UserIdList, _LoggedBy));
        }
        public DataTable GetLocReqFreqHstry()
        {
            locreqfreqhstry = new LocReqFreqHstryTableAdapter();
            return locreqfreqhstry.GetLocReqFreqHstry(_ClientId);
        }

        public int UpdateappGroupByUser()
        {
            chatapp = new tblChatAppTableAdapter();
            return Convert.ToInt32(chatapp.UpdateAppgroupForUser(_ChatGroupId, _LoggedBy, Convert.ToInt32(_AppId)).ToString());
        }
        public int DeleteGeoFenceDetails()
        {
            geofence = new GeofenceFreqTableAdapter();
            return Convert.ToInt32(geofence.DeleteGeoFenceDetails(_GeoFenceReqFreqId));
        }
        public int InsertUpdateGeoFenceLocReqFreq()
        {
            geofence = new GeofenceFreqTableAdapter();
            return Convert.ToInt32(geofence.IU_GeoFenceLocReqFreq(_GeoFenceReqFreqId, _ProfileId, _ClientId, _FrmTime, _ToTime, _Radius, _Lat, _Lng, _Name, "", _UserId, currentDateTime));
        }
    }
}
