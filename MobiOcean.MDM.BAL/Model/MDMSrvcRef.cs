using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
/// <summary>
/// Summary description for MDMSrvcRef
/// </summary>
/// 

namespace MobiOcean.MDM.BAL.Model
{
    public class MDMSrvcRef
    {

        public MDMSrvcRef()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region------ RT(ResetDateTime) -----
        public string ResetDate(RstDateTime cons)
        {
            try
            {
                var jsondata = new JavaScriptSerializer().Serialize(cons);
                var client = new RestClient("ResetDateTime/RT", HttpVerb.POST, jsondata.ToString());
                var json = client.MakeRequest("?isSms=1");
                string a = "1";
                return a;
            }
            catch (Exception ex)
            {
                string a = ex.Message;
                return a;
            }
            finally
            { }
        }
        #endregion

        //#region--- GL(Location Location) Decode SMS-----
        //public string LocationDecodeSms(Location objlocation)
        //{
        //    var jsondata1 = new JavaScriptSerializer().Serialize(objlocation);
        //    var client = new RestClient("Location/StuLoc", HttpVerb.POST, jsondata1.ToString());

        //    string a = "1";
        //    return a;
        //}
        //#endregion  


        //#region--- SC(SimChange) Decode SMS-----
        //public string SimChangeDecode(SimChangeCntrlr objsimchnage)
        //{
        //    var jsondata1 = new JavaScriptSerializer().Serialize(objsimchnage);
        //    var client = new RestClient("SimChange/SimChangeSMS", HttpVerb.POST, jsondata1.ToString());
        //    string a = "1";
        //    return a;
        //}
        //#endregion

        //#region--- AL(AppStatus) Decode SMS-----
        //public string AppStatusDecode(ApplicationStatus objappstatus)
        //{

        //    var jsondata1 = new JavaScriptSerializer().Serialize(objappstatus);
        //    var client = new RestClient("AppStatus/setGingrAppStatusByStuId", HttpVerb.POST, jsondata1.ToString());
        //    string a = "1";
        //    return a;
        //}
        //#endregion

        //#region--- AA(InstallApp) Decode SMS-----
        //public string InstallAppDecode(ChatAppList objchatapp)
        //{
        //    var jsondata1 = new JavaScriptSerializer().Serialize(objchatapp);
        //    var client = new RestClient("ChatApp/GetAppList", HttpVerb.POST, jsondata1.ToString());
        //    string a = "1";
        //    return a;
        //}
        //#endregion

        //#region--- RA(UninstallApp) Decode SMS-----
        //public string UninstallAppDecode(AppUnistallStatus objuninstall)
        //{
        //    var jsondata1 = new JavaScriptSerializer().Serialize(objuninstall);
        //    var client = new RestClient("Uninstall/setAppUnistallationStatus", HttpVerb.POST, jsondata1.ToString());
        //    string a = "1";
        //    return a;
        //}
        //#endregion

        //#region--- LM Decode SMS-----
        //public string LMDecode(DocVariable20 objdocvariable20)
        //{
        //    var jsondata1 = new JavaScriptSerializer().Serialize(objdocvariable20);
        //    var client = new RestClient("AndroidDoc/AppLog_Message", HttpVerb.POST, jsondata1.ToString());
        //    string a = "1";
        //    return a;
        //}
        //#endregion

        //#region--- GS(GPSStatus) Decode SMS-----
        //public string GPSStatusDecode(GPSReport objgpsrpt)
        //{
        //    var jsondata1 = new JavaScriptSerializer().Serialize(objgpsrpt);
        //    var client = new RestClient("Report/GPSStatus", HttpVerb.POST, jsondata1.ToString());
        //    string a = "1";
        //    return a;
        //}
        //#endregion  

        //#region--- VN(VersionNo) Decode SMS-----
        //public string AppVersionNoDecode(VrsnReport objvrsnrpt)
        //{
        //    var jsondata1 = new JavaScriptSerializer().Serialize(objvrsnrpt);
        //    var client = new RestClient("Report/VersionNo", HttpVerb.POST, jsondata1.ToString());
        //    string a = "1";
        //    return a;
        //}
        //#endregion

        #region--- MO(MobileONOFF) Decode SMS-----
        public string MobileOnOffDecode(DocBAL objdocvariable)
        {
            var jsondata1 = new JavaScriptSerializer().Serialize(objdocvariable);
            var client = new RestClient("AndroidDoc/BtnPress_XMLFormat", HttpVerb.POST, jsondata1.ToString());
            var json = client.MakeRequest();
            string a = "1";
            return a;
        }
        #endregion

        //#region--- LI Decode SMS-----
        //public string LIDecode(DocVariable19 objdocdovariableli)
        //{
        //    var jsondata1 = new JavaScriptSerializer().Serialize(objdocdovariableli);
        //    var client = new RestClient("AndroidDoc/BtnPress_XMLFormat", HttpVerb.POST, jsondata1.ToString());
        //    string a = "1";
        //    return a;
        //}
        //#endregion 
        #region---- LTCollegeVisit ----
        public string LTCollegeVisit(LTCollegeVisitBAL objdocvariable)
        {
            var jsondata1 = new JavaScriptSerializer().Serialize(objdocvariable);
            var client = new RestClient(Constant.URL+"api/Visit/InsertLTCollegeVisitDetails", HttpVerb.POST, jsondata1.ToString());
            var json = client.MakeRequest();
            string a = "1";
            return a;
        }
        #endregion
        #region---- Insert Attendance ----
        public string InsertAttendance(AttendanceBAL objdocvariable)
        {
            var jsondata1 = new JavaScriptSerializer().Serialize(objdocvariable);
            var client = new RestClient(Constant.URL + "api/Attendance/InsertAttendenceDetails", HttpVerb.POST, jsondata1.ToString());
            var json = client.MakeRequest();
            string a = "1";
            return a;
        }
        #endregion
    }
}
