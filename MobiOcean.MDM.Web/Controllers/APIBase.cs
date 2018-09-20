using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.ComponentModel;
using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using System.Text;
using MobiOcean.MDM.Web.Log.Interface;

/// <summary>
/// Summary description for APIBase
/// </summary>
/// 
namespace MobiOcean.MDM.Web.Controller
{
    public class APIBase : ApiController
    {


        DataTable dtuser, dt;
        UserBAL user;
        SubscribeBAL subscribeBal;
        AlertBAL alert;
        SendMailBAL sendMail;
        ConstantBAL consbal;

        private readonly ILog _ILog;


        public APIBase()
        {
            _ILog = Log.Repository.Log.GetInstance;
            //
            // TODO: Add constructor logic here
            //
        }

        protected DateTime GetCurrentDateTimeByUserId(int UserId)
        {
            consbal = new ConstantBAL();
            return consbal.GetCurrentDateTimeByUserId(UserId);
        }
        protected DateTime GetCurrentDateTimeByCountry(int CountryId)
        {
            consbal = new ConstantBAL();
            return consbal.GetCurrentDateTimeByCountry(CountryId);
        }
        protected void LogExceptions(string Controller, string input, string output, string Expecption)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Controller);
            sb.AppendLine();
            sb.Append(input);
            sb.AppendLine();
            if (!string.IsNullOrEmpty(output))
            {
                sb.Append(output);
                sb.AppendLine();
            }
            if (!string.IsNullOrEmpty(Expecption))
            {
                sb.Append(Expecption);
            }
            _ILog.LogAPIData(sb.ToString());
        }
        protected DataTable getDeviceDtlByAppId(string APPId)
        {
            try
            {
                //if (APPId.Contains("G"))//Genus
                //{
                //    dtuser = new DataTable();
                //    return dtuser;
                //}
                user = new UserBAL();
                return user.getDeviceDtlByAppId(APPId);
            }
            catch (Exception)
            {
                dtuser = new DataTable();
                return dtuser;
            }
        }
        protected string getDateInSQlServerFormat(string mydate)
        {
            mydate = mydate.Replace("/", "-");

            mydate = mydate.Replace("-01-", " Jan ");
            mydate = mydate.Replace("-02-", " Feb ");
            mydate = mydate.Replace("-03-", " Mar ");
            mydate = mydate.Replace("-04-", " Apr ");
            mydate = mydate.Replace("-05-", " May ");
            mydate = mydate.Replace("-06-", " Jun ");
            mydate = mydate.Replace("-07-", " Jul ");
            mydate = mydate.Replace("-08-", " Aug ");
            mydate = mydate.Replace("-09-", " Sep ");
            mydate = mydate.Replace("-10-", " Oct ");
            mydate = mydate.Replace("-11-", " Nov ");
            mydate = mydate.Replace("-12-", " Dec ");

            return mydate;
        }
        protected DataTable GetAdminDetail(int ClientId)
        {
            try
            {
                user = new UserBAL();
                user.ClientId = ClientId;
                user.RoleId = 2;
                return user.GetUserByRoleId();
            }
            catch (Exception)
            {
                dtuser = new DataTable();
                return dtuser;
            }
        }
        protected int ChkIFFeatureIsEnableAccordingDate(int UserId, string logDateTime, int featureId)
        {
            subscribeBal = new SubscribeBAL();
            subscribeBal.UserId = UserId;
            subscribeBal.RequestDateTime = logDateTime;
            return subscribeBal.ChkIFFeatureIsEnableAccordingDate(featureId);
        }
        protected void IsAlertEnable(int AlertTypeId, string AlertText, int UsrId, int ClitId)
        {
            alert = new AlertBAL();
            dt = new DataTable();
            alert.UserId = UsrId;
            alert.ClientId = ClitId;
            dt = alert.GetDeptHeadAndAdmin();
            string EmailIds = "";
            int IsMail = 0;
            foreach (DataRow row in dt.Rows)
            {
                alert.ForUserId = Convert.ToInt32(row["UserId"].ToString());
                alert.UserId = UsrId;
                alert.ClientId = ClitId;
                alert.AlertType = AlertTypeId;
                alert.AlertText = AlertText;
                alert.LogDateTime = GetCurrentDateTimeByUserId(UsrId).ToString("dd-MMM-yyyy HH:mm");
                alert.LoggedBy = UsrId;
                IsMail = alert.InsertIntotblAlert();
                if (IsMail > 0)
                {
                    EmailIds = EmailIds + "," + row["EmailId"].ToString();
                }
            }
            if (EmailIds.Length > 0)
            {
                EmailIds = EmailIds.Substring(1);
                sendMail = new SendMailBAL();
                sendMail.SendEmail(EmailIds, "MobiOcean Alert", "Dear sir <br/> " + AlertText, ClitId);
            }
        }
        protected string GenToken(int Size = 6, string input = "")
        {
            Random myrandom = new Random();
            if (input == "")
                input = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ9876543210";
            string pass = "";
            try
            {
                StringBuilder builder = new StringBuilder();
                char ch;
                for (int i = 0; i < Size; i++)
                {
                    ch = input[myrandom.Next(0, input.Length)];
                    builder.Append(ch);
                }
                pass = builder.ToString();
            }
            catch (Exception)
            {
                int myNum = myrandom.Next(10000000, 100000000);
                pass = "btyutyuuyerewvb" + myNum + "yhdguyfgd";
            }
            return pass;
        }
    }
}