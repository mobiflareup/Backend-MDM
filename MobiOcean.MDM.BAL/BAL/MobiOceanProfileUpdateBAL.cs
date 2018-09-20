using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobiOcean.MDM.DAL.DAL.MobiOceanProfileUpdateDALTableAdapters;
using MobiOcean.MDM.BAL.Model;
/// <summary>
/// Summary description for MobiOceanProfileUpdateBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class MobiOceanProfileUpdateBAL
    {
        InsertProfileUpdatesTableAdapter profile;
        public MobiOceanProfileUpdateBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        internal void InsertProfileList(int userId, Policy policy, DateTime dateTime)
        {
            profile = new InsertProfileUpdatesTableAdapter();
            profile.InsertProfileUpdates(userId, policy.ProfileId, policy.IsEnable, policy.Message, policy.FeatureId, policy.FeatureStatus, policy.IsBlackList, true, dateTime);
        }
    }
}