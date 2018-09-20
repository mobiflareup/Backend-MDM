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
    public class DeviceuserController : ApiController
    {
        DataTable dt;
        UserDeviceBAL userBAL, userdevicelist;
        [ActionName("Employee")]
        public List<UserDeviceBAL> Get(string AppId)
        {
            List<UserDeviceBAL> userlist = new List<UserDeviceBAL>();
            try
            {
                try
                {
                    //if (!AppId.Contains("G"))
                    //{
                        userBAL = new UserDeviceBAL();
                        dt = new DataTable();
                        userBAL.APPId = AppId;
                        dt = userBAL.GetDeviceDataByAppId();
                        foreach (DataRow row in dt.Rows)
                        {
                            userdevicelist = new UserDeviceBAL
                            {
                                EmpCompanyId = row["EmpCompanyId"].ToString(),
                                UserName = row["UserName"].ToString(),
                                DeviceName = row["DeviceName"].ToString(),
                                MobileNo1 = row["MobileNo1"].ToString(),
                                EmailId = row["EmailId"].ToString(),
                                Gender = row["Gender"].ToString(),
                                ProfileImagePath = row["ProfileImagePath"].ToString()
                            };
                            userlist.Add(userdevicelist);
                        }
                   // }
                    return userlist;

                }
                catch (Exception)
                {
                    return userlist;
                }
            }
            finally
            {

            }
        }

        [ActionName("UpdateImage")]
        public int Post([FromBody]UserDeviceBAL upd)
        {
            try
            {
                //if (!upd.APPId.Contains("G"))
                //{
                    return upd.UpdateGetUserDevice();
                //}
                //return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}