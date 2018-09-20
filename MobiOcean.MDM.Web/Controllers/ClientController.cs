using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.Web.Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controllers
{
    public class ClientController : APIBase
    {
        ClientBAL clientBAL;
        DataTable dt, dtApp;
        int DeviceId = 0, ClientId = 0, UserId = 0;
        [HttpGet]
        public List<CustomAppModel> GetClientCustomApp(string appID)
        {
            List<CustomAppModel> customAppList = new List<CustomAppModel>();
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(appID);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            if (DeviceId > 0)
            {
                try
                {
                    clientBAL = new ClientBAL();
                    clientBAL.ClientId = ClientId;
                    clientBAL.UserId = UserId;
                    dtApp = clientBAL.GetClientCustomApp();
                    if (dtApp != null && dtApp.Rows.Count > 0)
                    {
                        CustomAppModel customAppModel;
                        foreach (DataRow row in dtApp.Rows)
                        {
                            customAppModel = new CustomAppModel();
                            customAppModel.appTypeId = Convert.ToInt32(row["apptypeid"].ToString());
                            customAppModel.appName = row["Name"].ToString();
                            customAppModel.packageName = row["Package"].ToString();
                            customAppModel.downloadUrl = row["downloadurl"].ToString();
                            customAppList.Add(customAppModel);
                        }
                        return customAppList;
                    }
                    return customAppList;
                }

                catch (Exception)
                {
                    return customAppList;
                }
            }
            else
            {
                return customAppList;
            }
        }
    }
}
