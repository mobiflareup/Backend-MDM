using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MobiOcean.MDM.Web.Controllers
{
    public class GenusController : ApiController
    {
        SubscribeBAL subscribeBal;

        [HttpPost]
        public int register([FromBody]GenusBAL genusBal)
        {
            try
            {
                if (genusBal.ClientId > 0 && genusBal.userInfo != null)
                {
                    subscribeBal = new SubscribeBAL();
                    subscribeBal.ClientId = genusBal.ClientId;
                    DataTable dtSubscription = subscribeBal.GetSubscriptionByClientId();
                    if (dtSubscription != null && dtSubscription.Rows.Count > 0 && Convert.ToInt32(dtSubscription.Rows[0]["RemainingTimePeriod"].ToString()) > 0)
                    {
                        genusBal.allowedUserCount = Convert.ToInt32(dtSubscription.Rows[0]["NoOfEmployees"].ToString());
                        return genusBal.IU_CustomAppUser();
                    }                    
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}
