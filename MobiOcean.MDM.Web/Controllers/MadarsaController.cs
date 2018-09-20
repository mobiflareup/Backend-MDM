using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using MobiOcean.MDM.BAL.BAL;

namespace MobiOcean.MDM.Web.Controller
{
    public class MadarsaController : ApiController
    {
        MadarsaBAL mbal;
        DataTable dt;
        int k = 0, res = 0;
        // POST api/<controller>
        [ActionName("IUMadarsaDetail")]
        public Dictionary<string, string> Post([FromBody]MadarsaBAL value)
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            try
            {
                if (value.MadarsaDetailsList.Count() > 0)
                {
                    foreach (var obj in value.MadarsaDetailsList)
                    {
                        mbal = new MadarsaBAL();
                        mbal.DeviceId = obj.DeviceId;
                        mbal.DeviceLatitude = obj.DeviceLatitude;
                        mbal.DeviceLongitude = obj.DeviceLongitude;
                        mbal.District = obj.District;
                        mbal.MadarsaId = obj.MadarsaId;
                        mbal.MadarsaName = obj.MadarsaName;
                        mbal.MadarsaRegistrationNumber = obj.MadarsaRegistrationNumber;
                        mbal.IMINo = obj.IMINo;
                        mbal.MadarsaEmail = obj.MadarsaEmail;
                        res = mbal.IU_MadarsaDetail();
                        if (res > 0)
                        {
                            k++;
                        }
                    }
                }
                else
                {
                    details.Add("ResponseCode", "0");
                    details.Add("ResponseMessage", "Failed");
                }
                if (k > 0)
                {
                    details.Add("ResponseCode", "1");
                    details.Add("ResponseMessage", "Success");
                }
                else
                {
                    details.Add("ResponseCode", "0");
                    details.Add("ResponseMessage", "Failed");
                }
                return details;
            }
            catch (Exception)
            {
                details.Add("ResponseCode", "0");
                details.Add("ResponseMessage", "Something Went Wrong");
                return details;
            }
        }
        public Dictionary<string, string> RegisterDevice([FromBody]MadarsaBAL value)
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            try
            {
                mbal = new MadarsaBAL();
                mbal.DeviceId = value.DeviceId;
                mbal.DeviceLatitude = value.DeviceLatitude;
                mbal.DeviceLongitude = value.DeviceLongitude;
                mbal.MadarsaName = value.MadarsaName;
                mbal.MadarsaRegistrationNumber = value.MadarsaRegistrationNumber;
                int res = mbal.RegisterDevice();
                if (res > 0)
                {
                    details.Add("ResponseCode", "1");
                    details.Add("ResponseMessage", "Success");
                    details.Add("MadarsaId", res.ToString());
                }
                else
                {
                    details.Add("ResponseCode", "0");
                    details.Add("ResponseMessage", "Failed");
                }
                return details;
            }
            catch (Exception)
            {
                details.Add("ResponseCode", "0");
                details.Add("ResponseMessage", "Something Went Wrong");
                return details;
            }
        }
        [HttpGet]
        public ResponseClass GetParaTeacherList(int MadarsaId)
        {
            ResponseClass details = new ResponseClass();
            try
            {
                mbal = new MadarsaBAL();
                mbal.MadarsaId = MadarsaId;
                List<ParaTeacherList> parateacherlist = new List<ParaTeacherList>();
                dt = mbal.GetParaTeacherList();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow ob in dt.Rows)
                    {
                        ParaTeacherList ptlist = new ParaTeacherList
                        {
                            ParaTeacherId = Convert.ToInt32(ob["ParaTeacherId"].ToString()),
                            ParaTeacherName = ob["ParaTeacherName"].ToString(),
                            ParaTeacherEmail = ob["ParaTeacherEmail"].ToString(),
                            ParaTeacherAadhar = ob["ParaTeacherAadhar"].ToString(),
                            ParaTeacherMobile = ob["ParaTeacherMobile"].ToString(),
                            MadarsaId = Convert.ToInt32(ob["MadarsaId"].ToString())
                        };
                        parateacherlist.Add(ptlist);
                    }
                    details.ResponseCode = "1";
                    details.ResponseMessage = "Success";
                    details.ParaTeacherList = parateacherlist;
                }
                else
                {
                    details.ResponseCode = "0";
                    details.ResponseMessage = "Failed";
                    details.ParaTeacherList = parateacherlist;
                }
                return details;
            }
            catch (Exception)
            {
                details.ResponseCode = "0";
                details.ResponseMessage = "Something went wrong";
                details.ParaTeacherList = null;
                return details;
            }
        }
        public Dictionary<string, string> IU_ParaTeacherList([FromBody]MadarsaBAL value)
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            try
            {
                if (value.ParaTeacherList.Count() > 0)
                {
                    foreach (var obj in value.ParaTeacherList)
                    {
                        mbal = new MadarsaBAL();
                        mbal.ParaTeacherId = obj.ParaTeacherId;
                        mbal.ParaTeacherName = obj.ParaTeacherName;
                        mbal.ParaTeacherEmail = obj.ParaTeacherEmail;
                        mbal.ParaTeacherAadhar = obj.ParaTeacherAadhar;
                        mbal.ParaTeacherMobile = obj.ParaTeacherMobile;
                        mbal.MadarsaId = obj.MadarsaId;
                        res = mbal.IU_ParaTeacherList();
                        if (res > 0)
                        {
                            k++;
                        }
                    }
                }
                else
                {
                    details.Add("ResponseCode", "0");
                    details.Add("ResponseMessage", "Failed");
                }
                if (k > 0)
                {
                    details.Add("ResponseCode", "1");
                    details.Add("ResponseMessage", "Success");
                }
                else
                {
                    details.Add("ResponseCode", "0");
                    details.Add("ResponseMessage", "Failed");
                }
                return details;
            }
            catch (Exception)
            {
                details.Add("ResponseCode", "0");
                details.Add("ResponseMessage", "Something Went Wrong");
                return details;
            }
        }
        public Dictionary<string, string> InsertAttendance([FromBody]MadarsaBAL value)
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            try
            {
                mbal = new MadarsaBAL();
                mbal.ParaTeacherId = value.ParaTeacherId;
                mbal.DateTime = value.DateTime;
                mbal.DeviceLatitude = value.DeviceLatitude;
                mbal.DeviceLongitude = value.DeviceLongitude;
                res = mbal.MarkAttendance();
                if (res > 0)
                {
                    details.Add("ResponseCode", "1");
                    details.Add("ResponseMessage", "Success");
                }
                else
                {
                    details.Add("ResponseCode", "0");
                    details.Add("ResponseMessage", "Failed");
                }
                return details;
            }
            catch (Exception)
            {
                details.Add("ResponseCode", "0");
                details.Add("ResponseMessage", "Failed");
                return details;
            }
        }
        public Dictionary<string, string> UploadImage([FromBody]MadarsaBAL value)
        {

            Dictionary<string, string> details = new Dictionary<string, string>();
            try
            {
                mbal = new MadarsaBAL();
                mbal.ParaTeacherId = value.ParaTeacherId;
                mbal.ImagePath = value.ImagePath;
                res = mbal.UploadImage();
                if (res > 0)
                {
                    details.Add("ResponseCode", "1");
                    details.Add("ResponseMessage", "Success");
                }
                else
                {
                    details.Add("ResponseCode", "0");
                    details.Add("ResponseMessage", "Failed");
                }
                return details;
            }
            catch (Exception)
            {
                details.Add("ResponseCode", "0");
                details.Add("ResponseMessage", "Something Went Wrong");
                return details;
            }
        }
    }
}
