using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.DAL.DAL.AckDALTableAdapters;

/// <summary>
/// Summary description for AckBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class AckBAL
    {
        tblAcknowledgementTableAdapter tback;
        AcknowledgementTableAdapter ack;
        tblApkTableAdapter apkld;
        tblUserTableAdapter tbluser;
        DataTable dt;
        Result result;
        tblRemarkExtTableAdapter remarkExt;
        tblAppCategoeyMappingTableAdapter AppCat;

        public int SyncFeatureId { get; set; }
        public string Activity { get; set; }
        public string AppId { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public int UserDeviceId { get; set; }
        public string AckDateTime { get; set; }
        public DateTime currentDateTime { get; set; }
        public string Remark { get; set; }
        public int RemarkId { get; set; }
        public AckBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetApKLatestDate(int appTypeId=1)
        {
            apkld = new tblApkTableAdapter();
            return apkld.GetAPKLatestDate(appTypeId);
        }
        public int InsertUserListForActicity()
        {
            tbluser = new tblUserTableAdapter();
            return tbluser.InsertActivitytbl(UserId, Activity);
        }
        public string GetAck()
        {
            tback = new tblAcknowledgementTableAdapter();
            return tback.Acknowledgement(AppId, AckDateTime).ToString();
        }
        public string SetAck()
        {
            tback = new tblAcknowledgementTableAdapter();
            return tback.AcknowledgementBysyncId(AppId, AckDateTime, SyncFeatureId).ToString();
        }
        public List<Result> CheckUpdates()
        {
            ack = new AcknowledgementTableAdapter();
            dt = new DataTable();
            List<Result> lst = new List<Result>();
            dt = ack.CheckUpdates(AppId, ClientId, UserDeviceId, UserId, currentDateTime);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result = new Result
                    {
                        UpdateResult = row["Result"].ToString()
                    };
                    lst.Add(result);
                }
            }
            return lst;
        }
        public List<Result> CheckUpdatesBySyncId()
        {
            ack = new AcknowledgementTableAdapter();
            dt = new DataTable();
            List<Result> lst = new List<Result>();
            dt = ack.CheckUpdatsBySyncId(AppId, ClientId, UserDeviceId, UserId, currentDateTime);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result = new Result
                    {
                        UpdateResult = row["Result"].ToString()
                    };
                    lst.Add(result);
                }
            }
            return lst;
        }

        public DataTable GetURLforMap(int typeId)
        {
            try
            {
                QueriesTableAdapter qa = new QueriesTableAdapter();
                return qa.GetApiString(typeId, ClientId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int insertApiCount(long ClientId, int ApiType, int Is_API_Used, int UserId)
        {
            try
            {
                QueriesTableAdapter qa = new QueriesTableAdapter();
                return qa.InsertApiCount(ClientId, ApiType, Is_API_Used, UserId);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public bool InsertUserCaty(int CategoryId, int UserId, int ClientId, int Status)
        {
            AppCat = new tblAppCategoeyMappingTableAdapter();
            int res = AppCat.InsertAppCategoryUser(CategoryId, UserId, ClientId, Status);
            return res > 0 ? true : false;
        }

        public int GetRemarkData()
        {
            remarkExt = new tblRemarkExtTableAdapter();
            int res = (int)remarkExt.GetRemark(RemarkId, ClientId,UserId, Remark);
            return res;
        }

        public int InsertRemarkData()
        {
            remarkExt = new tblRemarkExtTableAdapter();
            int res = (int)remarkExt.InsertRemark(ClientId,UserId, Remark);
            return res;
        }

        public int DeleteRemarkData()
        {
            remarkExt = new tblRemarkExtTableAdapter();
            int res = (int)remarkExt.DeleteRemark(UserId, RemarkId);
            return res;
        }
    }
    public class Result
    {
        public string UpdateResult { get; set; }
    }
}
