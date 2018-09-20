using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MobiOcean.MDM.BAL.Model;
using MobiOcean.MDM.DAL.DAL.SOSDALTableAdapters;
using MobiOcean.MDM.BAL.Query;

/// <summary>
/// Summary description for SOSBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class SOSBAL
    {
       
        
        tblUserSosContactsTableAdapter usersosContact;
        SosCameraDetailTableAdapter soscam;
        tblFilesDetailTableAdapter tblFile;

        public FileModel fileModel { get; set; }        
        public int ContactId { get; set; }
        public string contactNo { get; set; }       
        public double Latitude { get; set; }       
        public double Longitude { get; set; }        
        public int UserId { get; set; }        
        public string personName { get; set; }
        public string location { get; set; }
        public string logdatetime { get; set; }
        public SOSBAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int InsertSosCamera()
        {
            try
            {
                soscam = new SosCameraDetailTableAdapter();
                soscam.InsertSosCamera(UserId, personName, contactNo, Latitude.ToString(), Longitude.ToString(), location, Convert.ToDateTime(logdatetime));
                return 1;
            }
            catch { return 0; }
        }        
        public DataTable GetSosContactDeetails()
        {
            usersosContact = new tblUserSosContactsTableAdapter();
            return usersosContact.GetSosContact(UserId);
        }
        public int InsertuserSosContact()
        {
            usersosContact = new tblUserSosContactsTableAdapter();
            return Convert.ToInt32(usersosContact.InsertUserSosContact(ContactId, contactNo, UserId));
        }
        public int DeleteUserSosContact()
        {
            usersosContact = new tblUserSosContactsTableAdapter();
            return Convert.ToInt32(usersosContact.DeleteUserSosContact(ContactId));
        }
        public int InsertFileDetails()
        {
            tblFile = new tblFilesDetailTableAdapter();
            return Convert.ToInt32(tblFile.InsertFileDetail(fileModel.ClientId, fileModel.UserId, fileModel.DeviceId, fileModel.AppId, fileModel.FileName, fileModel.FilePath, fileModel.IsAudio, fileModel.IsforSos, fileModel.Latitude, fileModel.Longitude, fileModel.LogDateTime, fileModel.CellId, fileModel.locationAreaCode, fileModel.mobileCountryCode, fileModel.mobileNetworkCode, fileModel.location).ToString());
        }
    }
}
