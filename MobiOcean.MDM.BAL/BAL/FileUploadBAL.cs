using MobiOcean.MDM.DAL.DAL.FileUploadTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FileUploadBAL
/// </summary>
namespace MobiOcean.MDM.BAL.BAL
{
    public class FileUploadBAL
    {
        QueriesTableAdapter qa;
        tblFileUploadTableAdapter ta;
        GetUserListByUserIdTableAdapter getUserListByUserIdTableAdapter;
       

        public string UserFileName { get; set; }
        public string ApplicatioFileName { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        public string FilePath { get; set; }
        public int ClientId { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }
        public int FileId { get; set; }
        public int Permission { get; set; }
        public string CreationDate { get; set; }
        public string Result { get; set; }
        public string Message { get; set; }
        public FileUploadBAL()
        {

        }
        public int InsertFileUpload(FileUploadBAL fa)
        {
            try
            {
                qa = new QueriesTableAdapter();
                var data = qa.InsertFileQuery(fa.UserFileName, fa.ApplicatioFileName, fa.FileType, fa.FileSize, fa.FilePath, fa.ClientId, fa.Status, fa.CreatedBy);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DataTable GetFileUploadData(int ClientId)
        {
            ta = new tblFileUploadTableAdapter();
            DataTable dt = ta.GetFileUploadData(ClientId);
            return dt;
        }        
        public int InsertAssignUserList(DataTable dt)
        {
            try
            {
                ta = new tblFileUploadTableAdapter();
                ta.Insert_tblAssignFileToUser(dt);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public DataTable GetFilesByUser(int index, int IsUpdate, string AppId, int ClientId, int UserId)
        {
            try
            {
                getUserListByUserIdTableAdapter = new GetUserListByUserIdTableAdapter();
                return getUserListByUserIdTableAdapter.GetUserListByIds(index, IsUpdate, AppId, ClientId, UserId);
            }
            catch (Exception)
            {
                return new DataTable();        
            }
        }

       
        public string GetFileNameById(int FileId)
        {
            try
            {
                qa = new QueriesTableAdapter();
                return qa.GetFileNameById(FileId);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
    public class FileResponse
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
    public class FinalFileResponse
    {
        public int Result { get; set; }
        public string Message { get; set; }
        public List<FileResponse> data { get; set; }
    }
}
