using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;

namespace MobiOcean.MDM.Web.Controller
{
    public class FullConveyanceDetailsController : APIBase
    {
        DataTable dt, dt1, dt2;
        CustomerBAL cust;
        tamasterdetail tamaster;
        int UserId = 0;
        int masterid = 0;
        [ActionName("GetFullConveyanceDetails")]
        public List<tamasterdetail> Get(string id)
        {
            List<tamasterdetail> talist = new List<tamasterdetail>();
            try
            {
                dt = getDeviceDtlByAppId(id);
                if (dt.Rows.Count > 0)
                {
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
            }
            catch (Exception)
            {
                UserId = 0;
            }
            if (UserId > 0)
            {
                cust = new CustomerBAL();
                cust.UserId = UserId;
                dt = cust.GetTA_MasterDetailsByUserId();
                foreach (DataRow obj in dt.Rows)
                {
                    masterid = Convert.ToInt32(obj["MasterId"].ToString());
                    tamaster = new tamasterdetail();
                    {
                        tamaster.masterid = Convert.ToInt32(obj["MasterId"].ToString());
                        tamaster.clientid = Convert.ToInt32(obj["ClientId"].ToString());
                        tamaster.userid = Convert.ToInt32(obj["UserId"].ToString());
                        tamaster.logdate = obj["LogDate"].ToString();
                        tamaster.istripend = Convert.ToInt32(obj["IsTripEnd"].ToString());
                        tamaster.isapproved = Convert.ToInt32(obj["IsApproved"].ToString());
                        tamaster.ispaid = Convert.ToInt32(obj["IsPaid"].ToString());
                        tamaster.approvedby = obj["ApprovedBy"].ToString();
                        tamaster.claimedamt = Convert.ToDouble(obj["ClaimedAmt"].ToString());
                        tamaster.approvedamt = Convert.ToDouble(obj["ApprovedAmt"].ToString());
                        tamaster.totaldistance = Convert.ToDouble(obj["TotalDistance"].ToString());
                        tamaster.approverremark = obj["ApproverRemark"].ToString();

                    };
                    List<tavisitdetail> visitdetail = new List<tavisitdetail>();
                    List<taextradetail> extradetail = new List<taextradetail>();
                    cust.MasterID = masterid;
                    dt1 = cust.GetTA_VisitDetailByMasterId();
                    foreach (DataRow obj1 in dt1.Rows)
                    {
                        tavisitdetail visit = new tavisitdetail();
                        {
                            visit.visitdetailid = Convert.ToInt32(obj1["VisitDetailId"].ToString());
                            visit.fromdatetime = obj1["FromDateTime"].ToString();
                            visit.visitdatetime = obj1["VisitDateTime"].ToString();
                            visit.todatetime = obj1["ToDateTime"].ToString();
                            visit.customerid = Convert.ToInt32(obj1["CustomerId"].ToString());
                            visit.isvisited = Convert.ToInt32(obj1["Isvisited"].ToString());
                            visit.isreturn = Convert.ToInt32(obj1["IsReturn"].ToString());
                            visit.filepath = obj1["Filepath"].ToString();
                            visit.remark = obj1["Remark"].ToString();
                            visit.totaldistance = Convert.ToDouble(obj1["TotalDistance"].ToString());
                            visit.modeoftravel = Convert.ToInt32(obj1["ModeoftravelId"].ToString());
                            visit.rateperkm = Convert.ToDouble(obj1["RatePerKM"].ToString());
                            visit.fromlat = Convert.ToDouble(obj1["FromLat"].ToString());
                            visit.fromlong = Convert.ToDouble(obj1["FromLong"].ToString());
                            visit.fromlocation = obj1["FromLocation"].ToString();
                            visit.tolat = Convert.ToDouble(string.IsNullOrEmpty(obj1["ToLat"].ToString()) ? "0" : obj1["ToLat"].ToString());
                            visit.tolong = Convert.ToDouble(string.IsNullOrEmpty(obj1["ToLong"].ToString()) ? "0" : obj1["ToLong"].ToString());
                            visit.tolocation = obj1["ToLocation"].ToString();
                            visit.claimedtravelamt = Convert.ToDouble(obj1["ClaimedTravelAmt"].ToString());
                            visit.approvedtravelamt = Convert.ToDouble(obj1["ApprovedTravelAmt"].ToString());
                            visit.approverremark = obj1["ApproverRemark"].ToString();
                        }
                        visitdetail.Add(visit);
                        tamaster.tavisitdetail = visitdetail;
                    }
                    dt2 = cust.GetTA_ExtraDetailByMasterId();
                    foreach (DataRow obj2 in dt2.Rows)
                    {
                        taextradetail extra = new taextradetail();
                        {
                            extra.extradetailid = Convert.ToInt32(obj2["ExtraDetailId"].ToString());
                            extra.claimedamt = Convert.ToDouble(obj2["ClaimedAmt"].ToString());
                            extra.approvedamt = Convert.ToDouble(obj2["ApprovedAmt"].ToString());
                            extra.logdatetime = obj2["LogDateTime"].ToString();
                            extra.remark = obj2["Remark"].ToString();
                            extra.filepath = obj2["FilePath"].ToString();
                            extra.approverremark = obj2["ApproverRemark"].ToString();
                            extra.isapproved = Convert.ToInt32(obj2["IsApproved"].ToString());
                            extra.approvedby = obj2["ApprovedBy"].ToString();
                        };
                        extradetail.Add(extra);
                        tamaster.taextradetail = extradetail;
                    }
                    talist.Add(tamaster);
                }

            }
            return talist;
        }

    }
}
