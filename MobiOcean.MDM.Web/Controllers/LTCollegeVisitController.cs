using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace MobiOcean.MDM.Web.Controller
{
    public class LTCollegeVisitController : APIBase
    {
        LTCollegeVisitBAL ltBal;
        LocationBAL gapi;
        DataTable dt, dt1, dt2;
        UserBAL user;
        SendSMSBAL sms;
        SendMailBAL mail;
        CustomerBAL cust;
        AnuSearch srch;
        int ClientId = 0, UserId = 0;
        [ActionName("InsertLTCollegeVisitDetails")]
        public int Post([FromBody]LTCollegeVisitBAL value)
        {
            try
            {
                dt = getDeviceDtlByAppId(value.appId);
                if (dt.Rows.Count > 0)
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
            }
            catch (Exception)
            {
            }
            if (ClientId > 0)//
            {
                try
                {
                    int res = 0;
                    string towwerLocation = value.CellId + "," + value.LAC + "," + value.MCC + "," + value.MNC;
                    gapi = new LocationBAL();
                    LocationModel locationModel = gapi.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.LAC.ToString(), value.MCC.ToString(), value.MNC.ToString(), ClientId, Constant.TA_VisitDetail, UserId);
                    if (string.IsNullOrEmpty(value.Location))
                    {
                        //manual=0                        
                        res = InsertLTVisitDetail(locationModel.latitude.ToString(), locationModel.longitude.ToString(), locationModel.location, value.LogDateTime, value.IsOutTime, value.CollegeId, value.Forgot, false, towwerLocation, value.Location,value.visitId);
                    }
                    else
                    {
                        // manual = 1
                        string locationByCellId = "";
                        if (locationModel.location != Constant.LocationNotFound)
                        {
                            value.Latitude = locationModel.latitude.ToString();
                            value.Longitude = locationModel.longitude.ToString();
                            locationByCellId = locationModel.location;
                        }
                        res = InsertLTVisitDetail(value.Latitude.ToString(), value.Longitude.ToString(), value.Location, value.LogDateTime, value.IsOutTime, value.CollegeId, value.Forgot, true, towwerLocation, locationByCellId, value.visitId);
                    }
                    //LogExceptions("AuthController --> LTCollegeVisitNew", "Input: " + new JavaScriptSerializer().Serialize(value), "Response : 1" + "", res.ToString());
                    return res;
                }
                catch (Exception ex)
                {
                   // LogExceptions("AuthController --> LTCollegeVisitNew", "Input: " + new JavaScriptSerializer().Serialize(value), "Response : 0" + "", ex.Message + "/n" + ex.StackTrace);
                    return 0;
                }
                // return 0;
            }
            else
            {
                //LogExceptions("AuthController --> LTCollegeVisitNew", "Input: " + new JavaScriptSerializer().Serialize(value), "Response : ClientId=0 " + "", "Wrong APPId");
                return 0;
            }

        }
        public int InsertLTVisitDetail(string latitude, string longitude, string location, string logdatetime, int isinTime, int collegeId, int forgot, bool isLocationManuallyEntered, string towerlocation, string manualLocation,int visitId)
        {
            double distance = 0;
            cust = new CustomerBAL();
            dt2 = new DataTable();
            cust.CustomerId = collegeId;
            dt2 = cust.CustomerDetailsbyCustomerid();
            try
            {
                distance = gapi.getDistanceFromLatLonInMtr(Convert.ToDouble(latitude), Convert.ToDouble(longitude), Convert.ToDouble(dt2.Rows[0][11]), Convert.ToDouble(dt2.Rows[0][12]));
            }
            catch (Exception)
            {
                distance = 0;
            }
            var d = distance != 0 ? distance < 1000 ? 1 : 0 : 0; // User is in 1KM range.
            string time = "", username = "";
            int dateofjoining = 0;
            ltBal = new LTCollegeVisitBAL();
            ltBal.IsOutTime = isinTime;
            ltBal.ClientId = ClientId;
            ltBal.UserId = UserId;
            ltBal.CollegeId = collegeId;
            ltBal.Forgot = forgot;
            ltBal.Location = location;
            logdatetime = getDateInSQlServerFormat(logdatetime);
            ltBal.Latitude = latitude;
            ltBal.Longitude = longitude;
            ltBal.LogDateTime = logdatetime;
            time = Convert.ToDateTime(logdatetime).ToString("HH:mm");
            ltBal.Time = time;
            ltBal.InVerification = d;
            ltBal.IsLocationManuallyEntered = isLocationManuallyEntered;
            ltBal.distance = distance;
            ltBal.TowerLocation = towerlocation;
            ltBal.ManualLoaction = manualLocation;
            ltBal.visitId = visitId;
            int res = ltBal.InsertLTVisitDetails2();
            //int res = ltBal.UpdateDailyAssign();
            try
            {
                user = new UserBAL();
                dt1 = new DataTable();
                user.UserId = UserId;
                dt1 = user.GetUserDtlByUserId();
                if (dt1.Rows.Count > 0)
                {
                    username = dt1.Rows[0]["UserName"].ToString();
                    dateofjoining = Convert.ToInt32(dt1.Rows[0]["DateOfJoining"].ToString());

                    if (dateofjoining == 1)
                    {
                        dt = new DataTable();
                        dt = user.GetRptngManagerByUserId();
                        if (dt.Rows.Count > 0)
                        {
                            string reportinguser = "", mblno = "", msg = "", custname = "", emailid = "";
                            reportinguser = dt.Rows[0]["UserName"].ToString();
                            mblno = dt.Rows[0]["MobileNo"].ToString();
                            emailid = dt.Rows[0]["EmailId"].ToString();
                            custname = dt2.Rows[0]["Name"].ToString();

                            if (location.Contains(Constant.LocationNotFound) && latitude != "0" && longitude != "0")
                            {
                                location = "https://www.google.com/maps?q=" + latitude + "," + longitude;
                            }
                            if (isinTime == 0)
                            {
                                msg = "Dear " + reportinguser + "" + System.Environment.NewLine + "" + username + " reached to " + custname + " at " + time + " at " + location + ".";
                            }
                            else
                            {
                                msg = "Dear " + reportinguser + System.Environment.NewLine + "" + username + " left from " + custname + " at " + time + " at " + location + ".";
                            }
                            try
                            {
                                sms = new SendSMSBAL();
                                sms.sendMsgUsingSMS(msg, mblno, ClientId,1);
                                mail = new SendMailBAL();
                                msg = msg + System.Environment.NewLine + "Thanks and regards, " + System.Environment.NewLine + "MobiOcean Team";
                                mail.SendEmail(emailid, "College visit Notification", msg, ClientId);
                            }
                            catch (Exception)
                            { }
                        }
                    }
                }
            }
            catch (Exception) { }
            return res;
        }
        [ActionName("InsertRemarkInLTCollegeVisit")]
        public int Post([FromBody]LTCollegeVisitBAL value, int c = 0)
        {
            try
            {
                dt = getDeviceDtlByAppId(value.appId);
                if (dt.Rows.Count > 0)
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
            }
            catch (Exception)
            {

            }
            if (ClientId > 0)
            {
                try
                {

                    ltBal = new LTCollegeVisitBAL();
                    if (value.ImagePath.Length > 0)
                    {
                        foreach (var obj in value.ImagePath)
                        {
                            ltBal.Remark = value.Remark;
                            ltBal.imgpath = obj;
                            ltBal.LTCollegeVisitId = value.LTCollegeVisitId;
                            ltBal.UserId = UserId;
                            ltBal.LogDateTime = value.LogDateTime;
                            ltBal.InsertRemarkInLTCollegeVisitRemark();
                        }
                    }
                    else
                    {
                        ltBal.Remark = value.Remark;
                        ltBal.imgpath = null;
                        ltBal.LTCollegeVisitId = value.LTCollegeVisitId;
                        ltBal.UserId = UserId;
                        ltBal.LogDateTime = value.LogDateTime;
                        ltBal.InsertRemarkInLTCollegeVisitRemark();
                    }

                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }


        [ActionName("GetRemark")]
        public List<RemarkTable> Get(string appId)
        {
            List<RemarkTable> listRmk = new List<RemarkTable>();
            try
            {
                dt = getDeviceDtlByAppId(appId);
                if (dt.Rows.Count > 0)
                {
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
            }
            catch (Exception)
            {

            }
            if (ClientId > 0)
            {
                try
                {
                    srch = new AnuSearch();
                    dt = new DataTable();
                    dt = srch.RemarExtData(ClientId);
                    if (dt != null)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            RemarkTable remark = new RemarkTable();
                            remark.RemarkId = (int)row["RemarkId"];
                            remark.Remark = row["Remark"].ToString();
                            listRmk.Add(remark);
                        }                      
                    }
                    return listRmk;
                }
                catch (Exception)
                {
                    return listRmk;
                }
                finally
                {
                    listRmk = null;
                }
            }
            else
            {
                return listRmk;
            }
          
        }
		}
}
