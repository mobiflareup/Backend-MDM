using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobiOcean.MDM.DAL.DAL.GenusDALTableAdapters;

namespace MobiOcean.MDM.BAL.BAL
{
    public class GenusBAL
    {
        CustomAppUserTableAdapter customAppUserTableAdapter;

        public string userInfo { get; set; }
        public string userInfo2 { get; set; }
        public string userInfo3 { get; set; }
        public string userInfo4 { get; set; }
        public int ClientId { get; set; }
        public int allowedUserCount { get; set; }

        public int IU_CustomAppUser()
        {
            try
            {
                customAppUserTableAdapter = new CustomAppUserTableAdapter();
                return Convert.ToInt32(customAppUserTableAdapter.IU_CustomAppUser(userInfo, userInfo2, userInfo3, userInfo4, ClientId, allowedUserCount).ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
