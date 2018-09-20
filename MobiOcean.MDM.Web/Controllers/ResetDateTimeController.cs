using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controller
{
    public class ResetDateTimeController : APIBase
    {
        SendSMSBAL sms;
        [ActionName("RT")]
        public string Post([FromBody]RstDateTime value, int isSms = 0)
        {
            try
            {
                UserDeviceBAL usr = new UserDeviceBAL();
                DataTable dt = new DataTable();
                usr.MobileNo1 = value.StuMob;
                dt = usr.GetDeviceByMobileNo();
                if (dt.Rows.Count > 0)
                {
                    value.countryId = Convert.ToInt32(dt.Rows[0]["CountryId"].ToString());
                }

            }
            catch (Exception)
            {
                if (value != null)
                {
                    value.countryId = 1;
                }
                else
                {
                    value = new RstDateTime();
                    value.countryId = 1;
                    isSms = 0;
                }
            }
            return Reset_XMLFormat(value.StuMob, value.countryId, isSms);
        }
        private string Reset_XMLFormat(string StuMob, int countryId, int isSms)
        {
            try
            {
                string myCurrentDateTime = GetCurrentDateTimeByCountry(countryId).ToString("dd-MM-yy hh mm sstt");
                if (isSms == 1)
                {
                    sms = new SendSMSBAL();
                    sms.sendMsgUsingSMS("GBox set as RT " + myCurrentDateTime, StuMob, 1);
                    return "1";
                }
                else
                {
                    return "GBox set as RT " + myCurrentDateTime;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                sms = null;
            }


        }
    }
}