using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Query;
using MobiOcean.MDM.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controller
{
    public class UserController : APIBase
    {
        DataTable dt;

        [ActionName("SimAndIMEI")]
        public string Post([FromBody]UserBAL usr)
        {
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(usr.APPId);
                if (dt.Rows.Count > 0)
                {
                    return usr.ChkUser();
                }
                else
                {
                    return "0";
                }

            }
            catch
            {
                return "0";
            }
        }
        [ActionName("CheckClient")]
        public string Get(string AppId, string ClientName)
        {
            try
            {

                if (ClientName == "Genus")
                {
                    var client = new RestClient("https://genus.mobiocean.com" + "/api/User/CheckGenus?AppId=" + AppId, HttpVerb.GET);
                    var json = client.MakeRequest();
                    if (Convert.ToBoolean(json) == true)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    GingerboxSrch gbox = new GingerboxSrch();
                    dt = new DataTable();
                    dt = gbox.CheckAppId(AppId);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }
}
