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
    public class AppGroupController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;
        GroupBAL grpbal;
        [ActionName("GetAppGroup")]
        public List<GroupBAL> Get(string AppId, int IsForUpdate = 0)
        {
            List<GroupBAL> grplst = new List<GroupBAL>();
            try
            {
                dt = new DataTable();
                dt = getDeviceDtlByAppId(AppId);
                if (dt.Rows.Count > 0)
                {
                    DeviceId = Convert.ToInt32(dt.Rows[0]["DeviceId"].ToString());
                    UserId = Convert.ToInt32(dt.Rows[0]["UserId"].ToString());
                    ClientId = Convert.ToInt32(dt.Rows[0]["ClientId"].ToString());
                }
            }
            catch (Exception)
            {
                DeviceId = 0;
            }
            if (DeviceId > 0)
            {
                grpbal = new GroupBAL();
                dt = new DataTable();
                grpbal.DeviceId = DeviceId;
                grpbal.ClientId = ClientId;
                grpbal.AppGrouping();
                grpbal.IsForUpdate = IsForUpdate;
                dt = grpbal.GetAppGroupByDeviceIdUpdate();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        grpbal = new GroupBAL
                        {
                            AppGroupDeviceId = Convert.ToInt32(row["AppGroupDeviceId"].ToString()),
                            ChatGroupId = Convert.ToInt32(row["GroupId"].ToString()),
                            GroupCount = row["GroupCount"].ToString(),
                            ProfileId = Convert.ToInt32(row["ProfileId"].ToString()),
                            Message = row["Message"].ToString(),
                            IsEnable = Convert.ToInt32(row["IsEnable"].ToString()),
                            GroupCode = row["AppGroupCode"].ToString(),
                            GroupName = row["AppGroupName"].ToString(),
                        };
                        grplst.Add(grpbal);
                    }

                }


            }
            return grplst;
        }


    }
}
