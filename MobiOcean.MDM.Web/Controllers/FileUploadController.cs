using MobiOcean.MDM.BAL.BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

/// <summary>
/// Summary description for FileUploadController
/// </summary>
/// 
namespace MobiOcean.MDM.Web.Controller
{
    public class FileUploadController : APIBase
    {
        int UserId = 0, ClientId = 0, DeviceId = 0;
        DataTable dt;

        [HttpGet]
        public FinalFileResponse GetFileListByUser(string AppId, int IsUpdate = 0, int Index = 0)
        {
            FinalFileResponse grplst = new FinalFileResponse();
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
                grplst.Result = 0;
                grplst.Message = "You are not authorised user. Please contact our support team!";

            }
            if (DeviceId > 0)
            {
                List<FileResponse> lst = new List<FileResponse>();
                FileUploadBAL fa = new FileUploadBAL();
                DataTable dt = fa.GetFilesByUser(Index, IsUpdate, AppId, ClientId, UserId);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        FileResponse grpbal = new FileResponse
                        {
                            FileId = Convert.ToInt32(row["Id"].ToString()),
                            FileName = row["UserFileName"].ToString() + "." + row["FileType"].ToString(),
                            FilePath = row["FilePath"].ToString()


                        };
                        lst.Add(grpbal);
                    }
                    grplst.Result = 1;
                    grplst.Message = "Success";
                    grplst.data = lst;

                }
                else
                {
                    grplst.Result = 0;
                    grplst.Message = "You dont fave any files!";

                }
                return grplst;
            }
            return grplst;
        }
    }
}
