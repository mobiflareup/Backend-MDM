using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
/// <summary>
/// Summary description for FileBAL
/// </summary>
namespace MobiOcean.MDM.BAL.Model
{
    public class FileModel
    {
        public int UserId { get; set; }       
        public int ClientId { get; set; }         
        public int DeviceId { get; set; }        
        public int IsforSos { get; set; }         
        public int IsAudio { get; set; }         
        public string FileName { get; set; }        
        public string FilePath { get; set; }        
        public string AppId { get; set; }        
        public string Longitude { get; set; }         
        public string Latitude { get; set; }        
        public string LogDateTime { get; set; }         
        public string CellId { get; set; }       
        public string locationAreaCode { get; set; }        
        public string mobileCountryCode { get; set; }        
        public string mobileNetworkCode { get; set; }
        public string location { get; set; }
    }
}
