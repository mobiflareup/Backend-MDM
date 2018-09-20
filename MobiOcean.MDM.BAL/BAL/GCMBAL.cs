using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.DAL.DAL.GCMDALTableAdapters;

/// <summary>
/// Summary description for GCMBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class GCMBAL
    {
        GiveStngSMSTOPhOnReqTableAdapter giveStngSMSTOPhOnReqTableAdapter;
        SetGCMMsgTableAdapter setgcm;
        DataTable dt;
        private string _AppId, _GCMId, _SendMsgIdList, _GCMSenderId;
        private int _IsAndroid;
        private int _DeviceId;

        public int DeviceId
        {
            get { return _DeviceId; }
            set { _DeviceId = value; }
        }
        public int IsAndroid
        {
            get { return _IsAndroid; }
            set { _IsAndroid = value; }
        }
        public string GCMSenderId
        {
            get { return _GCMSenderId; }
            set { _GCMSenderId = value; }
        }
        public string SendMsgIdList
        {
            get { return _SendMsgIdList; }
            set { _SendMsgIdList = value; }
        }
        public string AppId
        {
            get { return _AppId; }
            set { _AppId = value; }
        }
        public string GCMId
        {
            get { return _GCMId; }
            set { _GCMId = value; }
        }
        public GCMBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string UpdateGCMSenderId()
        {
            giveStngSMSTOPhOnReqTableAdapter = new GiveStngSMSTOPhOnReqTableAdapter();
            string res = giveStngSMSTOPhOnReqTableAdapter.UpdateGCMSenderId(_DeviceId, _GCMId, _GCMSenderId, _IsAndroid).ToString();
            return res;
        }
        public string GiveStngSMS()
        {
            string SendMsgId_List = "", Message = "";
            giveStngSMSTOPhOnReqTableAdapter = new GiveStngSMSTOPhOnReqTableAdapter();
            dt = new DataTable();
            dt = giveStngSMSTOPhOnReqTableAdapter.GiveStngSMSTOPhOnReq(_DeviceId);
            #region-------- Now we Give Pending SMS to Student ------------
            if (dt.Rows.Count > 0)
            {
                for (int idx = 0; idx < dt.Rows.Count; idx++)
                {
                    SendMsgId_List = SendMsgId_List + dt.Rows[idx]["SendMsgId"].ToString().Trim() + ",";
                    Message = Message + " " + dt.Rows[idx]["Message"].ToString().Trim();
                    Message = Message.Replace("GBox set as", "");
                    Message = Message.Replace("Gbox set as", "");
                }


                Message = Message.Replace("  ", " ");
                Message = Message.Replace("  ", " ");

                Message = SendMsgId_List + " GBox set as " + Message;
                return Message;
            }
            else
            {
                return "-1";
            }

            #endregion
        }
        public string SetMsgStatusOnAckwldgment()
        {
            giveStngSMSTOPhOnReqTableAdapter = new GiveStngSMSTOPhOnReqTableAdapter();
            return giveStngSMSTOPhOnReqTableAdapter.SetMsgStatusOnAckwldgment(_SendMsgIdList).ToString();
        }
        public string UpdateGCMIDByAppId()
        {
            try
            {
                giveStngSMSTOPhOnReqTableAdapter = new GiveStngSMSTOPhOnReqTableAdapter();
                giveStngSMSTOPhOnReqTableAdapter.UpdateGCMId(_GCMId, _AppId);
                return "1";
            }
            finally
            {
                giveStngSMSTOPhOnReqTableAdapter = null;
                dt = null;
            }
        }
        public DataTable SetGCM()
        {
            setgcm = new SetGCMMsgTableAdapter();
            return setgcm.SetGCMMsg();
        }
    }
}