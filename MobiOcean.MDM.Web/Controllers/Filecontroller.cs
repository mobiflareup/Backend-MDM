using MobiOcean.MDM.BAL.BAL;
using MobiOcean.MDM.BAL.Model;
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
    public class Filecontroller : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;
        SOSBAL sosBal;
        LocationBAL locationBal;

        // POST api/<controller>
        [ActionName("Insert")]
        public int Post([FromBody]FileModel value)
        {
            try
            {
                //LogExceptions("File --> Insert", "Input: " + new JavaScriptSerializer().Serialize(value), "Response : 1" + "","Input");
                dt = new DataTable();
                dt = getDeviceDtlByAppId(value.AppId);
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

                try
                {
                    value.LogDateTime = Convert.ToDateTime(value.LogDateTime).ToString("dd-MMM-yyyy HH:mm");
                    value.ClientId = ClientId;
                    value.DeviceId = DeviceId;
                    value.UserId = UserId;

                    locationBal = new LocationBAL();
                    LocationModel locationModel = locationBal.GetLocation(value.Latitude, value.Longitude, value.CellId.ToString(), value.locationAreaCode.ToString(), value.mobileCountryCode.ToString(), value.mobileNetworkCode.ToString(), ClientId, Constant.tblFileDetails, UserId);
                    value.Latitude = locationModel.latitude.ToString();
                    value.Longitude = locationModel.longitude.ToString();
                    value.location = locationModel.location;

                    sosBal = new SOSBAL();
                    sosBal.fileModel = value;
                    return sosBal.InsertFileDetails();
                }
                catch (Exception)
                {
                    //LogExceptions("File --> Insert", "OutPut : " + new JavaScriptSerializer().Serialize(value), "Response : 1" + ex.Message, "Output");
                    return 0;
                }
            }
            else
            {
                //LogExceptions("File --> Insert", "OutPut : " + new JavaScriptSerializer().Serialize(value), "Response : DeviceID=0", "Output");
                return 0;
            }
        }


    }
}
