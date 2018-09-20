using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MobiOcean.MDM.DAL.DAL.DocDALTableAdapters;
using MobiOcean.MDM.DAL.DAL.LocationDALTableAdapters;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.Infrastructure;

/// <summary>
/// Summary description for DocVariable
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class DocBAL
    {
        AndroidBtnPressDtlsTableAdapter btn;

        public int ClientId { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public string AppId { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string dateTime { get; set; }
        public string Remarks { get; set; }
        public int functionalityId { get; set; }
        public string Location { get; set; }
        public string CellId { get; set; }
        public string locationAreaCode { get; set; }
        public string mobileCountryCode { get; set; }
        public string mobileNetworkCode { get; set; }


        public string BtnPress_XMLFormat()
        {
            try
            {
                btn = new AndroidBtnPressDtlsTableAdapter();
                return btn.InsertbtnPressInfo(ClientId, UserId, DeviceId, functionalityId, Lat, Long, CellId, locationAreaCode, mobileCountryCode, mobileNetworkCode, Location, Convert.ToDateTime(dateTime), Remarks).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
            finally
            {
            }
        }
    }
}
